using System;
using UnityEngine;

namespace GrindingGame.Mining
{
    public class Mineable : MonoBehaviour
    {
        // BASE STATS
        [SerializeField] private RessourceType ressourceType;
        public float baseHealth = 1f;
        public float currentHealth = 1f;
        public int level = 1;
        public float respawnTimeDelay = 10f;
        public float regenerationRate = 0.1f;

        // CORE MINEABLE VARIABLES
        public bool beingMined = false;
        public bool fullyMined = false;
        public event Action onMineablesHealthChange = null;

        // TIMER VARIALBLES ONLY FOR RESETS
        float miningTimer = 0f;
        float respawnTimer = 0f;
        float regenerationTimer = 0f;

        // CACHE
        MeshRenderer meshRenderer;
        Collider col;
       
        private void Awake()
        {
            col = GetComponent<Collider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            currentHealth = baseHealth;
        }

        private void Update()
        {
            Regenerate();
            Respawn();
        }


        private void Regenerate()
        {
            if (!beingMined && !fullyMined && (baseHealth >= currentHealth))
            {
                regenerationTimer += Time.deltaTime;
                if (regenerationTimer >=regenerationRate)
                {
                    currentHealth += regenerationRate;
                    onMineablesHealthChange();
                    regenerationTimer = 0f;
                }
                // when hit max hp stop regeneration, and this is to avoid overcap
                if (currentHealth >= baseHealth) currentHealth = baseHealth;
                onMineablesHealthChange();
            }
            beingMined = false;
        }

        public void GetMined(float miningPower, float miningSpeed)
        {
            beingMined = true;
            miningTimer += Time.deltaTime;
            if (miningSpeed <= miningTimer)
            {
                Debug.Log(gameObject.name + " was mined for " + miningPower);
                currentHealth -= miningPower;
                onMineablesHealthChange();
                miningTimer = 0;
            }

            if(currentHealth <= 0)
            {
                fullyMined = true;
                beingMined = false;
                meshRenderer.enabled = false;
                col.enabled = false;  
            }
        }

        private void Respawn()
        {
            if (fullyMined) BeginRespawn();
        }

        private void BeginRespawn()
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimeDelay <= respawnTimer)
            {
                currentHealth = baseHealth;
                onMineablesHealthChange();
                meshRenderer.enabled = true;
                col.enabled = true;
                fullyMined = false;
                respawnTimer = 0;
            }
        }
    }
}
