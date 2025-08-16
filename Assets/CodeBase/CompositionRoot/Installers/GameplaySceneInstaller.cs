using CodeBase.CompositionRoot.EntryPoints;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEntryPoint();
        }

        private void BindEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
    }
}