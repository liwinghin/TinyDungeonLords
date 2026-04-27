using Temp.Game.Combat;
using UnityEngine;

namespace Temp.Game.Enemy
{
    public class MonsterView : MonoBehaviour
    {
        private MonsterSpawner _spawner;
        private float _hp;
        private float _atk;

        internal void BindSpawner(MonsterSpawner spawner)
        {
            _spawner = spawner;
        }

        public void Init(float hp, float atk)
        {
            _hp = hp;
            _atk = atk;

            Debug.Log($"Monster Spawned HP:{hp} ATK:{atk}");
        }

        public void Despawn()
        {
            _spawner?.ReturnToPool(this);
        }

        public void ResetState()
        {
            _hp = 0f;
            _atk = 0f;
        }
    }
}
