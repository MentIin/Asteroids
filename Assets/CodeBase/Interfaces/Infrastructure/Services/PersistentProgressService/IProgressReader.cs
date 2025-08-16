namespace CodeBase.Interfaces.Infrastructure.Services.PersistentProgressService
{
    public interface IProgressReader
    {
        void LoadProgress(Data.PlayerProgress progress);
    }
}