using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPServer
{
    public class TcpServer
    {
        private TcpListener _server;
        private Boolean _isRunning;
        

        public TcpServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            LoopClients();
        }

        public void LoopClients()
        {
            Console.WriteLine("Server byl spusten");
            while (_isRunning)
            {
                // čeká na spojení klienta
                TcpClient newClient = _server.AcceptTcpClient();
                string clientIPAddress = "IP adresa klienta: " + 
                    IPAddress.Parse(((IPEndPoint)newClient.Client.RemoteEndPoint).Address.ToString());
                Console.WriteLine(clientIPAddress);
                

                // klient se spojil
                // vytvoření vlákna pro komunikaci s klientem
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {
            // získání klienta z parametru předaného vláknu
            TcpClient client = (TcpClient)obj;
            string clientIPAddress = "IP adresa klienta: " +
                    IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());

            // vytvoření streamů pro komunikaci
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);

            Boolean bClientConnected = true;
            
           
            String jmeno = null;
            String prosba = null;
            String akce = null;
            String pismeno = null;
            

            int cisloA = 0;
            int cisloB = 0;
            int soucet = cisloA + cisloB;
            int odecet = cisloA - cisloB;
            int nasobeni = cisloA * cisloB;
            
          
            // po zapnuti
            sWriter.WriteLine("Uspesne jste se pripojili na server.");
            sWriter.Flush();
            sWriter.WriteLine("-----------------------");
            sWriter.Flush();

            // klientsk0 e jmeno 
            // Hotovo

            sWriter.WriteLine("Server> Klientske jmeno?.");
            sWriter.Flush();
            sWriter.Write("klient >");
            sWriter.Flush();
            jmeno = sReader.ReadLine();
            sWriter.Flush();
            string vracenajmeno = "Server> " + jmeno + "? Je toto tve jmeno?";
            sWriter.WriteLine(vracenajmeno);
            sWriter.Flush();
            sWriter.Write(jmeno + ">");
            sWriter.Flush();
            sReader.ReadLine();
            sWriter.WriteLine("Server> Pokud není máš smolíka ;)");
            sWriter.Flush();
            sWriter.WriteLine();
            sWriter.Flush();
            sWriter.WriteLine("Server> Tyto cmd umím");
            sWriter.Flush();
            sWriter.WriteLine("--------------------------------");
            sWriter.Flush();
            sWriter.WriteLine("Server> Seznam menu   (seznam) ");
            sWriter.Flush();
            sWriter.WriteLine("Server> Menu matematiky(matika)");
            sWriter.Flush();
            sWriter.WriteLine("Server> Help    (help)         ");
            sWriter.Flush();
            sWriter.WriteLine("Server> Exit session (exit)    ");
            sWriter.Flush();
            sWriter.WriteLine("--------------------------------");
            sWriter.Flush();

            //Comandy




            while (bClientConnected)
            {
                // Pozadavek

                sWriter.WriteLine("\n\rServer> Chceš něco??.");

                sWriter.Flush();
                sWriter.Write(jmeno + ">");
                sWriter.Flush();
                prosba = sReader.ReadLine();

                if (prosba == "Seznam")
                {
                    sWriter.WriteLine();

                    sWriter.Flush();
                    sWriter.WriteLine("-------------------------");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Menu matematiky(Matiku)|");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Ukol    (Ukol)         |");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Help    (help)         |");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Exit session (exit)    |");
                    sWriter.Flush();
                    sWriter.WriteLine("-------------------------");
                    sWriter.Flush();
                }
                if (prosba == "Ukol")
                {

                    
                    sWriter.WriteLine("Spočtěme si počet souhlásek a samohlásek. Napiš větu");
                    sWriter.Write(jmeno + ">");
                    sWriter.Flush();
                    prosba = sReader.ReadLine();

                    int i = 0;                   
                    int samohlaska = prosba.Count(i => "aeyuiox".Contains(char.ToLower(i)));
                    int souhlaska = prosba.Count(i => "qwrtpsdfghjklzcvbnm".Contains(char.ToLower(i)));


                    sWriter.WriteLine("Server> Samohlasek " + samohlaska);
                    sWriter.WriteLine("Server> Souhlásek " + souhlaska);
                }


                if (prosba == "help")
                {
                   
                    sWriter.WriteLine();
                    sWriter.Flush();
                    sWriter.WriteLine("Server> Tyto cmd umím");
                    sWriter.Flush();
                    sWriter.WriteLine("-------------------------");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Seznam menu   (Ukol) |");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Menu matematiky(Matiku)|");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Help    (help)         |");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> Exit session (exit)    |");
                    sWriter.Flush();
                    sWriter.WriteLine("-------------------------");
                    sWriter.Flush();
                } else if (prosba == "datum")
                {
                    sWriter.WriteLine("Datum: "+ DateTime.Now.ToString());
                    sWriter.Flush();
                }

                else if (prosba == "Matiku")
                {
                    sWriter.WriteLine();
                    sWriter.Flush();
                    sWriter.WriteLine("Server> Umím tyto operace");
                    sWriter.Flush();
                    sWriter.WriteLine("-------------------");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> |secti|    |");
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> |odecti|   |");
                    
                    sWriter.Flush();
                    sWriter.WriteLine("|Server> |vynasob|  |");
                    sWriter.Flush();
                    sWriter.WriteLine("-------------------");
                    sWriter.Flush();


                    sWriter.Write(jmeno + "> Zadej první číslo");
                    sWriter.Flush();
                    cisloA = Convert.ToInt32(sReader.ReadLine());
                    sWriter.WriteLine("Server > Číslo A tebou zadané je: " + cisloA);

                    sWriter.Write(jmeno + "> Zadej druhé číslo");
                    sWriter.Flush();
                    cisloB = Int32.Parse(sReader.ReadLine());
                    sWriter.WriteLine("Server > Číslo B tebou zadané je: " + cisloB);

                    
                }
               else if(prosba == "secti")
                {
                    sWriter.WriteLine("Server > Součet je: " + soucet);

                }
                else if (prosba == "odecti")
                {
                    sWriter.WriteLine("Server > Součet je: " + odecet);

                }
                else if (prosba == "vynasob")
                {
                    sWriter.WriteLine("Server > Součet je: " + nasobeni);

                }
                



                else if (prosba == "exit")
                {

                    Console.WriteLine("Sessinon ended");
                    client.Close();


                }

                else
                {
                    sWriter.WriteLine("Unknown command");
                    sWriter.Flush();
                }






            }

            /*
            
            */
        }
    }
}
