using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Physic;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids
{
    public class AsteroidModel : ITickable
    {
        public readonly CustomVelocity velocity;
        public readonly TransformData transformData;
        
        private readonly Stats _stats;
        private readonly Transform _viewTransform;
        
        private Vector2 _directionAxis;

        public AsteroidModel(Stats stats, Transform viewTransform)
        {
            _stats = stats;
            _viewTransform = viewTransform;
            transformData = new TransformData(viewTransform);
            velocity = new CustomVelocity(transformData);
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