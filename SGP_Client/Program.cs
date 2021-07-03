using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SGP_Client
{
    class Program
    {
        private static Socket TcpSocket;
        public static string name;
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
             int port = 9900;
            Console.WriteLine("Write your name");
            name = Console.ReadLine();

            var TcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                TcpSocket.Connect(TcpEndPoint);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }

            Thread send = new Thread(Sender);
            Thread reciever = new Thread(Reciever);
            //reciever.Start();
            //send.Start();
            MG_Console();
        }


        
        public static void MG_Console()
        {
            var buffer = new byte[8*50];
            string message = "";
            int size;
            while (message != "all connected")
            {


                do
                {
                    size = TcpSocket.Receive(buffer);
                    message = Encoding.UTF8.GetString(buffer, 0 , size);

                } while (TcpSocket.Available > 0);
                Console.WriteLine("From server:" + message);
            }
            while (true)
            {
                Console.WriteLine("MG Console receive");
                do
                {
                size = TcpSocket.Receive(buffer);

                } while (TcpSocket.Available >0);
                message =  Encoding.UTF8.GetString(buffer, 0 , size);

                Console.WriteLine("From server:" + message);
                switch (message)
                {
                    case "new":
                        TcpSocket.Send(Encoding.UTF8.GetBytes("done"));
                        size = TcpSocket.Receive(buffer);
                        TcpSocket.Send(Encoding.UTF8.GetBytes("done"));
                        write(name, Encoding.UTF8.GetString(buffer, 0, size));
                    break;
                    case "check":
                        string request = Read(name);
                        TcpSocket.Send(Encoding.UTF8.GetBytes(request));
                        Console.WriteLine(request + "SENDED");
                        break;

                    default:
                        break;
                }

            }

        }

        public static void Sender()
        {
            while (true)
            {
                Console.WriteLine("Write your message ::::");
                TcpSocket.Send((Encoding.UTF8.GetBytes(Console.ReadLine())));
            }

        }
        public static void Reciever()
        {
            while (true)
            {
                Console.WriteLine("thread recieve");
                var buffer = new byte[8*50];
                TcpSocket.Receive(buffer);
                string mes = Encoding.UTF8.GetString(buffer);
                Console.WriteLine();
            }

        }
        public static void write(string name, string str)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\secret\\" + name + ".txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(str);
            }
        }
        public static string Read(string name)
        {
            string result;
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\secret\\" + name + ".txt";
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            char[] charsToTrim = {'\n','\r' };
            result = result.Trim(charsToTrim);
            return result;
        }
    }
}
