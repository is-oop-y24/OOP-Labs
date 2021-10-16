using System;
using Backups.FileSystem;
using Isu;

namespace Backups.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().Configure().Run();
        }
    }
}