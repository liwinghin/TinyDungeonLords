using System.Collections.Generic;

namespace Temp.Game.Buff
{
    public class BuffContainer
    {
        public List<BuffData> Buffs = new();

        public void AddBuff(BuffData buff)
        {
            Buffs.Add(buff);
        }
    }
}