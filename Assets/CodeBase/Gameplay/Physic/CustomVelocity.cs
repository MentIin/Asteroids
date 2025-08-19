using UnityEngine;

namespace CodeBase.Gameplay.Physic
{
    public class CustomVelocity
    {
        private readonly Rigidbody2D _rigidbody;
        private Vector2 _velocity;

        public CustomVelocity(Rigidbody2D targetRigidbody)
        {
            _rigidbody = targetRigidbody;
            _velocity = Vector2.zero;
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
                _rigidbody.position += _velocity * deltaTime;
                _velocity *= Mathf.Pow(0.55f, deltaTime);
            }
        }

        public void HandleCollision(Collision2D other)
        {
            if (other.relativeVelocity.sqrMagnitude > Mathf.Epsilon)
            {
                _velocity -= other.relativeVelocity * 0.5f;
            }
        }
    }
}