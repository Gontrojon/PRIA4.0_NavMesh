using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentPatroll : MonoBehaviour
{

    public List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private const float PATROLL_SPEED = 3.5f;
    private const float CHASE_SPEED = 7f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % (points.Count-1);
    }


    void Update()
    {

        if (SeePlayer())
        {

        }
        Debug.DrawRay(transform.position + new Vector3(0,0.8f,0), transform.forward, Color.green);
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();


        
    }

    private bool SeePlayer()
    {
        RaycastHit hit;

        


        return false;
    }
}