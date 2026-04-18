using System.Collections.Generic;
using UnityEngine;

namespace Temp.Game.Buff
{
    public class CardSystem
    {
        private readonly List<BuffData> _cardPool;

        private readonly Dictionary<BuffRarity, int> _weights = new()
        {
            { BuffRarity.Common, 75 },
            { BuffRarity.Rare, 20 },
            { BuffRarity.Epic, 5 }
        };

        public CardSystem(List<BuffData> cardPool)
        {
            _cardPool = cardPool;
        }

        public List<BuffData> DrawCards(int count)
        {
            var result = new List<BuffData>();

            for (int i = 0; i < count; i++)
            {
                var rarity = GetRandomRarity();
                var candidates = _cardPool.FindAll(c => c.Rarity == rarity);

                if (candidates.Count == 0)
                {
                    // fallback（避免沒卡）
                    candidates = _cardPool;
                }

                var card = candidates[Random.Range(0, candidates.Count)];
                result.Add(card);
            }

            return result;
        }

        private BuffRarity GetRandomRarity()
        {
            int total = 0;
            foreach (var w in _weights.Values)
                total += w;

            int roll = Random.Range(0, total);

            int current = 0;

            foreach (var kv in _weights)
            {
                current += kv.Value;

                if (roll < current)
                    return kv.Key;
            }

            return BuffRarity.Common;
        }
    }
}