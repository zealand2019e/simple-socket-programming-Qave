using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    public class Server
    {
        public void Start()
        {
            TcpListener ServerListen = new TcpListener(IPAddress.Loopback, 3002);
            // start listening for clients on port 3001
            ServerListen.Start();
            Console.WriteLine("Listening for clients...");
            while (true)
            {
                TcpClient socket = ServerListen.AcceptTcpClient();
                Console.WriteLine("Client found");

                // For every new client, run a new task so they run simultaneously
                Task.Run(() =>
                {
                    // create a temp socket for the new client
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });

            }
        }
        public double Result { get; set; } = 0;
        public string Operator { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public void DoClient(TcpClient socket)
        {
            using (socket)
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
                        Console.WriteLine(line);



                        if (line != null)
                        {
                            //WordCount += line.Split(' ').Length;
                            if (CanCalculate(line))
                            {
                                streamWriter.WriteLine("Result: " + Result);
                            }
                            else
                            {
                                streamWriter.WriteLine("Formatting error, please make sure to provide whole numbers and in this format 'Add 5 5'");
                            }

                        }
                        // Write the line to the server that is read from the client

                        streamWriter.Flush();
                    }
                    catch (IOException)
                    {
                        socket.Dispose();
                        Console.WriteLine("Client disconnected");
                        return;
                    }
                }
            }
        }
        public bool CanCalculate(string streamLine)
        {
            string _operator = streamLine.Split(' ')[0];
            double firstNo = 0;
            double secondNo = 0;
            try
            {
                firstNo = double.Parse(streamLine.Split(' ')[1], new CultureInfo("en-UK"));
                secondNo = double.Parse(streamLine.Split(' ')[2], new CultureInfo("en-UK"));
            }
            catch (Exception)
            {
                return false;
            }

            switch (_operator)
            {
                case "add":
                    Result = firstNo + secondNo;
                    _operator = "";
                    break;
                case "mul":
                    Result = firstNo * secondNo;
                    _operator = "";
                    break;
                case "sub":
                    Result = firstNo - secondNo;
                    _operator = "";
                    break;
                case "div":
                    Result = firstNo / secondNo;
                    _operator = "";
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
