using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientControl clientControl = new ClientControl();
            clientControl.Connect("127.0.0.1", 12345);

            Console.WriteLine("请输入要发送的信息");
            string msg = null;
            while(msg != "quit")
            {
                clientControl.Send(msg);
                msg = Console.ReadLine();
            }
            Console.ReadKey();
        }
    }
}
