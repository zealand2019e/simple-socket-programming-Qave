﻿using System;
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
            try
            {
                using (TcpClient socket = new TcpClient("localhost", 7))
                {
                    NetworkStream ns = socket.GetStream();

                    StreamReader streamReader = new StreamReader(ns);
                    StreamWriter streamWriter = new StreamWriter(ns);

                    while (true)
                    {

                        try
                        {

                            // Word sent to the server
                            string lineSentToServer = Console.ReadLine();
                            // Send the word to the server
                            streamWriter.WriteLine(lineSentToServer);
                            // flush the streamWriter
                            streamWriter.Flush();

                            string line = streamReader.ReadLine();
                            Console.WriteLine(line);
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("Connection to the server cannot be made, is it running?");
                            return;
                        }
                        
                    }

                }
            }
            catch (SocketException)
            {
                Console.WriteLine("No connection could be made to the server.");
                return;
            }
        }
    }
}
