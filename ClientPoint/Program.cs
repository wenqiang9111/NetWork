using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] RecvBuf = new byte[1024];
            int RecvBytes = 0;
            string recvmsg = null;
            while (true)
            {
                IPEndPoint remodteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
                TcpClient remoteServer = new TcpClient();
                remoteServer.Connect(remodteEP);
                string sendStr = Console.ReadLine();
                byte[] SendBuf = Encoding.UTF8.GetBytes(sendStr);
                remoteServer.Client.Send(SendBuf);

                if (sendStr.Contains("886") || sendStr.Contains("再见"))
                {
                    RecvBytes = remoteServer.Client.Receive(RecvBuf);
                    recvmsg = Encoding.UTF8.GetString(RecvBuf, 0, RecvBytes);
                    if (recvmsg == "Exit!")
                    {
                        break;
                    }
                }
            }
            Console.WriteLine(recvmsg);
            Console.ReadKey();
        }
    }
}
