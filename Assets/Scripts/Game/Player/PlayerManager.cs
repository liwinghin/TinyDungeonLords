using System.Collections.Generic;
using MessagePipe;
using Temp.Game.Events;

namespace Temp.Game.Player
{
    public class PlayerManager
    {
        public List<Player> Players = new();

        private readonly IPublisher<PlayerSelectDungeonEvent> _publisher;

        public PlayerManager(IPublisher<PlayerSelectDungeonEvent> publisher)
        {
            _publisher = publisher;
        }

        public void CreatePlayers()
        {
            Players.Clear();

            for (int i = 0; i < 4; i++)
            {
                Players.Add(new Player
                {
                    Id = i,
                    Input = new AIPlayerInput(_publisher, i)
                });
            }
        }
    }
}