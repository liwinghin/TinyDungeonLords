using R3;
using Cysharp.Threading.Tasks;

namespace Temp.Core { 
    public class GameStateMachine
    {
        private readonly ReactiveProperty<IGameState> _currentState = new();

        public ReadOnlyReactiveProperty<IGameState> CurrentState => _currentState;

        public async UniTask ChangeState(IGameState newState)
        {
            if (_currentState.Value != null)
                await _currentState.Value.Exit();

            _currentState.Value = newState;

            if (newState != null)
                await newState.Enter();
        }
    }
}