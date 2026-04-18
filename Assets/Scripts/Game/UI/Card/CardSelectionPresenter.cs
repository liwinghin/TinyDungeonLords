using UnityEngine;
using MessagePipe;
using VContainer;
using R3;
using System.Collections.Generic;
using Temp.Game.Events;
using Temp.Game.Buff;

namespace Temp.Game.UI.Card
{
    public class CardSelectionPresenter : MonoBehaviour
    {
        [SerializeField] private CardSelectionView view;

        private ISubscriber<CardSelectionRequest> _requestSub;
        private IPublisher<CardSelectedEvent> _resultPub;

        private GameTimer _timer;

        private List<BuffData> _currentCards;

        [Inject]
        public void Construct(
            ISubscriber<CardSelectionRequest> requestSub,
            IPublisher<CardSelectedEvent> resultPub,
            GameTimer timer)
        {
            _requestSub = requestSub;
            _resultPub = resultPub;
            _timer = timer;
        }

        void Start()
        {
            _requestSub.Subscribe(OnRequest);

            view.OnCardClicked += OnClicked;

            _timer.Time.Subscribe(t =>
            {
                view.SetTimer(t);
            });
        }

        private void OnRequest(CardSelectionRequest req)
        {
            if (req.PlayerId == -1)
            {
                view.Hide();
                return;
            }

            _currentCards = req.Cards;
            view.Show(req.Cards);
        }

        private void OnClicked(int index)
        {
            var selected = _currentCards[index];

            _resultPub.Publish(new CardSelectedEvent
            {
                PlayerId = 0,
                Selected = selected
            });

            view.Hide();
            Debug.Log("Hide UI called");
        }
    }
}