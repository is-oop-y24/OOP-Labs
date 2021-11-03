using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Backups.Server
{
    public class BytesData
    {
        public BytesData(byte[] buffer, int bytesCount)
        {
            Array.Copy(buffer, Bytes, bytesCount);
            Size = bytesCount;
        }

        public BytesData(byte[] bytes)
        {
            Array.Copy(bytes, Bytes, bytes.Length);
            Size = bytes.Length;
        }
        
        public byte[] Bytes { get; }
        public int Size { get; }
    }
}