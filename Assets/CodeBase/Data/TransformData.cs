using UnityEngine;

namespace CodeBase.Data
{
    public class TransformData
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Direction => 
            new Vector2(Mathf.Cos(Rotation * Mathf.Deg2Rad), Mathf.Sin(Rotation * Mathf.Deg2Rad));
        
        public TransformData(Vector2 position)
        {
            Position = position;
            Rotation = 0f;
        }

        public TransformData(Vector2 newPosition, Vector2 newVelocity, float newRotation)
        {
            Position = newPosition;
            Rotation = newRotation;
        }
    }
}