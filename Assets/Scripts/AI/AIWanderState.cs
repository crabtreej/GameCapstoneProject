using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderState : IState
{
    private AIStateMachine parent;
    private NavMeshAgent agent;
    private Transform aiTransform;
    private Transform playerTransform;
    private float fovDeg = 120f;
    private float halfFOVRad; 
    public AIWanderState(AIStateMachine parent)
    {
        agent = parent.aiObj.GetComponent<NavMeshAgent>();
        this.parent = parent;
        aiTransform = parent.aiObj.transform;
        playerTransform = parent.playerObj.transform;
        halfFOVRad = (((fovDeg / 2) * Mathf.PI) / 180f);
    }

    public void ExitState()
    {
    }

    public bool ShouldTransition()
    {
        float distToPlayer = AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position);
        if (distToPlayer <= 10f || (AIUtility.objectInFieldOfView(playerTransform.position, aiTransform, halfFOVRad) 
            && AIUtility.objectVisibleFromPosition(playerTransform.gameObject, aiTransform.position)))
        {
            Debug.Log("Leaving AI Wander state!");
            return true;
        }
        return false;
    }

    public void Update()
    {
        int m_range = 20;
        if (agent.pathPending || agent.remainingDistance > 0.5f)
            return;

        Vector2 rand = Random.insideUnitCircle;
        agent.SetDestination(playerTransform.position + m_range * new Vector3(rand.x, 0.1f, rand.y));
    }
}
