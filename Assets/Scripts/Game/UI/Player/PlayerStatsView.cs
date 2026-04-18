using UnityEngine;
using UnityEngine.UIElements;
using R3;
using Temp.Game.Player;

namespace Temp.Game.UI.Player
{
    public class PlayerStatsView : MonoBehaviour
    {
        private Label _atk;

        public void Bind(PlayerRuntime runtime)
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _atk = root.Q<Label>("atkLabel");

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _atk.text = $"ATK: {runtime.Stats.Attack}Å@DEF: {runtime.Stats.Defense} SKILL: {runtime.Skill1} {runtime.Skill2}";
                });
        }
    }
}