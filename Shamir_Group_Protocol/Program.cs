using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Numerics;

namespace Shamir_Group_Protocol
{
    class Program
    {
        private static List<Socket> _clients = new List<Socket>();
        private static Socket TcpSocket;
        private static int n;
        private static int k;

        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\secret\\";
            n = 3;
            k = n + 1;
            Set_Server(n);
        }

        static void Reciever()
        {
            
            while (true)
            {
                var buffer = new byte[8*50];
                TcpSocket.Receive(buffer);
                foreach (var item in _clients)
                {
                    item.Send(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(buffer, 0, buffer.Length)));
                }
            }
            
           

        }

        static void Set_Server( int n)
        {
            const string ip = "127.0.0.1";

            int port = 9900;
            var TcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpSocket.Bind(TcpEndPoint);
            TcpSocket.Listen(n);
            Console.WriteLine("Server listening...........................");

            while (_clients.Count < n)
            {
                var listener = TcpSocket.Accept();
                _clients.Add(listener);
                listener.Send(Encoding.UTF8.GetBytes("Connected"));
                Console.WriteLine("Connected + 1");

            }
            foreach (var item in _clients)
            {
                item.Send(Encoding.UTF8.GetBytes("all connected"));
                Console.WriteLine("Вся команда зашла в комнату");
            }

            Thread rec = new Thread(Reciever);
            //rec.Start();
            Mg_Console();

        }
        static void Sender()
        {
            
        }

        static void Mg_Console()
        {
            Console.WriteLine("new - создать новую команду");
            Console.WriteLine("check - проверить пользователей на авторизацию");
            string command;
            var buffer = new byte[8*50];
            SGP sgp = new SGP(k);
            int size;
            while (true)
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "new":
                        foreach (var item in _clients)
                        {
                            item.Send(Encoding.UTF8.GetBytes("new"));
                            size = item.Receive(buffer);
                            if (Encoding.UTF8.GetString(buffer, 0, size) != "done") break;
                        }
                        for (int i = 0; i < n; i++)
                        {
                            _clients[i].Send(Encoding.UTF8.GetBytes(sgp.users[i+1].ToString()));
                            size = _clients[i].Receive(buffer);
                            if (Encoding.UTF8.GetString(buffer, 0, size) != "done") break;
                        }
                        Console.WriteLine("Zp =" + sgp.Zp.ToString());
                        write(sgp.users[0].ToString());
                        break;
                    case "check":
                        sgp.users[0] = new koef(Read());
                        foreach (var item in _clients)
                        {
                            item.Send(Encoding.UTF8.GetBytes("check"));
                        }
                        for (int i = 1; i < k; i++)
                        {


                            do
                            {
                                size = _clients[i-1].Receive(buffer);

                            } while (TcpSocket.Available > 0);

                            sgp.users[i] = new koef(Encoding.UTF8.GetString(buffer, 0, size));
                        }
                        Console.WriteLine("Write Zp");
                        BigInteger Zp = BigInteger.Parse(Console.ReadLine());
                        if (SGP.Check_Sum(sgp.users, Zp))
                        {
                            Console.WriteLine("All authorized");
                        }
                        else
                        {
                            Console.WriteLine("m =" + SGP.m );
                            
                        }
                        

                        Console.WriteLine();break;
                    default:
                        break;
                }
                

            }
        }
        public static void write( string str, string name = "dealler")
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\secret\\" + name + ".txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(str);
            }
        }
        public static string Read(string name = "dealler")
        {
            string result;
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\secret\\" + name + ".txt";
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            char[] charsToTrim = { '\n', '\r' };
            result = result.Trim(charsToTrim);
            return result;
        }
    }
}
