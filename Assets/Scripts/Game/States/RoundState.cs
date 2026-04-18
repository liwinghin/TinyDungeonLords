using Cysharp.Threading.Tasks;
using UnityEngine;
using Temp.Core;

namespace Temp.Game.States
{
    public class RoundState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly RoundManager _roundManager;

        public RoundState(
            GameStateMachine fsm,
            GameStateFactory factory,
            RoundManager roundManager)
        {
            _fsm = fsm;
            _factory = factory;
            _roundManager = roundManager;
        }

        public async UniTask Enter()
        {
            var config = _roundManager.Current;

            Debug.Log($"Round {config.Round} - {config.Type}");

            switch (config.Type)
            {
                case RoundType.Start:
                    await _fsm.ChangeState(_factory.Create<StartRoundState>());
                    break;

                case RoundType.CoreLoop:
                    await _fsm.ChangeState(_factory.Create<CoreLoopState>());
                    break;

                case RoundType.Event:
                    await _fsm.ChangeState(_factory.Create<EventState>());
                    break;

                case RoundType.MidBoss:
                    await _fsm.ChangeState(_factory.Create<MidBossState>());
                    break;

                case RoundType.FinalBoss:
                    await _fsm.ChangeState(_factory.Create<FinalBossState>());
                    break;
            }
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}