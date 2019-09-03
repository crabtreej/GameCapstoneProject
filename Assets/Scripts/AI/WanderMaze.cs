using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class WanderMaze : MonoBehaviour
{
    public Vector3 destination;
    public Transform playerPosition;

    NavMeshAgent agent;
    bool followingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        followingPlayer = false;
    }

    float findWalkableDistToPlayer()
    {
        // Player Unreachable
        float distSum = Mathf.Infinity;
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, playerPosition.position, NavMesh.AllAreas, path))
        {
            Debug.Log("path Exists");
            Debug.Log("There are " + path.corners.Length + " corners");
            distSum = 0;

            if(path.corners.Length == 1)
            {
                distSum += Vector3.Distance(transform.position, path.corners[0]);
            }

            // The destination is always one corner, don't need to check 0
            for(int i = 0; i < path.corners.Length; i++)
            {
                if(i < path.corners.Length - 1)
                {
                    distSum += Vector3.Distance(path.corners[i + 1], path.corners[i]);
                }
            }
        }

        Debug.Log("Distance to player: " + distSum);
        return distSum;
    }

    // Update is called once per frame
    void Update()
    {
        var distToPlayer = findWalkableDistToPlayer();
        RaycastHit hit;
        if (distToPlayer <= 10f || (Physics.Raycast(transform.position, playerPosition.position - transform.position, out hit) 
            && hit.collider.gameObject == playerPosition.gameObject))
        {
            agent.SetDestination(playerPosition.position);
            destination = agent.destination;           
        }
        else if(distToPlayer > 10f)
        {
            int m_range = 20;
            if (agent.pathPending || agent.remainingDistance > 0.5f)
                return;

            Vector2 rand = Random.insideUnitCircle;
            agent.SetDestination(playerPosition.position + m_range * new Vector3(rand.x, 0.1f, rand.y));
            destination = agent.destination;
        }
    }
}
