using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.CameraService
{
    public class CameraService : ITickable
    {
        private readonly Camera _camera;

        public CameraService(Camera camera)
        {
            // Store the camera reference if needed
            _camera = camera;
        }

        public void Tick()
        {
            // This method is called every frame, implement camera updates here if needed
            // For example, you could update camera position or rotation based on player input or game state
        }
    }
}