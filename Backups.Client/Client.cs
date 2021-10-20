using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Backups.FileSystem;
using Backups.Server;
using Microsoft.Extensions.DependencyInjection;
using File = Backups.FileSystem.File;

namespace Backups.Client
{
    public class Client
    {
        private const string _hostname = "localhost";
        private const int _port = 8888;


        private Response MakeRequest(Request request)
        {
            using var tcpClient = new TcpClient(_hostname, _port);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(Encoding.Default.GetBytes(JsonSerializer.Serialize(request)));

            byte[] buffer = new byte[1024];
            int bytesRead = networkStream.Read(buffer);
            return JsonSerializer.Deserialize<Response>(Encoding.Default.GetString(buffer,0, bytesRead));
        }
        
        public void Run()
        {
            var file1 = new File(new FileName("file1"), new byte []{1,2,3});
            var file2 = new File(new FileName("file2"), new byte []{4,5,6});


            var createRequest = new Request(RequestType.CreateJob, new RequestData() {JobName = "Job1", StorageMode = StorageMode.SingleStorage});
            var addFileRequest1 = new Request(RequestType.UploadFile, new RequestData() {File = file1, Path = ""});
            var addFileRequest2 = new Request(RequestType.UploadFile, new RequestData() {File = file2, Path = ""});
            var observeFileRequest1 = new Request(RequestType.ObserveFile, new RequestData {Path = "file1"});
            var observeFileRequest2 = new Request(RequestType.ObserveFile, new RequestData {Path = "file2"});
            var deleteFileRequest =
                new Request(RequestType.DeleteJobObject, new RequestData() {ObjectName = file2.Name.Name});
            var makePointRequest = new Request(RequestType.MakeRestorePoint, new RequestData{JobName = "Job1"});
            
            MakeRequest(createRequest);
            MakeRequest(addFileRequest1);
            MakeRequest(addFileRequest2);
            MakeRequest(observeFileRequest1);
            MakeRequest(observeFileRequest2);
            MakeRequest(deleteFileRequest);
            MakeRequest(makePointRequest);
        }
    }
}