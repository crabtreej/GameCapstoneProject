using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public static class AIUtility
    {
        /**
         * This method will find the distance between startPosition and 
         * endPosition as defined by walkable areas of the NavMesh of the level.
         * This means that it finds the distance of an actually navigable path 
         * (so not through walls and such) between them. Returns Mathf.Infinity 
         * if the endPosition is not reachable as defined by the NavMesh.
         */
        public static float findWalkableDistance(Vector3 startPosition, Vector3 endPosition)
        {
            // Player Unreachable is Infinity
            float distSum = Mathf.Infinity;

            // Create a NavMeshPath for it to populate
            NavMeshPath path = new NavMeshPath();
            // If a path exists
            if(NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path))
            {
                Debug.Log("path to object exists");
                // Running sum of distance
                distSum = 0;

                // There is always at least two corners in the path because the 
                // start and destination are corners
                // Just loop through the array and sum the distances between each corner
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
            // Sends a raycast in the direction (target - start) (which is to say, in the direction of the targt from 
            // where we're looking, and if there is a hit AND the thing we hit is our target, return true
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
            // target - transformToLookFrom is a vector that points from the AI to the player, then we normalize (magnitude = 1) it
            var normDir = Vector3.Normalize(target - transformToLookFrom.position);
            // this is the "forward" of the AI, i.e. where the AI is looking currently, normalized (magnitude = 1)
            var normForward = Vector3.Normalize(transformToLookFrom.forward);
            // A dot B = |A|*|B|*cos(theta), where theta is the angle between A and B. 
            // |A|=|B|=1 because we normalized, so the angle between A and B is cos^(-1)(A dot b) = theta
            // So if theta <= half our field of view (e.g. 120 degree field of view means 60 degrees vision to any side), then
            // the object is in our field of view
            bool inFOV = Mathf.Acos(Vector3.Dot(normDir, normForward)) <= halfFOVRad;

            if (inFOV)
            {
                Debug.Log("Target in FOV");
            }
            return inFOV;
        }
    }
}
