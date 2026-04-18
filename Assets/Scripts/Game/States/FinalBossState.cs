using Cysharp.Threading.Tasks;
using UnityEngine;
using Temp.Core;
using Temp.Game.Constants;

namespace Temp.Game.States
{
    public class FinalBossState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly RoundManager _roundManager;

        public FinalBossState(
            GameStateMachine fsm,
            GameStateFactory factory,
            RoundManager roundManager)
        {
            _fsm = fsm;
            _factory = factory;
            _roundManager = roundManager;
        }

        public async UniTask Enter()
        {
            Debug.Log("=== FINAL BOSS ===");

            // =========================
            //1. Boss（之後改成 Factory / Addressables）
            // =========================
            SpawnFinalBoss();

            // =========================
            // 2. 戰鬥（55秒 或 Boss死亡）
            // =========================
            Debug.Log("Final Battle Start");

            bool bossDefeated = false;

            var battleTask = SimulateBattle(() =>
            {
                bossDefeated = true;
            });

            var timeoutTask = UniTask.Delay(GameTimeConstants.BattlePhaseDuration);

            await UniTask.WhenAny(battleTask, timeoutTask);

            // =========================
            // 3. 結果
            // =========================
            if (bossDefeated)
            {
                Debug.Log("Boss Defeated! You Win!");
            }
            else
            {
                Debug.Log("Time Up! You Lose!");
            }

            // =========================
            //  4. GameEnd
            // =========================
            await UniTask.Delay(GameTimeConstants.PreparationPhaseDuration);

            Debug.Log("=== GAME END ===");

            // 之後可以進 ResultState
            // await _fsm.ChangeState(_factory.Create<ResultState>());
        }

        public UniTask Exit() => UniTask.CompletedTask;

        // =========================
        // 戦闘（仮）
        // =========================
        private async UniTask SimulateBattle(System.Action onBossDead)
        {
            await UniTask.Delay(GameTimeConstants.BossSimulationDelay); // 假設30秒打死

            onBossDead?.Invoke();
        }

        private void SpawnFinalBoss()
        {
            Debug.Log("Spawn Final Boss");
        }
    }
}