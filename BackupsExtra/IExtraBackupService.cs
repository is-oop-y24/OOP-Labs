namespace BackupsExtra
{
    public interface IExtraBackupService
    {
        ExtraBackupJob CreateExtraJob(ExtraJobBuilder builder);
        ExtraBackupJob FindJob(string jobName);
        void Save(string path);
        void Load(string path);
    }
}