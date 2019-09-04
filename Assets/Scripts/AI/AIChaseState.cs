using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIChaseState : IState
{
    private NavMeshAgent agent;
    private Transform aiTransform;
    private Transform playerTransform;
    private float fovDeg = 120f;
    private float halfFOVRad;
    private bool heardPlayer;

    public AIChaseState(AIStateMachine parent)
    {
        agent = parent.aiObj.GetComponent<NavMeshAgent>();
        aiTransform = parent.aiObj.transform;
        playerTransform = parent.playerObj.transform;
        heardPlayer = true;
        EventCenter.Instance.ObjectMadeNoise.AddListener(heardNoiseEventListener);
        aiTransform.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void heardNoiseEventListener(GameObject noiseSource)
    {
        Debug.Log("Heard a noise in ChaseState");
        if (noiseSource == playerTransform.gameObject)
        {
            heardPlayer = true;
        }
    }

    public void ExitState()
    {
        //what should happen in here?
    }

    //might put this method in a player class to reduce repeated code. 
    private bool canHearPlayer()
    {
        bool heard = false;
        if (heardPlayer && (AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position) <= 20f))
        {
            heard = true;
            Debug.Log("Heard player");
        }

		heardPlayer = false;
        return heard;
    }

    //might put this method in a player class to reduce repeated code. 
    private bool canSeePlayer()
    {
        float distToPlayer = AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position);
        bool inFOV = AIUtility.objectInFieldOfView(playerTransform.position, aiTransform, halfFOVRad);
        bool hasLineOfSight = AIUtility.objectVisibleFromPosition(playerTransform.gameObject, aiTransform.position);

        return (distToPlayer <= 50f && inFOV && hasLineOfSight);
    }

    //below 2 should probably be put in sometype of player class but o well for now
    //need to implement "safe zone"
    private bool PlayerIsSafe()
    {
        //check if player is safe, return true if so
        return false;
    }

    private bool PlayerEscaped()
    {
        //if player has not been seen or **HEARD(will have to fix this later), return true. 
        if(canSeePlayer()) // || canHearPlayer())
        {
            return false;
        }
        return true;
    }

    public bool ShouldTransition()
    {
        if (PlayerIsSafe() || PlayerEscaped())
        {
			Debug.Log("Leaving AI Chase state!");
			return true;
        }

        return false;
    }


    public void Update()
    {   
        agent.SetDestination(playerTransform.position);
        
    }


}
