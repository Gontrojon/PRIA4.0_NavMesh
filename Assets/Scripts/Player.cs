using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private Transform cameraTransform;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed;
    private float playerSpeedFree = 6f;
    private float playerSpeedPushable = 1.8f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private PlayerState state;
    private Animator animator;
    private bool pickedObjectTooHight = false;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (state == PlayerState.Push)
        {
            playerSpeed = playerSpeedPushable;
        }
        else if (state == PlayerState.Run)
        {
            playerSpeed = playerSpeedFree;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Transformamos el movimiento en la direccion de la cámara
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        move = move.normalized;

        if (pickedObjectTooHight)
        {
            move = Vector3.zero;
        }

        Vector3 displacement = move * Time.deltaTime * playerSpeed;
        controller.Move(displacement);

        if (move != Vector3.zero && groundedPlayer && !pickedObjectTooHight)
        {
            gameObject.transform.forward = move;
            SetState(PlayerState.Run);
        }
        else if (groundedPlayer && move == Vector3.zero)
        {

            SetState(PlayerState.Idle);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer && state != PlayerState.Push && !pickedObjectTooHight)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            SetState(PlayerState.Jump);
        }
        else if (playerVelocity.y < 0 && !groundedPlayer && state != PlayerState.Fall)
        {
            SetState(PlayerState.Fall);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void SetState(PlayerState newState)
    {
        if (state != newState)
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Run");
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("Fall");
            animator.ResetTrigger("Push");
            state = newState;
            animator.SetTrigger($"{state}");
            //print($"triguereado el estado: {state}");
        }
    }

}

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall,
    Push
}
