using System.Collections.Generic;
using Temp.Game.Enemy;
using UnityEngine;

namespace Temp.Game.Combat
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject monsterPrefab;
        [SerializeField, Min(1)] private int initialPoolSize = 3;

        private readonly Stack<MonsterView> _inactiveMonsters = new();
        private readonly List<MonsterView> _activeMonsters = new();
        private Transform _poolRoot;
        private bool _isPoolInitialized;

        private void Awake()
        {
            InitializePool();
        }

        public void Spawn(Monster monster, Vector3 pos)
        {
            InitializePool();

            var view = GetFromPool();
            if (view == null) return;

            view.transform.SetParent(transform, false);
            view.transform.SetPositionAndRotation(pos, Quaternion.identity);
            view.gameObject.SetActive(true);
            view.BindSpawner(this);
            view.Init(monster.FinalHp, monster.FinalAtk);

            _activeMonsters.Add(view);
        }

        public void DespawnAll()
        {
            while (_activeMonsters.Count > 0)
            {
                ReturnToPool(_activeMonsters[_activeMonsters.Count - 1]);
            }
        }

        internal void ReturnToPool(MonsterView view)
        {
            if (view == null) return;
            if (!_activeMonsters.Remove(view)) return;

            view.ResetState();
            view.transform.SetParent(_poolRoot, false);
            view.gameObject.SetActive(false);

            _inactiveMonsters.Push(view);
        }

        private void InitializePool()
        {
            if (_isPoolInitialized) return;

            var poolRootObject = new GameObject("MonsterPool");
            _poolRoot = poolRootObject.transform;
            _poolRoot.SetParent(transform, false);

            for (int i = 0; i < initialPoolSize; i++)
            {
                var view = CreatePooledMonster();
                if (view != null)
                {
                    _inactiveMonsters.Push(view);
                }
            }

            _isPoolInitialized = true;
        }

        private MonsterView GetFromPool()
        {
            while (_inactiveMonsters.Count > 0)
            {
                var view = _inactiveMonsters.Pop();
                if (view != null)
                {
                    return view;
                }
            }

            return CreatePooledMonster();
        }

        private MonsterView CreatePooledMonster()
        {
            var go = Instantiate(monsterPrefab, _poolRoot);
            var view = go.GetComponent<MonsterView>();

            if (view == null)
            {
                Debug.LogError("Monster prefab is missing MonsterView.");
                Destroy(go);
                return null;
            }

            view.BindSpawner(this);
            view.ResetState();
            go.SetActive(false);

            return view;
        }
    }
}
