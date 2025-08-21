using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour, IArenaMember
    {
        private PlayerModel _playerModel;
        public TransformData TransformData => _playerModel.transformData;


        [Inject]
        public void Construct(IInputService inputService, Stats playerStats)
        {
            _playerModel = new PlayerModel(inputService, playerStats);
        }

        private void Update()
        {
            _playerModel.Tick();
        }

        private void OnCollisionEnter2D(Collision2D other) => _playerModel.OnCollisionEnter2D(other);
    }
}