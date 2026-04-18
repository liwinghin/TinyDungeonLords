using Cysharp.Threading.Tasks;
using MessagePipe;
using System.Collections.Generic;
using UnityEngine;
using Temp.Game.Events;

namespace Temp.Game.Player
{
    public class AIPlayerInput : IPlayerInput
    {
        private readonly IPublisher<PlayerSelectDungeonEvent> _publisher;
        private readonly int _id;

        public AIPlayerInput(IPublisher<PlayerSelectDungeonEvent> publisher, int id)
        {
            _publisher = publisher;
            _id = id;
        }

        public async UniTask SelectDungeon(List<int> options)
        {
            await UniTask.Delay(500);

            int choice = options[Random.Range(0, options.Count)];

            _publisher.Publish(new PlayerSelectDungeonEvent
            {
                PlayerId = _id,
                DungeonId = choice
            });
        }
        public async UniTask<int> SelectCard(List<int> options)
        {
            await UniTask.Delay(300);
            return options[Random.Range(0, options.Count)];
        }

        public async UniTask<int> SelectSkill(List<int> options)
        {
            await UniTask.Delay(100);
            return options[0];
        }
    }
}
