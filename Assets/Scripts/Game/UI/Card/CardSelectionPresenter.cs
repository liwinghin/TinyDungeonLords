using UnityEngine;
using MessagePipe;
using VContainer;
using R3;
using System.Collections.Generic;
using System.Linq;
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
        private BuffData _selectedCard;
        private int _selectedIndex = -1;

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

            view.OnCardClicked += OnCardClicked;
            view.OnTargetClicked += OnTargetClicked;

            _timer.Time.Subscribe(t =>
            {
                view.SetTimer(t);
            });
        }

        public void Show(List<BuffData> cards)
        {
            _currentCards = cards;
            _selectedCard = null;
            _selectedIndex = -1;
            view.Show(cards);
        }


        private void OnRequest(CardSelectionRequest req)
        {
            if (req.PlayerId == -1)
            {
                view.Hide();
                return;
            }

            _currentCards = req.Cards;
            _selectedCard = null;
            _selectedIndex = -1;

            view.Show(req.Cards);
        }

        private void OnCardClicked(int index)
        {
            if (_currentCards == null) return;

            _selectedIndex = index;
            _selectedCard = _currentCards[index];

            view.ApplySelectionStyle(index);
            view.ShowTarget();             
        }

        private void OnTargetClicked(int targetPlayerId)
        {
            if (_selectedCard == null || _selectedIndex < 0) return;

            var others = _currentCards
                .Where((_, index) => index != _selectedIndex)
                .ToList();

            _resultPub.Publish(new CardSelectedEvent
            {
                PlayerId = 0,
                SelfCard = _selectedCard,
                OthersCards = others,
                TargetPlayerId = targetPlayerId
            });

            view.Hide();
            Debug.Log("Card + Target Selected");
        }
    }
}
