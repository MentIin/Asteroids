using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Physic
{
    public class CustomVelocity
    {
        private readonly TransformData _transformData;
        private Vector2 _velocity;
        private float _angularVelocity;

        public CustomVelocity(TransformData transformData)
        {
            _transformData = transformData;
            _velocity = Vector2.zero;
        }

        public void AddForce(Vector2 force)
        {
            _velocity.x += force.x;
            _velocity.y += force.y;
        }
        public void AddAngularForce(float force)
        {
            _angularVelocity += force;
        }

        public void Tick(float deltaTime)
        {
            if (_velocity.sqrMagnitude > Mathf.Epsilon)
            {
                _transformData.Position += _velocity * deltaTime;
                _velocity *= Mathf.Pow(0.8f, deltaTime);
            }
            _transformData.Rotation += _angularVelocity * deltaTime;
            _angularVelocity *= Mathf.Pow(GameConstants.PhysicAngularBump, deltaTime);
        }

        public void Set(Vector2 directionAxis)
        {
            _velocity = directionAxis;
        }
    }
}