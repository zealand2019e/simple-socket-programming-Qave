using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.Start();
            Console.ReadLine();
        }
        
    }
}
