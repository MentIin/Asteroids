using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Services.CameraService
{
    public class CameraService : ILateTickable
    {
        private readonly Camera _camera;
        private Transform _target;

        public CameraService(Camera camera)
        {
            // Store the camera reference if needed
            _camera = camera;
        }

        public void Follow(Transform target)
        {
            _target = target;
        }

        public void LateTick()
        {
            if (_target != null)
            {
                _camera.transform.position = new Vector3(
                    _target.position.x,
                    _target.position.y,
                    _camera.transform.position.z
                );
            }
        }
    }
}