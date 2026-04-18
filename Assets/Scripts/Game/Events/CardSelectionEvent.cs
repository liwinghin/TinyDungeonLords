using UnityEngine;
using System.Collections.Generic;
using Temp.Game.Buff;

namespace Temp.Game.Events
{
    public struct CardSelectionRequest
    {
        public int PlayerId;
        public List<BuffData> Cards;
    }

    public struct CardSelectedEvent
    {
        public int PlayerId;
        public BuffData Selected;
    }
}