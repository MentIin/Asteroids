namespace CodeBase.Interfaces.Infrastructure.Services.PersistentProgressService
{
    public interface IPersistentProgressService
    {
        Data.PlayerProgress Progress { get; set; }
    }
}