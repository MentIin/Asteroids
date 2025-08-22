using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Enums;
using CodeBase.Data.StaticData;
using CodeBase.Data.Tools;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyConfigsPath = "Configs/Gameplay/Enemies";
        private const string ProjectileConfigsPath = "Configs/Gameplay/Projectiles";
        private const string PlayerConfigPath = "Configs/Gameplay/Player";
        private const string MapConfigPath = "Configs/Gameplay/Map";
        
        private Dictionary<EnemyType, EnemyConfig> _enemyConfigsDictionary;
        private PlayerConfig _playerConfig;
        private MapConfig _mapConfig;

        public void Initialize()
        {
            //GenerateMockData();
            LoadEnemyData();
            LoadPlayerData();
            LoadMapData();
        }

        public PlayerConfig ForPlayer()
        {
            return _playerConfig;
        }
        public MapConfig ForMap()
        {
            return _mapConfig;
        }

        public EnemyConfig ForEnemy(EnemyType type)
        {
            return _enemyConfigsDictionary[type];
        }

        #region DataLoading

        private void LoadEnemyData()
        {
            _enemyConfigsDictionary = new Dictionary<EnemyType, EnemyConfig>();
            
            TextAsset[] localConfigText = Resources.LoadAll<TextAsset>(EnemyConfigsPath);
            foreach (TextAsset textAsset in localConfigText)
            {
                EnemyConfig enemyConfig = textAsset.text.ToDeserialized<EnemyConfig>();
                
                _enemyConfigsDictionary.Add(enemyConfig.Type, enemyConfig);
            }
        }
        private void LoadPlayerData()
        {
            _playerConfig = Resources.Load<TextAsset>(PlayerConfigPath).text.ToDeserialized<PlayerConfig>();
        }
        private void LoadMapData()
        {
            _mapConfig = Resources.Load<TextAsset>(MapConfigPath).text.ToDeserialized<MapConfig>();
        }

        #endregion
        
    }
}