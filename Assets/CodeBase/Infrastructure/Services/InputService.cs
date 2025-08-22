using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService
    {
        public InputService(PlayerProvider playerProvider)
        {
        }
        public Vector2 GetMoveAxis()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public float GetRotation()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
                Vector2 direction = (worldPosition - Camera.main.transform.position).normalized;
                return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            return 0f;
        }
    }
}