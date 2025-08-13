using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private ILogService log;

        public SceneLoader(ILogService log) => 
            this.log = log;

        public async UniTask Load(string nextScene)
        {
            var handler = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
            await handler.ToUniTask();
        }
    }
}