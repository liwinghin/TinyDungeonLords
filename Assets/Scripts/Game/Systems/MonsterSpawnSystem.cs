using Temp.Game.Combat;
using UnityEngine;

namespace Temp.Game.Systems
{
    public class MonsterSpawnSystem
    {
        private readonly MonsterFactory _factory;
        private readonly MonsterSpawner _spawner;

        public MonsterSpawnSystem(MonsterFactory factory, MonsterSpawner spawner)
        {
            _factory = factory;
            _spawner = spawner;
        }

        public void SpawnForPlayer(int playerId)
        {
            var monster = _factory.Create(playerId);

            var pos = new Vector3(playerId * 2, 0, 0);

            _spawner.Spawn(monster, pos);
        }

        public void DespawnAll()
        {
            _spawner.DespawnAll();
        }
    }
}
