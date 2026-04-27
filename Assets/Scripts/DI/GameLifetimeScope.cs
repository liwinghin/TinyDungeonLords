using System.Collections.Generic;
using UnityEngine;

using VContainer;
using VContainer.Unity;
using MessagePipe;
using Temp.Game.Events;
using Temp.Game.Player;
using Temp.Game.Systems;
using Temp.Game.States;
using Temp.Game.Context;
using Temp.Game.Buff;
using Temp.Core;
using Temp.Game.UI.Card;
using Temp.Game.Combat;

namespace Temp.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private List<BuffData> cardPool;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe(/* global = true */);

            // UI関連
            builder.RegisterComponentInHierarchy<CardSelectionPresenter>();
            builder.RegisterComponentInHierarchy<CardSelectionView>();

            // Event関連
            builder.RegisterMessageBroker<PlayerSelectDungeonEvent>(options);
            builder.RegisterMessageBroker<CardSelectionRequest>(options);
            builder.RegisterMessageBroker<CardSelectedEvent>(options);
            builder.RegisterEntryPoint<CardResolveSystem>();

            // System関連
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<GameLoop>(Lifetime.Singleton);
            builder.Register<PlayerManager>(Lifetime.Singleton);
            builder.Register<VotingSystem>(Lifetime.Singleton);
            builder.Register<CardSystem>(Lifetime.Singleton);
            builder.Register<GameTimer>(Lifetime.Singleton);
            builder.Register<RoundManager>(Lifetime.Singleton);
            builder.Register<GameStateFactory>(Lifetime.Singleton);

            // State関連
            builder.Register<GameStartState>(Lifetime.Transient);
            builder.Register<RoundState>(Lifetime.Transient);
            builder.Register<CoreLoopState>(Lifetime.Transient);
            builder.Register<EventState>(Lifetime.Transient);
            builder.Register<MidBossState>(Lifetime.Transient);
            builder.Register<FinalBossState>(Lifetime.Transient);
            builder.Register<StartRoundState>(Lifetime.Transient);

            // Player関連
            builder.Register<GameContext>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameEntryPoint>();
            builder.RegisterInstance(cardPool);

            // Enemy
            builder.Register<MonsterModifier>(Lifetime.Singleton);
            builder.Register<MonsterFactory>(Lifetime.Singleton);
            builder.Register<MonsterSpawnSystem>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<MonsterSpawner>();

        }
    }
}