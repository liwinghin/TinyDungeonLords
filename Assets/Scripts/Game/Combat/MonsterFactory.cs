using Temp.Game.Enemy;
using UnityEngine;

namespace Temp.Game.Combat
{
    public class MonsterFactory
    {
        private readonly MonsterModifier _modifier;

        public MonsterFactory(MonsterModifier modifier)
        {
            _modifier = modifier;
        }

        public Monster Create(int playerId)
        {
            var monster = new Monster
            {
                BaseAtk = 100,
                BaseHp = 1000
            };

            float atkMul = _modifier.GetAtkMultiplier(playerId);
            Debug.Log($"Player {playerId} atkMul: {atkMul}");

            float hpMul = _modifier.GetHpMultiplier(playerId);

            monster.FinalAtk = monster.BaseAtk * atkMul;
            monster.FinalHp = monster.BaseHp * hpMul;

            return monster;
        }
    }
}