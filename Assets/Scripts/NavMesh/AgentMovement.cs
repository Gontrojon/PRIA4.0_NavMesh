using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{

    public Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        // obtiene el componente navmeshagent
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    private void Update()
    {
        // se le asigna el destino como la posicion de la goal
        agent.destination = goal.position;
    }
}
