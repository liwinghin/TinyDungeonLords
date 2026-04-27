using System.Collections.Generic;
using Temp.Game.Buff;

namespace Temp.Game.Combat
{
    public class MonsterModifier
    {
        private Dictionary<int, List<BuffData>> _modifiers = new();

        public void AddModifier(int playerId, BuffData buff)
        {
            if (!_modifiers.ContainsKey(playerId))
                _modifiers[playerId] = new List<BuffData>();

            _modifiers[playerId].Add(buff);
        }

        public float GetAtkMultiplier(int playerId)
        {
            if (!_modifiers.ContainsKey(playerId)) return 1f;

            float total = 1f;

            foreach (var buff in _modifiers[playerId])
                total += buff.AtkMultiplier;

            return total;
        }

        public float GetHpMultiplier(int playerId)
        {
            if (!_modifiers.ContainsKey(playerId)) return 1f;

            float total = 1f;

            foreach (var buff in _modifiers[playerId])
                total += buff.HpMultiplier;

            return total;
        }

        public void Clear()
        {
            _modifiers.Clear();
        }
    }
}