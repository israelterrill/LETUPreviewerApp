using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace APIDataServer
{
    class Program
    {
        private const int LISTEN_PORT = 44623;

        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, LISTEN_PORT);

            Socket listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    Task.Factory.StartNew(() => HandleConnection(handler));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener.Close();
            }
        }

        private static void HandleConnection(Socket handler)
        {
            string data = null;

            while (true)
            {
                var bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }

            handler.Send(Encoding.ASCII.GetBytes(HandleRequest(data)));

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        private static string HandleRequest(string requestStr)
        {
            var response = string.Empty;

            return response;
        }
    }
}
