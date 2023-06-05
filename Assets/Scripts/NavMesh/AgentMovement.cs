using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    // transfor de la meta que tiene que alcanzar
    public Transform goal;
    // variable  del agente
    private NavMeshAgent agent;
    // variable del animador
    private Animator animator;
    // Variable para controlar estados
    private PlayerState state;

    void Start()
    {
        // obtiene el componente navmeshagent
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // se le asigna el destino como la posicion de la goal
        agent.destination = goal.position;
        // si la distancia al punto goal es menor a 1 metro se le pone en idle
        if (animator != null && agent.remainingDistance < 1f && state != PlayerState.Idle)
        {
            SetState(PlayerState.Idle);
        }

        // si la distancia al punto de goal es mayor a 1 metro se le pone el estado de correr
        if (animator != null && state != PlayerState.Run && agent.remainingDistance > 1f)
        {
            SetState(PlayerState.Run);
        }

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