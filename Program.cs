using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Eine Multicast Gruppenadresse zwischen: 224.0.0.0 - 239.255.255.255
            var destAddr = IPAddress.Parse("224.0.0.100"); 
            // Multicast port
            var destPort = 8080;
            // Time-to-live für das Datagramm, Standard TTL = 1
            var TTL = 1;
            // Multicast Socket
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // Setze TTL
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, TTL);

            try
            {
                while (true)
                {
                    // Eingabe der Konsole lesen
                    Console.Write("Schreibe eine Nachricht: ");
                    var text = Console.ReadLine();
                    // Kodiere den Text zu einer Bytesequenz
                    var textBytes = Encoding.ASCII.GetBytes(text);
                    // Generiere Zielendpunkt
                    var endPoint = new IPEndPoint(destAddr, destPort);
                    // Sende die kodierte Nachricht als UDP-Paket
                    sock.SendTo(textBytes, 0, textBytes.Length, SocketFlags.None, endPoint);
                }
            }
            finally
            {
                sock.Close();
            }
        }
    }
}
