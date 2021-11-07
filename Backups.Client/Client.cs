using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Backups.FileSystem;
using Backups.Server;
using Backups.Server.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Backups.Client
{
    public class Client
    {
        private string _hostname;
        private int _port;
        private readonly ILogger _logger;
        private readonly IBytesDecoder _decoder;
            
        public Client(string hostname, int port, ILogger logger)
        {
            _hostname = hostname;
            _port = port;
            _logger = logger;
            _decoder = new BytesDecoder();
        }


        public Response MakeRequest(Request request)
        {
            using var connection = new Connection(new ClientConnector(_hostname, _port));
            connection.SendData(_decoder.Encode(request));
            BytesData responseBytes = connection.GetData();
            Response response = _decoder.Decode<Response>(responseBytes);
            return response;
        }
        
    }
}