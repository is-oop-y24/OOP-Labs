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
        
        private readonly IFileRepository _repository;
        private readonly IBackupService _backupService;
        private readonly IOperationFactory _operationFactory;
        private readonly ILogger _logger;
        private readonly IBytesDecoder _decoder;
        
        public Server(string ipString, int port)
        {
            _ip = IPAddress.Parse(ipString);
            _port = port;
            _decoder = new BytesDecoder();
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
                _logger.Log("Connection set.");
                BytesData requestBytes = connection.GetData();
                _logger.Log($"Bytes read. Bytes count: {requestBytes.Size}.");
                Request request = _decoder.Decode<Request>(requestBytes);
                _logger.Log($"Request decoded. Type is {request.RequestType}.");
                IOperation operation = _operationFactory.GetOperation(request);
                Response response = operation.Execute();
                connection.SendData(_decoder.Encode(response));
            }
        }
    }
}