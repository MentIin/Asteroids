using UnityEngine;

namespace CodeBase.Data
{
    public class TransformData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity;
        public Vector2 Direction => 
            new Vector2(Mathf.Cos(Rotation * Mathf.Deg2Rad), Mathf.Sin(Rotation * Mathf.Deg2Rad));
        
        public TransformData(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.zero;
            Rotation = 0f;
            AngularVelocity = 0f;
        }

        public TransformData(Vector2 newPosition, Vector2 newVelocity, float newRotation)
        {
            Position = newPosition;
            Velocity = newVelocity;
            Rotation = newRotation;
            AngularVelocity = 0f;
        }
    }
}