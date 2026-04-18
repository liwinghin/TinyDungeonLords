using System.Collections.Generic;

namespace Temp.Game.States
{
    public enum RoundType
    {
        Start,      // 第1回合
        CoreLoop,   // 5秒卡片 + 55秒戰鬥
        Event,      // 投票事件
        MidBoss,
        FinalBoss
    }

    public class RoundConfig
    {
        public int Round;
        public RoundType Type;
    }

    public static class RoundTable
    {
        public static List<RoundConfig> Rounds = new()
        {
            new RoundConfig { Round = 1, Type = RoundType.Start },
            new RoundConfig { Round = 2, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 3, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 4, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 5, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 6, Type = RoundType.Event },
            new RoundConfig { Round = 7, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 8, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 9, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 10, Type = RoundType.MidBoss },
            new RoundConfig { Round = 11, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 12, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 13, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 14, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 15, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 16, Type = RoundType.Event },
            new RoundConfig { Round = 17, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 18, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 19, Type = RoundType.CoreLoop },
            new RoundConfig { Round = 20, Type = RoundType.FinalBoss },
        };
    }
}