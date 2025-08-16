using UnityEngine;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IInputService
    {
        Vector2 GetMoveAxis();
        float GetRotation();
    }
}