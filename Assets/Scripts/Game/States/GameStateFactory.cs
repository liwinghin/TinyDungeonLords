using VContainer;
using Temp.Core;

namespace Temp.Game.States
{
    public class GameStateFactory
    {
        private readonly IObjectResolver _resolver;

        public GameStateFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Create<T>() where T : IGameState
        {
            return _resolver.Resolve<T>();
        }
    }
}