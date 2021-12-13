namespace BackupsExtra
{
    public interface IExtraBackupService
    {
        ExtraBackupJob CreateExtraJob(ExtraJobBuilder builder);
        void Save(string path);
        void Load(string path);
    }
}