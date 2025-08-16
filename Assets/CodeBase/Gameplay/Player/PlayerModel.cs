using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerModel
    {
        public TransformData TransformData;
        public int Health = 3;
        
        public bool IsInvulnerable { get; private set; }
        public float InvulnerabilityTimer { get; private set; }
        private Config _config;

        public PlayerModel(Vector2 position)
        {
            TransformData = new TransformData(position);
            
            //TODO: вынести в конфиг
            _config = new Config
            {
                Acceleration = 5f,
                MaxSpeed = 10f,
                RotationSpeed = 180f,
                InvulnerabilityDuration = 2f,
                InvulnerabilityCooldown = 5f
            };
        }
        
        public void UpdateTransform(Vector2 acceleration, float rotationInput, float deltaTime)
        {
            Vector2 newVelocity = TransformData.Velocity + acceleration * (_config.Acceleration * deltaTime);
            //newVelocity = Vector2.ClampMagnitude(newVelocity, _config.MaxSpeed);
        
            Vector2 newPosition = TransformData.Position + newVelocity * deltaTime;
        
            float newRotation = TransformData.Rotation + rotationInput * _config.RotationSpeed * deltaTime;
        
            TransformData = new TransformData(newPosition, newVelocity, newRotation);
        }
        public void UpdateInvulnerability(float deltaTime)
        {
            if (IsInvulnerable)
            {
                InvulnerabilityTimer -= deltaTime;
                if (InvulnerabilityTimer <= 0)
                {
                    IsInvulnerable = false;
                }
            }
        }

        public void ApplyDamage()
        {
            if (!IsInvulnerable)
            {
                Health--;
                IsInvulnerable = true;
                InvulnerabilityTimer = _config.InvulnerabilityDuration;
            }
        }
    }

    public struct Config
    {
        public float Acceleration;
        public float MaxSpeed;
        public float RotationSpeed;
        public float InvulnerabilityDuration;
        public float InvulnerabilityCooldown;
    }
    
}