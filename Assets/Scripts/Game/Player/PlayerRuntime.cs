using UnityEngine;
using Temp.Game.Buff;

namespace Temp.Game.Player
{
    public class PlayerRuntime
    {
        public PlayerStats Stats = new();
        public BuffContainer Buffs = new();

        public string Skill1 = "Skill 1";
        public string Skill2 = "Skill 2";
    }
}