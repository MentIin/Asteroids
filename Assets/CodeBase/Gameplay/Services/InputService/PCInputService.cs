using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Gameplay.Services.InputService
{
    public class PCInputService : IInputService
    {
        private readonly ICameraService _cameraService;
        private readonly PlayerProvider _playerProvider;
        private Vector2 _moveAxis;

        public PCInputService(ICameraService cameraService, PlayerProvider playerProvider)
        {
            _cameraService = cameraService;
            _playerProvider = playerProvider;
        }
        public Vector2 GetMoveAxis()
        {
            _moveAxis.x = Input.GetAxis("Horizontal");
            _moveAxis.y = Input.GetAxis("Vertical");
            return  _moveAxis;
        }

        public float GetRotation()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 playerScreenPosition = _cameraService.Camera.WorldToScreenPoint(_playerProvider.Player.transform.position);
            Vector2 direction = (mousePosition - playerScreenPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return angle;
        }

        public bool GetBaseAttack()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}