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
using File = Backups.FileSystem.File;

namespace Backups.Client
{
    public class Client
    {
        private const string _hostname = "localhost";
        private const int _port = 8888;
        private readonly Logger _logger = new Logger();


        private Response MakeRequest(Request request)
        {
            using var tcpClient = new TcpClient(_hostname, _port);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(Encoding.Default.GetBytes(JsonSerializer.Serialize(request)));

            byte[] buffer = new byte[1024];
            int bytesRead = networkStream.Read(buffer);
            string responseString = Encoding.Default.GetString(buffer, 0, bytesRead);
            _logger.Log(responseString);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ServerExceptionConverter());
            Response response = JsonSerializer.Deserialize<Response>(responseString, options);
            return response;
        }
        
        public void Run()
        {
            var file1 = new File(new FileName("file1"), new byte []{1,2,3});
            var file2 = new File(new FileName("file2"), new byte []{4,5,6});
            const string jobName = "Job1";
            
            var createRequest = new Request(RequestType.CreateJob, new RequestData() {JobName = jobName, StorageMode = StorageMode.SplitStorage});
            var addFileRequest1 = new Request(RequestType.UploadFile, new RequestData() {File = file1, Path = ""});
            var addFileRequest2 = new Request(RequestType.UploadFile, new RequestData() {File = file2, Path = ""});
            var observeFileRequest1 = new Request(RequestType.ObserveFile, new RequestData {Path = "file1", JobName = jobName});
            var observeFileRequest2 = new Request(RequestType.ObserveFile, new RequestData {Path = "file2", JobName = jobName});
            var deleteFileRequest =
                new Request(RequestType.DeleteJobObject, new RequestData() {ObjectName = file2.Name.Name, JobName = jobName});
            var makePointRequest = new Request(RequestType.MakeRestorePoint, new RequestData{JobName = jobName});

            var requests = new List<Request>()
            {
                createRequest,
                addFileRequest1,
                addFileRequest2,
                observeFileRequest1,
                observeFileRequest2,
                makePointRequest,
                deleteFileRequest,
                makePointRequest,
            };

            foreach (Request request in requests)
            {
                Response response = MakeRequest(request);
                if (response.ResponseCode == ResponseCode.Error)
                    Console.WriteLine("Error: " + response.ResponseData.Exception.Message);
            }
        }
    }
}