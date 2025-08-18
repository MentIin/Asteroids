using CodeBase.Gameplay.EntryPoints;
using CodeBase.Gameplay.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();

            BindEntryPoint();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().ToSelf().AsSingle();
            Container.Bind<EnemyFactory>().ToSelf().AsSingle();
        }

        private void BindEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
    }
}