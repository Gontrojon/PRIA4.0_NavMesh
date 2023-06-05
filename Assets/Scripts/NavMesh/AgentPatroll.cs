using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentPatroll : MonoBehaviour
{

    public List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    // variable del animador
    private Animator animator;
    // Variable para controlar estados
    private PlayerState state;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
        {
            SetState(PlayerState.Idle);
            return;
        }

        if (animator != null && state != PlayerState.Run)
        {
            SetState(PlayerState.Run);
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % (points.Count-1);
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    // metodo que asigna el estado para que empiece la animacion
    private void SetState(PlayerState newState)
    {
        if (state != newState)
        {
            // reset de triggers anteriores para que no causen problemas
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Run");
            // se cambia el estado guardado por el nuevo
            state = newState;
            // se triggerea la nueva animacion
            animator.SetTrigger($"{state}");
        }
    }
}