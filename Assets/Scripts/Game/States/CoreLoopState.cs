using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using MessagePipe;

using Temp.Core;
using Temp.Game.Context;
using Temp.Game.Systems;
using Temp.Game.Buff;
using Temp.Game.Events;

namespace Temp.Game.States
{
    public class CoreLoopState : IGameState
    {
        private readonly GameStateMachine _fsm;
        private readonly GameStateFactory _factory;
        private readonly RoundManager _roundManager;
        private readonly GameContext _context;
        private readonly GameTimer _timer;
        private readonly CardSystem _cardSystem;

        private readonly IPublisher<CardSelectionRequest> _requestPub;
        private readonly ISubscriber<CardSelectedEvent> _resultSub;

        public CoreLoopState(
            GameStateMachine fsm,
            GameStateFactory factory,
            RoundManager roundManager,
            GameContext context,
            CardSystem cardSystem,
            IPublisher<CardSelectionRequest> requestPub,
            ISubscriber<CardSelectedEvent> resultSub,
            GameTimer timer)
        {
            _fsm = fsm;
            _factory = factory;
            _roundManager = roundManager;
            _context = context;
            _cardSystem = cardSystem;
            _requestPub = requestPub;
            _resultSub = resultSub;
            _timer = timer;
        }

        public async UniTask Enter()
        {
            Debug.Log("=== CoreLoop ===");

            // =========================
            // 1. 卡片選擇（UI + MessagePipe）
            // =========================
            Debug.Log("=== Card Phase ===");

            BuffData selected = null;

            // 1️⃣ 抽卡（給玩家）
            var cards = _cardSystem.DrawCards(3);

            // 2️⃣ 顯示 UI
            _requestPub.Publish(new CardSelectionRequest
            {
                PlayerId = 0,
                Cards = cards
            });

            // 3️⃣ 訂閱玩家選擇
            using var sub = _resultSub.Subscribe(e =>
            {
                selected = e.SelfCard;
            });

            // 4️⃣ 啟動 Timer（5秒）
            var timerTask = _timer.Start(5);

            // 5️⃣ 等待「選擇 or timeout」
            await UniTask.WhenAny(
                UniTask.WaitUntil(() => selected != null),
                timerTask);

            // 6️⃣ 如果沒選 → 隨機
            if (selected == null)
            {
                selected = cards[Random.Range(0, cards.Count)];
                Debug.Log("Timeout → Random Pick");
            }

            // 7️⃣ 套用 Buff
            var runtime = _context.PlayerRuntimes[0];
            runtime.Buffs.AddBuff(selected);

            Debug.Log($"Selected: {selected.name}");

            // 8️⃣ 隱藏 UI（用 event 或直接 call）
            _requestPub.Publish(new CardSelectionRequest
            {
                PlayerId = -1, // 👉 特殊值 = Hide
                Cards = null
            });

            // =========================
            // 2. 戰鬥
            // =========================
            Debug.Log("=== Battle Phase ===");

            foreach (var player in _context.Players)
            {
                var rt = _context.PlayerRuntimes[player.Id];

                rt.Stats.Reset();

                // 套用 Buff
                foreach (var buff in rt.Buffs.Buffs)
                {
                    buff.Apply(rt.Stats);
                }

                Debug.Log($"Player {player.Id} ATK = {rt.Stats.Attack} DEF = {rt.Stats.Defense}");
            }

            await UniTask.Delay(2000);

            // =========================
            // 3. 下一回合
            // =========================
            _roundManager.Next();

            await _fsm.ChangeState(_factory.Create<RoundState>());
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}