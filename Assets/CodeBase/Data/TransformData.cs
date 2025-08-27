using UnityEngine;

namespace CodeBase.Data
{
    public class TransformData
    {
        public Vector2 Position;

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value % 360f;
        }

        public Vector2 Direction => 
            new Vector2(Mathf.Cos(Rotation * Mathf.Deg2Rad), Mathf.Sin(Rotation * Mathf.Deg2Rad));
        public Quaternion RotationQuaternion => 
            Quaternion.Euler(0f, 0f, Rotation);
        
        private float _rotation;

        public TransformData()
        {
            Position = Vector2.zero;
            Rotation = 0f;
        }
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
        public TransformData(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.eulerAngles.z;
        }
    }
}