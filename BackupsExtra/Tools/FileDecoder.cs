using System;
using System.IO;
using System.Text;
using Backups;
using Backups.FileSystem;

namespace BackupsExtra
{
    public class FileDecoder : IDisposable
    {
        private MemoryStream _fileDataStream;

        public FileDecoder(BackupFile file)
        {
            _fileDataStream = new MemoryStream(file.Content);
        }

        public bool FinishedReading => _fileDataStream.Position == _fileDataStream.Length;

        public long GetNextLong()
        {
            if (FinishedReading)
                throw new FileSystemException("File is ended");

            byte[] longBuffer = new byte[sizeof(long)];
            _fileDataStream.Read(longBuffer, 0, longBuffer.Length);
            return BitConverter.ToInt32(longBuffer);
        }

        public string GetNextString(long length)
        {
            if (FinishedReading)
                throw new FileSystemException("File is ended");

            byte[] stringBuffer = new byte[length];
            _fileDataStream.Read(stringBuffer, 0, (int)length);
            return Encoding.Default.GetString(stringBuffer);
        }

        public byte[] GetNextByteContent(long bytesCount)
        {
            if (FinishedReading)
                throw new FileSystemException("File is ended");

            byte[] content = new byte[bytesCount];
            _fileDataStream.Read(content, 0, (int)bytesCount);
            return content;
        }

        public void Dispose()
        {
            _fileDataStream.Dispose();
        }
    }
}