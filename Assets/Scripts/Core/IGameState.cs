using Cysharp.Threading.Tasks;

namespace Temp.Core
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Exit();
    }
}