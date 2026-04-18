using Cysharp.Threading.Tasks;
using Temp.Core;

namespace Temp.Game.States
{
    public class EventState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly RoundManager _roundManager;

        public EventState(
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
            UnityEngine.Debug.Log("Event Voting");

            await UniTask.Delay(3000);

            _roundManager.Next();

            await _fsm.ChangeState(_factory.Create<RoundState>());
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}