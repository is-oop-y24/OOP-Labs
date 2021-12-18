namespace Backups.Server
{
    public enum RequestType
    {
        UploadFile,
        ObserveFile,
        MakeRestorePoint,
        CreateJob,
        DeleteJobObject,
        CreateExtraJob,
        RestoreThePoint
    }
}