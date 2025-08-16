namespace CodeBase.Interfaces.Infrastructure.Services.PersistentProgressService
{
    public interface IProgressSaver : IProgressReader
    {
        void UpdateProgress(Data.PlayerProgress progress);
    }
}