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
            
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ServerExceptionConverter());
            _decoder = new BytesDecoder(options);
        }


        private Response MakeRequest(Request request)
        {
            using var connection = new Connection(new ClientConnector(_hostname, _port));
            connection.SendData(_decoder.Encode(request));
            BytesData responseBytes = connection.GetData();
            Response response = _decoder.Decode<Response>(responseBytes);
            return response;
        }

        public void TestCase1()
        {
            var file1 = new BackupFile(new FileName("file1"), new byte []{1,2,3});
            var file2 = new BackupFile(new FileName("file2"), new byte []{4,5,6});
            const string jobName = "Job1";
            
            var createJobRequest = new Request(RequestType.CreateJob, new RequestData()
            {
                JobName = jobName, 
                StorageMode = StorageMode.SingleStorage
            });
            
            var addFileRequest1 = new Request(RequestType.UploadFile, new RequestData()
            {
                BackupFile = file1, 
                Path = ""
            });
            
            var addFileRequest2 = new Request(RequestType.UploadFile, new RequestData()
            {
                BackupFile = file2, 
                Path = ""
            });
            
            var observeFileRequest1 = new Request(RequestType.ObserveFile, new RequestData
            {
                Path = "file1", 
                JobName = jobName
            });
            
            var observeFileRequest2 = new Request(RequestType.ObserveFile, new RequestData
            {
                Path = "file2",
                JobName = jobName
            });
            
            var deleteFileRequest = new Request(RequestType.DeleteJobObject, new RequestData()
            {
                ObjectName = file2.Name.Name,
                JobName = jobName
            });
            
            var makePointRequest = new Request(RequestType.MakeRestorePoint, new RequestData
            {
                JobName = jobName
            });

            var requests = new List<Request>()
            {
                createJobRequest,
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

        public void TestCase2()
        {
            var file1 = new BackupFile(new FileName("file3"), new byte []{1,2,3});
            var file2 = new BackupFile(new FileName("file4"), new byte []{4,5,6});
            const string jobName = "Job1";
            const string jobPath = "jobDirectory"; 
            
            var createJobRequest = new Request(RequestType.CreateJob, new RequestData()
            {
                JobName = jobName, 
                StorageMode = StorageMode.SplitStorage,
                Path = jobPath
            });
            
            var addFileRequest1 = new Request(RequestType.UploadFile, new RequestData()
            {
                BackupFile = file1, 
                Path = ""
            });
            
            var addFileRequest2 = new Request(RequestType.UploadFile, new RequestData()
            {
                BackupFile = file2, 
                Path = ""
            });
            
            var observeFileRequest1 = new Request(RequestType.ObserveFile, new RequestData
            {
                Path = "file3", 
                JobName = jobName
            });
            
            var observeFileRequest2 = new Request(RequestType.ObserveFile, new RequestData
            {
                Path = "file4",
                JobName = jobName
            });
            
            
            var makePointRequest = new Request(RequestType.MakeRestorePoint, new RequestData
            {
                JobName = jobName
            });

            var requests = new List<Request>
            {
                createJobRequest,
                addFileRequest1,
                addFileRequest2,
                observeFileRequest1,
                observeFileRequest2,
                makePointRequest
            };
            
            foreach (Request request in requests)
            {
                Response response = MakeRequest(request);
                if (response.ResponseCode == ResponseCode.Error)
                    Console.WriteLine("Error: " + response.ResponseData.Exception.Message);
            }
        }
        
        public void Run()
        {
            //TestCase1();
            TestCase2();
        }
    }
}