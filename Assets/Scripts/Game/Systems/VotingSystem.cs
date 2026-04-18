using MessagePipe;
using R3;
using System;
using System.Collections.Generic;
using Temp.Game.Events;


namespace Temp.Game.Systems
{
    public class VotingSystem
    {
        private readonly ISubscriber<PlayerSelectDungeonEvent> _subscriber;

        private Dictionary<int, int> _votes = new();

        public VotingSystem(ISubscriber<PlayerSelectDungeonEvent> subscriber)
        {
            _subscriber = subscriber;
        }

        public IDisposable Start()
        {
            _votes.Clear();

            return _subscriber.Subscribe(e =>
            {
                _votes[e.PlayerId] = e.DungeonId;
            });
        }

        public bool IsAllVoted(int playerCount)
        {
            return _votes.Count >= playerCount;
        }

        public int GetResult()
        {
            var count = new Dictionary<int, int>();

            foreach (var v in _votes.Values)
            {
                if (!count.ContainsKey(v)) count[v] = 0;
                count[v]++;
            }

            int max = -1;
            int result = -1;

            foreach (var kv in count)
            {
                if (kv.Value > max)
                {
                    max = kv.Value;
                    result = kv.Key;
                }
            }

            return result;
        }
    }
}