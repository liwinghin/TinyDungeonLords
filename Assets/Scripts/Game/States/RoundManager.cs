using UnityEngine;

namespace Temp.Game.States
{
    public class RoundManager
    {
        private int _currentIndex = 0;

        public RoundConfig Current => RoundTable.Rounds[_currentIndex];

        public bool IsEnd => _currentIndex >= RoundTable.Rounds.Count - 1;

        public void Next()
        {
            _currentIndex++;
        }
    }
}
