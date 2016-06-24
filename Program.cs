

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {

            IPHostEntry ipHostInfo = Dns.GetHostEntry("synclapn5500");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket serverSoket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            serverSoket.Bind(ipEndPoint);
            serverSoket.Listen(10);
            byte[] bytes = null;
            string strData = string.Empty;
            while (true)
            {
                Socket clientSocket = serverSoket.Accept();
                bytes = null;
                strData = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int dataCount = clientSocket.Receive(bytes);
                    strData += Encoding.UTF8.GetString(bytes, 0, dataCount);
                    if (strData.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                Console.WriteLine(strData);
            }

        }
    }
}
