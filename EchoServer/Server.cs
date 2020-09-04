using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
                DoClient(socket);
            }
            Console.ReadKey();
        }
        public int WordCount { get; set; } = 0;
        public void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();

            StreamReader streamReader = new StreamReader(ns);
            StreamWriter streamWriter = new StreamWriter(ns);

            while (true)
            {
                // Word recieved from the client
                try
                {
                    // Word read from the client
                    string line = streamReader.ReadLine();

                    // Count the words
                    WordCount += line.Split(' ').Length;
                    // Write the line that is read from the client
                    Console.WriteLine(line);

                    streamWriter.WriteLine("Totalt words sent to server: " + WordCount);
                    streamWriter.Flush();

                }
                catch (IOException)
                {
                    Console.WriteLine("Connection to the client was closed");
                    return;
                }
            }
        }
    }
}
