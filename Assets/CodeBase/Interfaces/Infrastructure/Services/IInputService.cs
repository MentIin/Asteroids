namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IInputService
    {
        float GetMovement();
        float GetRotation();
        bool GetBaseAttack();
        bool GetSpecialAttack();
    }
}