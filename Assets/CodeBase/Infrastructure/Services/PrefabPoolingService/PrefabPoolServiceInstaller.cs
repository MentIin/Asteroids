using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.PrefabPoolingService
{
    public class PrefabPoolServiceInstaller : Installer<PrefabPoolServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, SpecificPrefabPool, SpecificPrefabPool.Factory>();

            Container.BindInterfacesTo<PrefabPoolingService>().AsSingle();
        }
    }
}