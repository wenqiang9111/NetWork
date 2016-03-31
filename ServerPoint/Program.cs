using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerPoint
{
    class Program
    {

        static void Main(string[] args)
        {
            byte[] SendBuf = Encoding.UTF8.GetBytes("Exit!");
            IPEndPoint LocalEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
            TcpListener listener = new TcpListener(LocalEP);
            listener.Start(10);

            Console.WriteLine("Server is listening...");
            while (true)
            {
                TcpClient remoteClient = listener.AcceptTcpClient();
                Thread th = new Thread(new ThreadStart(() =>
                {
                    Console.WriteLine("Client:{0} connected!", remoteClient.Client.RemoteEndPoint.ToString());
                    while (true)
                    {
                        byte[] RecvBuf = new byte[1024];
                        int RecvBytes = 0;
                        string recvmsg = null;

                        RecvBytes = remoteClient.Client.Receive(RecvBuf);
                        recvmsg = Encoding.UTF8.GetString(RecvBuf, 0, RecvBytes);
                        Console.WriteLine("Client {0} says:{1}", remoteClient.Client.RemoteEndPoint.ToString(), recvmsg);
                        if (recvmsg.Contains("886") || recvmsg.Contains("再见"))
                        {
                            Console.WriteLine("Client {0} 断开连接！", remoteClient.Client.RemoteEndPoint.ToString());
                            break;
                        }
                    }
                    remoteClient.Client.Send(SendBuf);
                    remoteClient.Close();
                }));
                th.Start();
            }
        }
    }
}
