using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public static class AIUtility
    {
        public static float findWalkableDistance(Vector3 startPosition, Vector3 endPosition)
        {
            // Player Unreachable
            float distSum = Mathf.Infinity;
            NavMeshPath path = new NavMeshPath();
            if(NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path))
            {
                Debug.Log("path to object exists");
                distSum = 0;

                if(path.corners.Length == 1)
                {
                    distSum += Vector3.Distance(startPosition, path.corners[0]);
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

            //Debug.Log("Path length is " + distSum);
            return distSum;
        }

        /**
         * Checks if there is an unimpeded line of sight from the enemy to the player
         * Does NOT respect field of view/where the enemy is facing. Use in tandem with objectInFOV
         */
        public static bool objectVisibleFromPosition(GameObject target, Vector3 positionToLookFrom)
        {
            RaycastHit hit;
            bool visible = (Physics.Raycast(positionToLookFrom, target.transform.position - positionToLookFrom, out hit) 
                && hit.collider.gameObject == target);
            if(visible)
            {
                Debug.Log("Target visible");
            }
            return visible;
        }

        /**
         * Checks if the player is in the enemy's cone of sight/field of view
         * Does NOT check if the player is actually visible unimpeded by objects. 
         * Use in tandem with objectVisibleFromPosition
         */
        public static bool objectInFieldOfView(Vector3 target, Transform transformToLookFrom, float halfFOVRad)
        {
            var normDir = Vector3.Normalize(target - transformToLookFrom.position);
            var normForward = Vector3.Normalize(transformToLookFrom.forward);
            bool inFOV = Mathf.Acos(Vector3.Dot(normDir, normForward)) <= halfFOVRad;

            if (inFOV)
            {
                Debug.Log("Target in FOV");
            }
            return inFOV;
        }
    }
}
