﻿using System;
using System.Net.Sockets;
using System.Text.Json;
using Backups.Server;

namespace Backups.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            new Client().Run();
        }
    }
}