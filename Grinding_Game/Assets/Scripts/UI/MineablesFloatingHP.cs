using GrindingGame.Mining;
using System.Collections;
using UnityEngine;

namespace GrindingGame.UI
{
    public class MineablesFloatingHP : MonoBehaviour
    {
        // CACHE
        [SerializeField] SlicedFilledImage hpBar = null;
        [SerializeField] Canvas canvas = null;
        Mineable mineable = null;


        private void Awake()
        {
            mineable = GetComponentInParent<Mineable>();
        }
        private void Start()
        {
            UpdateHealthBarDisplay();
        }

        private void OnEnable()
        {
            mineable.onMineablesHealthChange += UpdateHealthBarDisplay;
        }
        private void onDisable()
        {
            mineable.onMineablesHealthChange -= UpdateHealthBarDisplay;
        }

        public void UpdateHealthBarDisplay()
        {
            // if dead don't display
            if (mineable.currentHealth<=0)
            {
                canvas.enabled = false;
                return;
            }

            // updates hp bar levels
            canvas.enabled = true;
            float targetHp = mineable.currentHealth / mineable.baseHealth;
            hpBar.fillAmount = targetHp;
        }
    }
}
