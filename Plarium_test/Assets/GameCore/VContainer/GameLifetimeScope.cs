using Plarium.Assets.GameCore.ScriptableObjects;
using GameCore;
using GameCore.Json;
using GameCore.ScriptableObjects;
using GameCore.ServerComs;
using Plarium.GameCore.UI;
using UnityEngine;
using VContainer.Unity;
using Plarium.Assets.GameCore.Events;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetRefs _assetRefs;
        [SerializeField] private Keywords _keywords;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.RegisterInstance<IKeywords, Keywords>(_keywords);

            builder.RegisterEntryPoint<GameDirector>();

            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<JsonSerialization>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UWebRequest>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UIManager>(Lifetime.Scoped).AsImplementedInterfaces();
            //builder.Register<InfoBtn>(Lifetime.Scoped);
        }
    }
}


