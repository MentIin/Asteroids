using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Movers;
using CodeBase.Gameplay.Physic;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroidModel : ITickable
    {
        private readonly Stats _stats;
        public readonly TransformData transformData;
        private readonly CustomVelocity _velocity;
        private readonly IMover _mover;
        
        private Vector2 _directionAxis;

        public SmallAsteroidModel(Stats stats)
        {
            _stats = stats;
            transformData = new TransformData(Vector2.zero);
            _velocity = new CustomVelocity(transformData);
            _mover = new ConstantVelocityMover(_velocity);
        }


        public void SetMoveDirection(Vector2 dir)
        {
            _directionAxis = dir.normalized;
        }

        public void Tick()
        {
            _mover.Tick(_directionAxis, Time.deltaTime);
            _velocity.Tick(Time.deltaTime);
        }
    }
}