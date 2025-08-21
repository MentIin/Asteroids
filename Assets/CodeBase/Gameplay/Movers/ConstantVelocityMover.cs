using CodeBase.Gameplay.Physic;
using UnityEngine;

namespace CodeBase.Gameplay.Movers
{
    public class ConstantVelocityMover : IMover
    {
        private CustomVelocity _velocity;
        public ConstantVelocityMover(CustomVelocity velocity) 
        {
            _velocity = velocity;
        }
        
        public void Tick(Vector2 directionAxis, float deltaTime)
        {
            _velocity.Set(directionAxis);
        }
    }
}