using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyliaServer.Utilities;

namespace VyliaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Out.Info("Server starting...");

            AsynchronousSocketListener.StartListening();

            Out.Info("Server stopping...");
        }
    }
}
