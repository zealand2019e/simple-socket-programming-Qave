using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
{
    public class Server
    {
        public void Start()
        {
            TcpListener ServerListen = new TcpListener(IPAddress.Loopback, 7);
            ServerListen.Start();

            TcpClient socket = ServerListen.AcceptTcpClient();

            using (socket)
            {
                NetworkStream ns = socket.GetStream();

                StreamReader streamReader = new StreamReader(ns);
                StreamWriter streamWriter = new StreamWriter(ns);

                string line = streamReader.ReadLine();

                streamWriter.WriteLine(line);
                streamWriter.Flush();

            }
        }
    }
}
