using CodeBase.Gameplay.Enemies.Asteroids.Big;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroid : Enemy
    {
        private SmallAsteroidModel _model;

        [Inject]
        public void Construct(SmallAsteroidModel model)
        {
            _model = model;
        }
    }
}