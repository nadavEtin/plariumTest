using Plarium.Assets.GameCore.ScriptableObjects;
using Plarium.GameCore.UI;
using UnityEngine;
using VContainer.Unity;
using Plarium.Assets.GameCore.Events;
using Plarium.Assets.GameCore;
using Plarium.Assets.Shapes;
using VContainer;

namespace Plarium.Assets.GameCore.VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetRefs _assetRefs;
        [SerializeField] private Keywords _keywords;
        [SerializeField] private ShapesManager _shapesManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.RegisterInstance<IKeywords, Keywords>(_keywords);

            builder.RegisterEntryPoint<GameDirector>(Lifetime.Singleton);
            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<UIManager>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterComponent(_shapesManager).AsImplementedInterfaces();
        }
    }
}


