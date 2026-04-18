using UnityEngine;
using System.Collections.Generic;
using Temp.Game.Player;

namespace Temp.Game.Context
{
    public class GameContext
    {
        public int CurrentRound;
        public int CurrentDungeon;

        public List<Player.Player> Players = new();

        // プレイヤー
        public Dictionary<int, PlayerRuntime> PlayerRuntimes = new();
    }
}