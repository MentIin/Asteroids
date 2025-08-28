using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class PCInputStrategy : IInputStrategy
    {
        public float GetMovement() => Input.GetAxis("Vertical");

        public float GetRotation() => -Input.GetAxis("Horizontal");

        public bool GetBaseAttack() => Input.GetMouseButtonDown(0);

        public bool GetSkill() => Input.GetMouseButtonDown(1);
    }
}