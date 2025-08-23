using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Physic;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class UfoModel : ITickable, IInitializable
    {
        public readonly CustomVelocity velocity;
        public readonly TransformData transformData;
        
        private readonly Stats _stats;
        private readonly PlayerProvider _playerProvider;
        private readonly Transform _viewTransform;
        
        private Vector2 _directionAxis;

        public UfoModel(Stats stats, PlayerProvider playerProvider, Transform viewTransform)
        {
            _stats = stats;
            _playerProvider = playerProvider;
            _viewTransform = viewTransform;
            transformData = new TransformData(viewTransform.position);
            velocity = new CustomVelocity(transformData);
        }
        public void Initialize()
        {
            SetMoveDirection(
                _playerProvider.Player.TransformData.Position - (Vector2)_viewTransform.position
            );
        }

        public void Tick()
        {
            velocity.AddForce(_directionAxis * Time.deltaTime);
            velocity.Tick(Time.deltaTime);
        }

        public void SetMoveDirection(Vector2 dir)
        {
            _directionAxis = dir.normalized;
        }
    }
}