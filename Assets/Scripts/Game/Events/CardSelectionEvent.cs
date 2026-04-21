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

    public class CardSelectedEvent
    {
        public int PlayerId;
        public BuffData SelfCard;
        public List<BuffData> OthersCards;
        public int TargetPlayerId;
    }
}