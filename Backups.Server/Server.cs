using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Backups.FileSystem;
using Isu;
using Microsoft.Extensions.DependencyInjection;

namespace Backups.Server
{
    public class Server
    {
        private TcpListener _listener;
        private ServiceProvider _services;

        public Server() {}

        public Server Configure()
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            const string localPath = @"F:\Repository";
            const int port = 8888;
            _listener = new TcpListener(localIp, port);

            var repository = new LocalRepository(localPath);
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton(typeof(IFileRepository), repository);
            serviceCollection.AddSingleton(typeof(IBackup), new Backup("", repository));
            serviceCollection.AddScoped(typeof(IOperationFabric), typeof(OperationFabric));
            _services = serviceCollection.BuildServiceProvider();
            
            return this;
        }

        public void Run()
        {
            const int buffSize = 1024;
            try
            {
                _listener.Start();
                while (true)
                {
                    var requestString = new StringBuilder();
                    using TcpClient client = _listener.AcceptTcpClient();
                    using NetworkStream networkStream = client.GetStream();
                    byte[] buffer = new byte[buffSize];
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    requestString.Append(Encoding.Default.GetString(buffer, 0, bytesRead));
                    
                    Request request = JsonSerializer.Deserialize<Request>(requestString.ToString());
                    IOperation operation = _services.GetService<IOperationFabric>().GetOperation(request);

                    Response response = operation.Execute();
                    string responseString = JsonSerializer.Serialize(response);
                    byte[] responseBytes = Encoding.Default.GetBytes(responseString);
                    networkStream.Write(responseBytes);
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