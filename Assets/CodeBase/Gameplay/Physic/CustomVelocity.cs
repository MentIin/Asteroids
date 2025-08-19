using UnityEngine;

namespace CodeBase.Gameplay.Physic
{
    public class CustomVelocity
    {
        private readonly Transform _transform;
        private Vector3 _velocity; // Используем Vector3 вместо Vector2

        public CustomVelocity(Transform targetTransform)
        {
            _transform = targetTransform;
            _velocity = Vector3.zero;
        }

        public void AddForce(Vector2 force)
        {
            _velocity.x += force.x;
            _velocity.y += force.y;
        }

        public void Tick(float deltaTime)
        {
            if (_velocity.sqrMagnitude > Mathf.Epsilon)
            {
                _transform.localPosition += _velocity * deltaTime;
            }
        }
    }
}