using CodeBase.Interfaces.Infrastructure.Services.PersistentProgressService;

namespace CodeBase.Infrastructure.Services.PlayerProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public Data.PlayerProgress Progress { get; set; }
    }
}