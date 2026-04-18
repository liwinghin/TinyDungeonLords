using Temp.Core;
using Cysharp.Threading.Tasks;
using Temp.Game.Constants;

namespace Temp.Game.States
{
    public class MidBossState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly RoundManager _roundManager;

        public MidBossState(GameStateMachine fsm, GameStateFactory factory, RoundManager rm)
        {
            _fsm = fsm;
            _factory = factory;
            _roundManager = rm;
        }

        public async UniTask Enter()
        {
            UnityEngine.Debug.Log("Mid Boss Fight");

            await UniTask.Delay(GameTimeConstants.BossSimulationDelay);

            _roundManager.Next();

            await _fsm.ChangeState(_factory.Create<RoundState>());
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}