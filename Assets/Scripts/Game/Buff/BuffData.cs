using UnityEngine;
using Temp.Game.Player;
using System.Collections.Generic;

namespace Temp.Game.Buff
{
    public enum BuffRarity
    {
        Common,
        Rare,
        Epic
    }
    public enum BuffType
    {
        PlayerBuff,
        MonsterDebuff
    }

    [CreateAssetMenu(menuName = "Game/Buff")]
    public class BuffData : ScriptableObject
    {
        public string Id;

        public BuffRarity Rarity;

        public int AttackBonus;
        public int DefenseBonus;

        public string Name;
        public string Description;

        public BuffType Type;

        public float AtkMultiplier; // 0.3 = +30%
        public float HpMultiplier;



        public void Apply(PlayerStats stats)
        {
            stats.Attack += AttackBonus;
            stats.Defense += DefenseBonus;
        }
        public string GetDescription()
        {
            List<string> lines = new();

            if (AttackBonus != 0)
                lines.Add($"ATK +{AttackBonus}");

            if (DefenseBonus != 0)
                lines.Add($"DEF +{DefenseBonus}");

            return string.Join("\n", lines);
        }
    }
}