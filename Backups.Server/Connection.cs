using System;
using System.Net;
using System.Net.Sockets;

namespace Backups.Server
{
    public class Connection : IDisposable
    {
        private TcpListener _listener;
        private const int _buffSize = 1024;
        private NetworkStream _networkStream;
        
        public Connection(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
            _listener.Start();
            _networkStream = _listener.AcceptTcpClient().GetStream();
        }

        public BytesData GetData()
        {
            byte[] buffer = new byte[_buffSize];
            int bytesRead = _networkStream.Read(buffer, 0, buffer.Length);
            return new BytesData(buffer, bytesRead);
        }

        public void SendData(BytesData data)
        {
            _networkStream.Write(data.Bytes);
        }

        public void Dispose() {
            _networkStream.Dispose();
            _listener.Stop();
        }
    }
}