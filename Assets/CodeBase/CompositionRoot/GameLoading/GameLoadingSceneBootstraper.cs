using Zenject;

namespace CodeBase.GameLoading
{
    public class GameLoadingSceneBootstraper : IInitializable
    {
        public GameLoadingSceneBootstraper()
        {
            
        }

        public void Initialize()
        {
            // log.Log("Start loading scene bootstraping");
            //
            // sceneStateMachine.RegisterState(statesFactory.Create<LoadPlayerProgressState>());
            // sceneStateMachine.RegisterState(statesFactory.Create<FinishGameLoadingState>());
            //
            // log.Log("Finish loading scene bootstraping");
            //
            // // go to the first scene state
            // sceneStateMachine.Enter<LoadPlayerProgressState>();
        }
    }
}