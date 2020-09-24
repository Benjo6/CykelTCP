using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerWorker
    {
        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 4646);
            server.Start();

            TcpClient socket = server.AcceptTcpClient();

            DoClient(socket);

            socket.Close();


        }
        public void DoClient(TcpClient socket)
        {
            // Net Stream
            NetworkStream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            // læs tekst fra klient
            string str = sr.ReadLine();
            string str1 = sr.ReadLine(); 
            string str2 = sr.ReadLine();
            Console.WriteLine($"{str}");
            Console.WriteLine("");
            Console.WriteLine($"{str1}");
            Console.WriteLine("");
            Console.WriteLine($"{str2}");
            Console.WriteLine("");


            //skriv tilbage til klient
            string sum = str.ToUpper();
            string sum1 = str1.ToUpper();
            string sum2 = str2.ToUpper();
            sw.WriteLine(sum);
            sw.WriteLine(sum1);
            sw.WriteLine(sum2);
            sw.Flush();


        }
    }
}
