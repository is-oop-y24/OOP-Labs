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
        private IPAddress _ip;
        private int _port;
        
        private IFileRepository _repository;
        private IBackupService _backupService;
        private IOperationFactory _operationFactory;
        private ILogger _logger;
        private IBytesDecoder _decoder;
        
        public Server(string ipString, int port)
        {
            _ip = IPAddress.Parse(ipString);
            _port = port;

            var options = new JsonSerializerOptions();
            options.Converters.Add(new ServerExceptionConverter());
            _decoder = new BytesDecoder(options);
        }

        public IFileRepository FileRepository { init => _repository = value; }
        public IBackupService BackupService { init => _backupService = value; }
        public IOperationFactory OperationFactory { init => _operationFactory = value; }
        public ILogger Logger { init => _logger = value; }

        public void Run()
        {
            while (true)
            {
                using var connection = new Connection(new ServerConnector(_ip, _port));
                BytesData requestData = connection.GetData();
                Request request = _decoder.Decode<Request>(requestData);
                IOperation operation = _operationFactory.GetOperation(request);
                Response response = operation.Execute();
                BytesData responseData = _decoder.Encode(response);
                connection.SendData(responseData);
            }
        }
    }
}