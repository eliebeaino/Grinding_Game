using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using GrindingGame.Core;

namespace GrindingGame.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [Header("Stats")]
        [SerializeField] private float moveSpeed = 5f; // TODO use stats sheet later and add speed method to all movement callings
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float maxNavPathLength = 40f; // TODO add max distance to travel on navigation, this blocks both player and AI

        // EVENT
        [SerializeField] private UnityEvent runEvent = null;

        // CACHE
        private Animator anim = null;
        private NavMeshAgent navMeshAgent = null;
        private Camera cam = null;
        //private Health health = null;

        // OTHER
        private bool keyboardAnimOverride = false;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            //health = GetComponent<Health>();
            cam = Camera.main;
        }

        private void Start()
        {
            navMeshAgent.speed = moveSpeed;
        }

        private void Update()
        {
            //navMeshAgent.enabled = !health.IsDead();
            //UpdateAnimator();
        }

        public void MoveTowards(Vector3 targetDestination)
        {
            navMeshAgent.destination = targetDestination;
            navMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Check if the path is available to move to and isn't too long.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool CanMoveTo(Vector3 destination)
        {
            // Stores the path, checks if exists || full path to access || path is not long
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        /// <summary>
        /// Calculates the total length of the path for the navmesh to travel - needed to check if the path is too long to avoid moving
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }

        #region Animation (unused yet)
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;  // storing speed value from z axis (only anxis that interests us for animation, moving forward or not)
            anim.SetFloat("Run_Speed", speed); // TODO still needs tweaking
        }

        public void RunTrigger()
        {
            // animation trigger
            runEvent.Invoke();
        }
        #endregion

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}