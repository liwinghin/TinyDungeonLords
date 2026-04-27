using MessagePipe;
using System;
using Temp.Game.Buff;
using Temp.Game.Combat;
using Temp.Game.Events;
using UnityEngine;
using VContainer.Unity;

namespace Temp.Game.Systems
{
    public class CardResolveSystem : IStartable, IDisposable
    {
        private readonly ISubscriber<CardSelectedEvent> _sub;
        private readonly MonsterModifier _modifier; 
        private IDisposable _disposable;

        public CardResolveSystem(
            ISubscriber<CardSelectedEvent> sub,
            MonsterModifier modifier)
        {
            _sub = sub;
            _modifier = modifier;
        }

        public void Start()
        {
            _disposable = _sub.Subscribe(OnCardSelected);
            Debug.Log("CardResolveSystem started");
        }

        private void OnCardSelected(CardSelectedEvent e)
        {
            Debug.Log("CardResolveSystem received");

            foreach (var card in e.OthersCards)
            {
                _modifier.AddModifier(e.TargetPlayerId, card);
                Debug.Log($"Add Modifier to {e.TargetPlayerId}");
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
