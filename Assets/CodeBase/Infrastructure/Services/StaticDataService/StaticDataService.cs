using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Data.Stats;
using CodeBase.Data.Stats.Main;
using CodeBase.Data.Tools;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyConfigsPath = "Configs/Gameplay/Enemies";
        
        private Dictionary<EnemyType, EnemyData> _enemyDataDictionary;

        public void Initialize()
        {
            //GenerateMockData();
            LoadEnemyData();
            Debug.Log(ForEnemy(EnemyType.Ufo).Stats.GetStat<HealthStat>().Value);
        }

        private void GenerateMockData()
        {
            _enemyDataDictionary = new Dictionary<EnemyType, EnemyData>();
            Stats stats = new Stats();
            stats.AddStat(new HealthStat(5));
            _enemyDataDictionary[EnemyType.BigAsteroid] = new EnemyData
            {
                PrefabPath = "Prefabs/Enemies/BigAsteroid",
                Type = EnemyType.BigAsteroid,
                Stats = stats
            };
            stats = new Stats();
            stats.AddStat(new HealthStat(55));
            stats.AddStat(new SpeedStat(33));
            _enemyDataDictionary[EnemyType.Ufo] = new EnemyData
            {
                PrefabPath = "Prefabs/Enemies/BigAsteroid",
                Type = EnemyType.BigAsteroid,
                Stats = stats
            };
            
            // save to json

            foreach (var enemyData in _enemyDataDictionary)
            {
                string json = enemyData.Value.ToJson();
                
                // Save to Resources folder 
                System.IO.File.WriteAllText($"Assets/Resources/{EnemyConfigsPath}/{enemyData.Key}.json", json);
            }

        }

        public PlayerConfig ForPlayer()
        {
            //PlayerConfig playerData = assetProvider.Load<PlayerConfig>("StaticData/PlayerConfig");

            PlayerConfig playerData = new PlayerConfig
            {
                PrefabPath = "Prefabs/Player/Player"
            };
            
            return playerData;
        }

        public EnemyData ForEnemy(EnemyType type)
        {
            return _enemyDataDictionary[type];
        }

        #region DataLoading

        private void LoadEnemyData()
        {
            _enemyDataDictionary = new Dictionary<EnemyType, EnemyData>();
            
            TextAsset[] localConfigText = Resources.LoadAll<TextAsset>(EnemyConfigsPath);
            foreach (TextAsset textAsset in localConfigText)
            {
                EnemyData enemyData = textAsset.text.ToDeserialized<EnemyData>();
                
                _enemyDataDictionary.Add(enemyData.Type, enemyData);
            }
        }

        #endregion
        
    }
}