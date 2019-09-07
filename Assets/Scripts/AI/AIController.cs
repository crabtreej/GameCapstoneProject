using Assets.Scripts.AI;
using Panda;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{
    public GameObject player;
    private bool heardPlayer;
    private float halfFOVRad;
    private float fovDeg = 120f;
    NavMeshAgent agent;

    /**
     * Start should be used to create any states that you want the StateMachine
     * to run. The states will control the AIs behavior, then ideally all
     * you have to do is update the machine forever.
     */
    void Start()
    {
        EventCenter.Instance.ObjectMadeNoise.AddListener(heardNoiseEventListener);
        halfFOVRad = (((fovDeg / 2) * Mathf.PI) / 180f);
        agent = GetComponent<NavMeshAgent>();
    }

    // This gets fired by any objects that make noise (or at least, objects that
    // make noise *should* fire this event).
    private void heardNoiseEventListener(GameObject noiseSource)
    {
        Debug.Log("Heard a noise in Wander State.");
        if (noiseSource == player)
        {
            
            heardPlayer = true;
        }
    }

    private IEnumerator ClearPlayerHeard()
    {
        yield return new WaitForSeconds(3f);
        heardPlayer = false;
    }

    private bool canHearPlayer()
    {
        // If we've heard the player and they are fairly nearby, we'll say that we've actually heard the player
        bool heard = false;
        if(heardPlayer && (AIUtility.findWalkableDistance(transform.position, player.transform.position) <= 20f))
        {
            StopCoroutine(ClearPlayerHeard());
            StartCoroutine(ClearPlayerHeard());
            heard = true;
            Debug.Log("Heard player");
        }
        
        // Want to clear this so it isn't cheating by always hearing them
        return heard;
    }

    // If the player is in an unobstructed line of sight to the AI and the AI is actually facing
    // the player, AND the player is super super far away, then we can see the player
    private bool canSeePlayer()
    {
        float distToPlayer = AIUtility.findWalkableDistance(transform.position, player.transform.position);
        bool inFOV = AIUtility.objectInFieldOfView(player.transform.position, transform, halfFOVRad);
        bool hasLineOfSight = AIUtility.objectVisibleFromPosition(player.transform.gameObject, transform.position);

        return (distToPlayer <= 50f && inFOV && hasLineOfSight);
    }

   /**
    * FoundPlayer, WanderRandomly, ChasePlayer
    */
    [Task]
    bool FoundPlayer()
    {
        if (canSeePlayer() || canHearPlayer())
        {
            Debug.Log("Leaving AI Wander state!");
            return true;
        }
        return false;   
    }

    [Task]
    void WanderRandomly()
    {
        // This is size of the circle we draw around the player
        int m_range = 20;
        // If we're still heading to the last chosen 
        // destination, then just quit out
        if (!Task.current.isStarting && (agent.pathPending || agent.remainingDistance > 0.5f))
            return;

        // Essentially draw a circle around the player, then pick a random point
        // in the circle, with radius 20, and send the AI to that X,Z coordinate
        Vector2 rand = Random.insideUnitCircle;
        agent.SetDestination(player.transform.position + m_range * new Vector3(rand.x, 0.2f, rand.y));
        if(Task.isInspected)
        {
            Task.current.debugInfo = agent.destination.ToString();
        }
    }

    [Task]
    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        if(AIUtility.findWalkableDistance(transform.position, player.transform.position) <= 1.2f)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void ExploreLastPlayerLocation()
    {
        if(!(agent.pathPending || agent.remainingDistance > 0.5f) || FoundPlayer())
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void KillPlayer()
    {
        SceneManager.LoadScene("Exit");
    }
}
