using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    public class Client
    {
        public void Start()
        {

            using (TcpClient socket = new TcpClient("localhost", 7))
            {
                NetworkStream ns = socket.GetStream();

                StreamReader streamReader = new StreamReader(ns);
                StreamWriter streamWriter = new StreamWriter(ns);

                streamWriter.WriteLine("Lol");
                streamWriter.Flush();


                string line = streamReader.ReadLine();
                Console.WriteLine(line);


            }      
            

        }
    }
}
