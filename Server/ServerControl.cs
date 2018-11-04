using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class ServerControl
    {
        private Socket serverSocket;

        public ServerControl()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 12345));
            serverSocket.Listen(10);
            Console.WriteLine("服务器启动成功");

            Thread threadAccept = new Thread(Accept);
            threadAccept.IsBackground = true;
            threadAccept.Start();
        }

        private void Accept()
        {
            //接受客户端的方法，会挂起当前线程
            Socket client = serverSocket.Accept();
            IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine(point.Address + "[" + point.Port + "]连接成功");

            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start(client);

            Accept();
        }

        public void Receive(object obj)
        {
            Socket client = obj as Socket;

            IPEndPoint point = client.RemoteEndPoint as IPEndPoint;

            byte[] msg = new byte[1024];
            int msgLen = client.Receive(msg);
            Console.WriteLine(point.Address + "[" + point.Port + "]:" + Encoding.UTF8.GetString(msg, 0, msgLen));

            Receive(client);
        }
    }
}
