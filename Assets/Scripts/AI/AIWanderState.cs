using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIWanderState : IState
{
    private AIStateMachine parent;
    private NavMeshAgent agent;
    private Transform aiTransform;
    private Transform playerTransform;
    private float fovDeg = 120f;
    private float halfFOVRad;
    private bool heardPlayer;

    public AIWanderState(AIStateMachine parent)
    {
        agent = parent.aiObj.GetComponent<NavMeshAgent>();
        this.parent = parent;
        aiTransform = parent.aiObj.transform;
        playerTransform = parent.playerObj.transform;
        halfFOVRad = (((fovDeg / 2) * Mathf.PI) / 180f);
        heardPlayer = false;
        EventCenter.Instance.ObjectMadeNoise.AddListener(heardNoiseEventListener);
        aiTransform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void ExitState() { }

    private void heardNoiseEventListener(GameObject noiseSource)
    {
        Debug.Log("Heard a noise in Wander State.");
        if (noiseSource == playerTransform.gameObject)
        {
            heardPlayer = true;
        }
    }

    private bool canHearPlayer()
    {
        bool heard = false;
        if(heardPlayer && (AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position) <= 20f))
        {
            heard = true;
            Debug.Log("Heard player");
        }
        
        // Want to clear this so it isn't cheating by always hearing them
        heardPlayer = false;
        return heard;
    }

    private bool canSeePlayer()
    {
        float distToPlayer = AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position);
        bool inFOV = AIUtility.objectInFieldOfView(playerTransform.position, aiTransform, halfFOVRad);
        bool hasLineOfSight = AIUtility.objectVisibleFromPosition(playerTransform.gameObject, aiTransform.position);

        return (distToPlayer <= 50f && inFOV && hasLineOfSight);
    }

    public bool ShouldTransition()
    {
        if (canSeePlayer() || canHearPlayer())
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
