using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Backups.FileSystem;
using Backups.Server.Tools;
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
            serviceCollection.AddSingleton(typeof(IOperationFactory), new OperationFactory(serviceCollection));
            _services = serviceCollection.BuildServiceProvider();
            
            return this;
        }

        public void Run()
        {
            const int buffSize = 1024;
            try
            {
                _listener.Start();
                var logger = new Logger();
                while (true)
                {
                    var requestString = new StringBuilder();
                    using TcpClient client = _listener.AcceptTcpClient();
                    using NetworkStream networkStream = client.GetStream();
                    byte[] buffer = new byte[buffSize];
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    logger.Log($"{bytesRead} bytes read.");
                    requestString.Append(Encoding.Default.GetString(buffer, 0, bytesRead));
                    logger.Log(requestString.ToString());

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new ServerExceptionConverter());
                    Request request = JsonSerializer.Deserialize<Request>(requestString.ToString(), options);
                    logger.Log($"Request deserialized. Command type {request.RequestType}");
                    IOperation operation = _services.GetService<IOperationFactory>().GetOperation(request);

                    Response response = operation.Execute();
                    if (response.ResponseCode == ResponseCode.Error)
                        logger.Log($"Error: {response.ResponseData.Exception}");
                    string responseString = JsonSerializer.Serialize(response, options);
                    logger.Log("Response serialized.");
                    logger.Log(responseString);
                    byte[] responseBytes = Encoding.Default.GetBytes(responseString);
                    networkStream.Write(responseBytes);
                    logger.Log("Response sent.\n");
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