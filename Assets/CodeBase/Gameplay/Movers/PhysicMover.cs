using CodeBase.Gameplay.Physic;
using UnityEngine;

namespace CodeBase.Gameplay.Movers
{
    public class PhysicMover : IMover
    {
        private CustomVelocity _velocity;
        public PhysicMover(CustomVelocity velocity) 
        {
            _velocity = velocity;
        }
        
        public void Tick(Vector2 directionAxis, float deltaTime)
        {
            _velocity.AddForce(directionAxis * deltaTime);
        }
    }
}