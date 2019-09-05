using Assets.Scripts.AI;
using UnityEngine;
using UnityEngine.AI;

/**
 * This state implements the behavior for the AI simply wandering
 * around aimlessly, and lightly directs the AI to be in places
 * near the player. Checks if the player is visible and reports
 * shouldTransition = true if that's the case.
 */
public class AIWanderState : IState
{
    private NavMeshAgent agent;
    private Transform aiTransform;
    private Transform playerTransform;
    // half of the field of view we want the AI to have
    private float fovDeg = 120f;
    private float halfFOVRad;
    // true if we heard the player during the last frame, 
    // that is, the player fired a made noise event
    private bool heardPlayer;

    public AIWanderState(AIStateMachine parent)
    {
        agent = parent.aiObj.GetComponent<NavMeshAgent>();
        //this.parent = parent;
        aiTransform = parent.aiObj.transform;
        playerTransform = parent.playerObj.transform;
        halfFOVRad = (((fovDeg / 2) * Mathf.PI) / 180f);
        heardPlayer = false;
        // Registering our method as a listener for any object making noise, that
        // way the AI can respond to nearby things happening in the world, as well
        // as "hear" the player.
        EventCenter.Instance.ObjectMadeNoise.AddListener(heardNoiseEventListener);
        aiTransform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void ExitState() { }

    // This gets fired by any objects that make noise (or at least, objects that
    // make noise *should* fire this event).
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
        // If we've heard the player and they are fairly nearby, we'll say that we've actually heard the player
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

    // If the player is in an unobstructed line of sight to the AI and the AI is actually facing
    // the player, AND the player is super super far away, then we can see the player
    private bool canSeePlayer()
    {
        float distToPlayer = AIUtility.findWalkableDistance(aiTransform.position, playerTransform.position);
        bool inFOV = AIUtility.objectInFieldOfView(playerTransform.position, aiTransform, halfFOVRad);
        bool hasLineOfSight = AIUtility.objectVisibleFromPosition(playerTransform.gameObject, aiTransform.position);

        return (distToPlayer <= 50f && inFOV && hasLineOfSight);
    }

    /**
     * Returns true if the player can be seen or heard so we can transition to
     * a chase state (or whatever is appropriate).
     */
    public bool ShouldTransition()
    {
        if (canSeePlayer() || canHearPlayer())
        {
            Debug.Log("Leaving AI Wander state!");
            return true;
        }
        return false;
    }

    /**
     * Paths the AI to the next semi-random place to wander.
     * The AI doesn't do anything besides walking to areas and picking
     * new areas to travel to, the logic for finding the player is in
     * ShouldTransition().
     */
    public void Update()
    {
        // This is size of the circle we draw around the player
        int m_range = 20;
        // If we're still heading to the last chosen 
        // destination, then just quit out
        if (agent.pathPending || agent.remainingDistance > 0.5f)
            return;

        // Essentially draw a circle around the player, then pick a random point
        // in the circle, with radius 20, and send the AI to that X,Z coordinate
        Vector2 rand = Random.insideUnitCircle;
        agent.SetDestination(playerTransform.position + m_range * new Vector3(rand.x, 0.2f, rand.y));
        Debug.Log("Destination: " + agent.destination);
    }
}
