using Temp.Game.Buff;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace Temp.Game.UI.Card
{
    public class CardSelectionView : MonoBehaviour
    {
        [SerializeField] UIDocument _root;
        public Action<int> OnCardClicked;

        private Label _timer;
        private Label[] _names = new Label[3];
        private Label[] _descs = new Label[3];
        private VisualElement[] _cards = new VisualElement[3];

        private List<BuffData> _cardsData;

        void Awake()
        {
            var root = _root.GetComponent<UIDocument>().rootVisualElement;

            _cards[0] = root.Q<VisualElement>("card0");
            _cards[1] = root.Q<VisualElement>("card1");
            _cards[2] = root.Q<VisualElement>("card2");

            _names[0] = root.Q<Label>("name0");
            _names[1] = root.Q<Label>("name1");
            _names[2] = root.Q<Label>("name2");

            _descs[0] = root.Q<Label>("desc0");
            _descs[1] = root.Q<Label>("desc1");
            _descs[2] = root.Q<Label>("desc2");

            _timer = root.Q<Label>("timerLabel");

            for (int i = 0; i < 3; i++)
            {
                int index = i;

                _cards[i].RegisterCallback<ClickEvent>(_ =>
                {
                    OnCardClicked?.Invoke(index);
                });
            }
            Hide();
        }
        public void Show(List<BuffData> cards)
        {
            var root = _root.GetComponent<UIDocument>().rootVisualElement;
            root.style.display = DisplayStyle.Flex;

            _cardsData = cards;

            for (int i = 0; i < 3; i++)
            {
                if (i < cards.Count)
                {
                    var card = cards[i];

                    _names[i].text = card.name;

                    _descs[i].text = card.GetDescription();

                    _cards[i].style.display = DisplayStyle.Flex;

                    switch (card.Rarity)
                    {
                        case BuffRarity.Common:
                            _names[i].style.color = Color.white;
                            break;
                        case BuffRarity.Rare:
                            _names[i].style.color = Color.blue;
                            break;
                        case BuffRarity.Epic:
                            _names[i].style.color = Color.magenta;
                            break;
                    }
                }
                else
                {
                    _cards[i].style.display = DisplayStyle.None;
                }

            }
        }

        public void SetTimer(int t)
        {
            if (_timer != null)
                _timer.text = t > 0 ? t.ToString() : "";
        }

        public void Hide()
        {
            var root = _root.GetComponent<UIDocument>().rootVisualElement;
            root.style.display = DisplayStyle.None;
        }

        public BuffData GetSelected(int index)
        {
            return _cardsData[index];
        }
    }
}