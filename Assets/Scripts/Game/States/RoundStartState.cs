using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Temp.Core;
using Temp.Game.Player;
using Temp.Game.Systems;
using Temp.Game.Constants;
using Temp.Game.Context;

namespace Temp.Game.States
{
    public class StartRoundState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly PlayerManager _pm;
        private readonly VotingSystem _votingSystem;
        private readonly RoundManager _roundManager;
        private readonly GameContext _context;

        public StartRoundState(
            GameStateMachine fsm,
            GameStateFactory factory,
            PlayerManager pm,
            VotingSystem votingSystem,
            RoundManager roundManager,
            GameContext context)
        {
            _fsm = fsm;
            _factory = factory;
            _pm = pm;
            _votingSystem = votingSystem;
            _roundManager = roundManager;
            _context = context;
        }

        public async UniTask Enter()
        {
            foreach (var kv in _context.PlayerRuntimes)
            {
                Debug.Log($"Player {kv.Key} runtime created");
            }

            Debug.Log("=== Round 1: Dungeon Voting ===");

            // イベント投票
            using var disposable = _votingSystem.Start();

            var dungeonOptions = new List<int> { 1, 2, 3, 4, 5};

            foreach (var p in _pm.Players)
            {
                _ = p.Input.SelectDungeon(dungeonOptions);
            }
 
            await UniTask.WaitUntil(() =>
                _votingSystem.IsAllVoted(_pm.Players.Count));

            int selectedDungeon = _votingSystem.GetResult();

            Debug.Log($"Selected Dungeon: {selectedDungeon}");

            // =========================
            // 準備時間
            // =========================
            Debug.Log("Preparation Phase");

            await UniTask.Delay(GameTimeConstants.PreparationPhaseDuration);

            // 次のターン
            _roundManager.Next();

            await _fsm.ChangeState(_factory.Create<RoundState>());
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}