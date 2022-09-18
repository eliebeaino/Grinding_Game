using GrindingGame.Core;
using System;
using UnityEngine;

namespace GrindingGame.Mining
{
    public class Miner : MonoBehaviour, IAction
    {
        public float miningRadius = 5f;
        public float miningPower = 1f;
        public float miningSpeed = 1f;
        public int targetCount = 2;
        [SerializeField] GameObject radiusDisplay = null;


        void Update()
        {
            CheckForMineables();
            ShowRadius();
        }

        void CheckForMineables()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, miningRadius,Vector3.up);       
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.GetComponent<Mineable>())
                {
                    MineStuff(hit.collider.GetComponent<Mineable>());
                }
            }
        }

         void MineStuff(Mineable mineable)
        {
            mineable.GetMined(miningPower, miningSpeed);
        }

        public void ShowRadius()
        {
            radiusDisplay.transform.localScale = new Vector3(miningRadius, miningRadius, 1);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, miningRadius);
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}
