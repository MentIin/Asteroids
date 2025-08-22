using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Gameplay.Services.InputService
{
    public class PCInputService : IInputService
    {
        public float GetMovement()
        {
            return Input.GetAxis("Vertical");
        }

        public float GetRotation()
        {
            return -Input.GetAxis("Horizontal");
        }

        public bool GetBaseAttack()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}