using System.Net;
using System.Net.Sockets;
using Backups.Server;

namespace Backups.Client
{
    public class ClientConnector : IConnector
    {
        private TcpClient _client;

        public ClientConnector(string hostname, int port)
        {
            _client = new TcpClient(hostname, port);
        }
        
        public void Dispose()
        {
            _client.Dispose();
        }

        public NetworkStream GetStream()
        {
            return _client.GetStream();
        }
    }
}