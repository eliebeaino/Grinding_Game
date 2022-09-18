using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.AI;
using GrindingGame.Movement;
using GrindingGame.Core;

namespace GrindingGame.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField][Tooltip ("Setup as a dummy high value, no need to change")] float maxRaycastDistance = 1000f;
        [SerializeField][Tooltip("Raycast Projection Distance for mouse movement")] float maxNavMeshProjectionDistance = 0.5f;


        // CACHE
        Camera cam = null;
        Mover mover = null;
        ActionScheduler actionScheduler = null;


        private void Awake()
        {
            cam = Camera.main;
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            InteractWithMovement();
        }

        private bool InteractWithMovement()
        {
            Vector3 targetDestination;
            bool hasHit = RayCastNavMesh(out targetDestination);

            if (hasHit)
            {
                if (!mover.CanMoveTo(targetDestination)) return false; // path is too far
                if (Mouse.current.leftButton.isPressed)
                {
                    actionScheduler.StartAction(mover);
                    mover.MoveTowards(targetDestination);
                    //ShowMoveLocation(targetDestination); // TODO VFX EFFECT
                }
            }
            return false;
        }

        /// <summary>
        /// Grabs the target destination on the navigation if it's possible to travel there.
        /// </summary>
        /// <param name="targetDestination"></param>
        /// <returns></returns>
        private bool RayCastNavMesh(out Vector3 targetDestination)
        {
            targetDestination = new Vector3();

            // check for raycast on any object
            RaycastHit[] hits;
            hits = Physics.RaycastAll(GetMouseRay(), maxRaycastDistance);
            if (hits.Length < 0) return false;

            // check if the raycast hit the navmesh
            NavMeshHit navMeshHit;
            foreach (RaycastHit hit in hits)
            {
                bool navHasHit = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);

                if (!navHasHit) continue;

                // assign destination from the raycast hit
                targetDestination = navMeshHit.position;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Grabs the current position of the mouse.
        /// </summary>
        /// <returns></returns>
        private Ray GetMouseRay()
        {
            return cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

    }
}