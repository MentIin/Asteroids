using CodeBase.Data;
using CodeBase.Data.StatsSystem;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Physic;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids
{
    public class AsteroidModel : ITickable
    {
        public readonly CustomVelocity velocity;
        public readonly TransformData transformData;
        
        private readonly Stats _stats;
        
        private Vector2 _directionAxis;

        public AsteroidModel(Stats stats, Transform viewTransform)
        {
            _stats = stats;
            transformData = new TransformData(viewTransform);
            velocity = new CustomVelocity(transformData);
        }

        public void Tick()
        {
            velocity.AddForce(_directionAxis * (Time.deltaTime * _stats.GetStat<SpeedStat>().Value));
            velocity.Tick(Time.deltaTime);
        }
    }
}