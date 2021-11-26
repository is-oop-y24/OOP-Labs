using System;
using System.Net.Sockets;

namespace Backups.Server
{
    public interface IConnector : IDisposable
    {
        NetworkStream GetStream();
    }
}