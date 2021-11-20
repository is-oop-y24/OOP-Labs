using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Data;
using Banks.BusinessLogic.Tools;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            new Application().Run();
        }
    }
}
