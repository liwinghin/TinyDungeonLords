using Temp.Core;
using Temp.Game.Player;
using Temp.Game.Context;

namespace Temp.Game.States
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class GameStartState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly PlayerManager _pm;
        private readonly RoundManager _roundManager;
        private readonly GameContext _context;

        public GameStartState(
            GameStateMachine fsm,
            GameStateFactory factory,
            PlayerManager pm,
            RoundManager roundManager,
            GameContext context)
        {
            _fsm = fsm;
            _factory = factory;
            _pm = pm;
            _roundManager = roundManager;
            _context = context;
        }
        public async UniTask Enter()
        {
            Debug.Log("=== Game Start ===");

            _pm.CreatePlayers();

            //　プレイヤー情報をゲームコンテキストにセット
            _context.Players = _pm.Players;

            // プレイヤー初始化
            foreach (var p in _pm.Players)
            {
                _context.PlayerRuntimes[p.Id] = new PlayerRuntime();
            }

            await UniTask.Delay(500);

            await _fsm.ChangeState(_factory.Create<RoundState>());
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}