using UnityEngine;

namespace CodeBase.Gameplay.Services.InputService
{
    public interface IInputService
    {
        Vector2 GetMoveAxis();
        float GetRotation();
        bool GetBaseAttack();
    }
}