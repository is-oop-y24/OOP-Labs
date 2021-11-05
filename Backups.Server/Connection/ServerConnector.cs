using System.Net;
using System.Net.Sockets;

namespace Backups.Server
{
    public class ServerConnector : IConnector
    {
        private TcpListener _listener;

        public ServerConnector(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
            _listener.Start();
        }
        
        public void Dispose()
        {
            _listener.Stop();
        }

        public NetworkStream GetStream()
        {
            return _listener.AcceptTcpClient().GetStream();
        }
    }
}