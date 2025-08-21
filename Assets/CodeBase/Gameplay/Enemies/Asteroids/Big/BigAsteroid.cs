using CodeBase.Data.StatsSystem.Main;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Big
{
    public class BigAsteroid : Enemy
    {
        private BigAsteroidModel _model;

        [Inject]
        public void Construct(Stats stats)
        {
            _model = new BigAsteroidModel(stats);
        }
    }
}