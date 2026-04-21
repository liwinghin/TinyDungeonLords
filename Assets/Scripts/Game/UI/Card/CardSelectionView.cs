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
        public Action<int> OnTargetClicked;

        private Label _timer;

        private Label[] _names = new Label[3];
        private Label[] _descs = new Label[3];
        private VisualElement[] _cards = new VisualElement[3];
        private Button[] _buttons = new Button[3];

        private Button[] _playerBtns = new Button[2];
        private VisualElement _targetRoot;

        private List<BuffData> _cardsData;

        void Awake()
        {
            var root = _root.rootVisualElement;

            _names[0] = root.Q<Label>("name0");
            _names[1] = root.Q<Label>("name1");
            _names[2] = root.Q<Label>("name2");

            _descs[0] = root.Q<Label>("desc0");
            _descs[1] = root.Q<Label>("desc1");
            _descs[2] = root.Q<Label>("desc2");

            _timer = root.Q<Label>("timerLabel");

            for (int i = 0; i < 3; i++)
            {
                _cards[i] = root.Q<VisualElement>($"card{i}");
                _buttons[i] = root.Q<Button>($"btn{i}");

                int index = i;
                _buttons[i].clicked += () => OnCardClicked?.Invoke(index);
            }

            _targetRoot = root.Q<VisualElement>("targetRoot");

            _playerBtns[0] = root.Q<Button>("player0");
            _playerBtns[1] = root.Q<Button>("player1");

            for (int i = 0; i < 2; i++)
            {
                int index = i;
                _playerBtns[i].clicked += () =>
                {
                    OnTargetClicked?.Invoke(index + 1); // 👈 避開自己
                };
            }

            Hide();
        }

        public void Show(List<BuffData> cards)
        {
            var root = _root.rootVisualElement;
            root.style.display = DisplayStyle.Flex;

            ResetCardState();

            _targetRoot.style.display = DisplayStyle.None;

            _cardsData = cards;

            for (int i = 0; i < 3; i++)
            {
                if (i < cards.Count)
                {
                    var card = cards[i];

                    _names[i].text = card.name;
                    _descs[i].text = card.GetDescription();

                    _cards[i].style.display = DisplayStyle.Flex;
                    _cards[i].style.opacity = 1f; // reset
                    _cards[i].SetEnabled(true);

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

        public void ApplySelectionStyle(int selectedIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == selectedIndex)
                {
                    _cards[i].style.opacity = 0.5f;
                }
                else
                {
                    _cards[i].style.borderTopWidth = 5;
                    _cards[i].style.borderBottomWidth = 5;
                    _cards[i].style.borderLeftWidth = 5;
                    _cards[i].style.borderRightWidth = 5;

                    _cards[i].style.borderTopColor = Color.red;
                    _cards[i].style.borderBottomColor = Color.red;
                    _cards[i].style.borderLeftColor = Color.red;
                    _cards[i].style.borderRightColor = Color.red;

                    _buttons[i].SetEnabled(false);
                }
            }
        }

        public void ResetCardState()
        {
            for (int i = 0; i < 3; i++)
            {
                _cards[i].style.opacity = 1f;

                _cards[i].style.borderTopWidth = 0;
                _cards[i].style.borderBottomWidth = 0;
                _cards[i].style.borderLeftWidth = 0;
                _cards[i].style.borderRightWidth = 0;

                _buttons[i].SetEnabled(true);
            }
        }

        public void ShowTarget()
        {
            _targetRoot.style.display = DisplayStyle.Flex;
        }

        public void SetTimer(int t)
        {
            if (_timer != null)
                _timer.text = t > 0 ? t.ToString() : "";
        }

        public void Hide()
        {
            var root = _root.rootVisualElement;
            root.style.display = DisplayStyle.None;
        }

        public BuffData GetSelected(int index)
        {
            return _cardsData[index];
        }
    }
}