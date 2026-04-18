using UnityEngine;

namespace Temp.Game.Player
{    
    public class PlayerStats
    {
        public int Attack;
        public int Defense;

        public float AttackMultiplier = 1f;

        public void Reset()
        {
            Attack = 0;
            Defense = 0;
            AttackMultiplier = 1f;
        }
    }
}