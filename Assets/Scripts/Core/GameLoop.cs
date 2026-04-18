using Cysharp.Threading.Tasks;
using Temp.Game.Player;
using Temp.Game.States;

namespace Temp.Core
{
    public class GameLoop
    {
        private readonly GameStateMachine _fsm;
        private readonly PlayerManager _pm;
        private readonly GameStateFactory _factory;

        public GameLoop(
            GameStateMachine fsm,
            PlayerManager pm,
            GameStateFactory factory)
        {
            _fsm = fsm;
            _pm = pm;
            _factory = factory;
        }

        public async UniTask StartGame()
        {
            _pm.CreatePlayers();

            await _fsm.ChangeState(_factory.Create<GameStartState>());
        }
    }
}