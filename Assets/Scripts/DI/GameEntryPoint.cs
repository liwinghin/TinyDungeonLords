using VContainer.Unity;
using Temp.Core;

namespace Temp.DI
{
    public class GameEntryPoint : IStartable
    {
        private readonly GameLoop _gameLoop;

        public GameEntryPoint(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }

        public async void Start()
        {
            await _gameLoop.StartGame();
        }
    }
}