using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Big
{
    public class BigAsteroid : Enemy
    {
        private BigAsteroidModel _model;

        [Inject]
        public void Construct(BigAsteroidModel model)
        {
            _model = model;
        }
    }
}