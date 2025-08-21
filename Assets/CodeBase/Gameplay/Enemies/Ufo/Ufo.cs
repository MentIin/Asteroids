using CodeBase.Data.StatsSystem.Main;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy
    {
        private UfoModel _model;

        [Inject]
        public void Construct(Stats stats)
        {
            _model = new UfoModel();
        }
    }
}