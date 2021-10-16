using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Backups.FileSystem;
using Isu;

namespace Backups.Server
{
    public class Server
    {
        private TcpListener _listener;
        private IFileRepository _repository;

        public Server() {}

        public Server Configure()
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            const string localPath = @"F:\Repository";
            const int port = 8888;
            _listener = new TcpListener(localIp, port);
            _repository = new LocalRepository(localPath);
            return this;
        }

        public void Run()
        {
            try
            {
                _listener.Start();
                while (true)
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    Span<byte> request = null;
                    stream.Read(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}