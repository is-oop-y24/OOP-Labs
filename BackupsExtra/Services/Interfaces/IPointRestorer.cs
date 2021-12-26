using Backups;

namespace BackupsExtra.Services.Services
{
    public interface IPointRestorer
    {
        void RestoreThePoint(RestorePoint restorePoint);
    }
}