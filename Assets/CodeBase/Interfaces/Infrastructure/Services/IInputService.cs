namespace CodeBase.Gameplay.Services.InputService
{
    public interface IInputService
    {
        float GetMovement();
        float GetRotation();
        bool GetBaseAttack();
        bool GetSpecialAttack();
    }
}