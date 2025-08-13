using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.UI.Overlays;
using CodeBase.UI.PopUps.ErrorPopup;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class PrefabFactoryAsync<TComponent> : IFactory<string, TComponent>
    {
        private IInstantiator instantiator;
        private IAssetProvider assetProvider;

        public PrefabFactoryAsync(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this.instantiator = instantiator;
            this.assetProvider = assetProvider;
        }

        public TComponent Create(string assetKey)
        {
            GameObject prefab = assetProvider.Load<GameObject>(assetKey);
            GameObject newObject = instantiator.InstantiatePrefab(prefab);
            return newObject.GetComponent<TComponent>();
        }
    }
}