using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SClient
{
    public class SClient
    {
        public static void Main(string[] args)
        {
            Console.Write("Connection to ");
            byte[] bytes = new byte[1024];
            try
            {
                while (true)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(File.ReadAllText("ip.txt")), 8888);
                    Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sender.Connect(ipEndPoint);
                    Console.Write("{0} ... DONE!\n", sender.RemoteEndPoint.ToString());
                    Console.WriteLine("\nCommand: rec - record and play\n\t rom - open cd-rom\n\t mon - monitor off\n\t any command cmd\n");
                    string theMessage = Console.ReadLine();
                    Console.WriteLine("\nConnection to {0}", sender.RemoteEndPoint.ToString());
                    Console.WriteLine("Wait 5 sec...");
                    byte[] msg = Encoding.ASCII.GetBytes(theMessage);
                    int bytesSent = sender.Send(msg);
                    string data = null;
                    int bytesRec = sender.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.Clear();
                    Console.WriteLine(data + "\n");
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}