using CodeBase.Interfaces.Infrastructure.UI.HUD;

namespace CodeBase.Interfaces.Infrastructure.Factories
{
    public interface IGameFactory
    {
        IHUDRoot CreateHUD();
        void Cleanup();
    }
}