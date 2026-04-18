using R3;
using UnityEngine;
using Temp.Game.Player;

namespace Temp.Game.UI.Player
{
    public class PlayerStatsPresenter : MonoBehaviour
    {
        public void Bind(PlayerRuntime runtime)
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    Debug.Log($"ATK: {runtime.Stats.Attack} {runtime.Stats.Defense} {runtime.Skill1} {runtime.Skill2}");
                });
        }
    }
}