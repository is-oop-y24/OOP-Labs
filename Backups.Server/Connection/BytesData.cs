using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

namespace Backups.Server
{
    public class BytesData
    {
        public BytesData(byte[] buffer, int bytesCount)
        {
            Size = bytesCount;
            Bytes = new byte[Size];
            Array.Copy(buffer, Bytes, bytesCount);
        }

        public BytesData(byte[] bytes)
        {
            Size = bytes.Length;
            Bytes = new byte[Size];
            Array.Copy(bytes, Bytes, bytes.Length);
        }

        public byte[] Bytes { get; }
        public int Size { get; }
    }
}