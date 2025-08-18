using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyFactory _enemyFactory;

        public GameplayEntryPoint(PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
        }
        public void Initialize()
        {
            _playerFactory.CreatePlayer();
        }
    }
}