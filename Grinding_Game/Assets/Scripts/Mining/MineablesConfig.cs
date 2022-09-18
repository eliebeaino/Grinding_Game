using UnityEngine;

namespace GrindingGame.Mining
{
    [CreateAssetMenu(fileName = "Mineable", menuName = "Mineable/Make New Mineable", order = 0)]
    public class MineablesConfig : ScriptableObject
    {
        [SerializeField] private RessourceType ressourceType;
        public float health;
        public int level;
        public float respawnTimer;
        public float regenerationRate;
    }
}

