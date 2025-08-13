using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoadingState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly IAssetProvider assetProvider;

        public GameLoadingState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            Debug.Log("GameLoadingState Enter");
            //loadingCurtain.Show();
            
            await sceneLoader.Load(InfrastructureAssetPath.GameLoadingScene);
            
            loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            loadingCurtain.Show();
        }
    }
}