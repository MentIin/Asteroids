using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy
    {
        private UfoModel _model;

        [Inject]
        public void Construct(UfoModel model)
        {
            _model = model;
        }
    }
}