using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Temp.Game.Player
{
    public interface IPlayerInput
    {
        UniTask SelectDungeon(List<int> options);
        UniTask<int> SelectCard(List<int> options);
        UniTask<int> SelectSkill(List<int> options);
    }
}