using CodeBase.Gameplay.Factories;
using Zenject;

namespace CodeBase.CompositionRoot.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        public GameplayEntryPoint(PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            
            
        }
        public void Initialize()
        {
            
        }
    }
}