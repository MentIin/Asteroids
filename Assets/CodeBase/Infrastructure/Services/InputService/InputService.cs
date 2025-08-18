using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService
    {
        public Vector2 GetMoveAxis()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public float GetRotation()
        {
            throw new System.NotImplementedException();
        }
    }
}