using Cykel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace Client
{
    public class ClientWorker
    {
        private static List<Cykel.Cykel> cykels = new List<Cykel.Cykel>()
        {
            new Cykel.Cykel("Blå", 500, 16, 1),
            new Cykel.Cykel("Gul", 500, 24, 2),
            new Cykel.Cykel("Rød", 1000, 32, 3),
            new Cykel.Cykel("Blå", 1500, 8, 4),
            new Cykel.Cykel("Rød", 750, 8, 5),
        };

        private Cykel.Cykel c;
        public ClientWorker()
        {
            c = new Cykel.Cykel();
        }
        public List<Cykel.Cykel> L { get => cykels; }

        public void Start()
        {
            TcpClient socket = new TcpClient("localhost", 4646);

            DoClient(socket);

        }
        public void DoClient(TcpClient socket)
        {
            // Net Stream
            NetworkStream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            // tekst da sendes fra klient
            Cykel.Cykel ck = new Cykel.Cykel("Sort", 299, 12, 8); 
            sw.AutoFlush = true;
            String json = JsonConvert.SerializeObject(GetAll());
            string json1 = JsonConvert.SerializeObject(Get(2));
            string json2 = JsonConvert.SerializeObject(Gem(ck));
            sw.WriteLine(json);
            sw.WriteLine(json1);
            sw.WriteLine(json2);
            //skriv tilbage til klient
            string strRetur = sr.ReadLine();
            string str1retur = sr.ReadLine();
            string str2retur = sr.ReadLine();
            Console.WriteLine(strRetur);
            Console.WriteLine("");
            Console.WriteLine(str1retur);
            Console.WriteLine("");
            Console.WriteLine(str2retur);

            socket.Close();
        }
        public List<Cykel.Cykel> GetAll()
        {
            return L;
        }
        // GET api/<PizzasController>/5
        public Cykel.Cykel Get(int id)
        {
            return L.Find(p => p.Id == id);
        }
        public Cykel.Cykel Gem(Cykel.Cykel cykel)
        {
            cykels.Add(cykel);
            return cykel;
        }
        
    }
}
