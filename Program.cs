using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
public class UDPServer
{
    public static void StartServer()
    {
        byte[] buffer = new byte[512];
        IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = hostInfo.AddressList[1];
        Console.WriteLine("Server name: {0}  IP: {1}", hostInfo.HostName, ip);
        IPEndPoint localEP = new IPEndPoint(ip, 8888);
        Socket server = new Socket(ip.AddressFamily,
            SocketType.Dgram, ProtocolType.Udp);

        EndPoint remoteClient = new IPEndPoint(IPAddress.Any, 0);
        try
        {
            server.Bind(localEP);
            Console.WriteLine("Waiting for data....");
            while (true)
            {
                int recv = server.ReceiveFrom(buffer, ref remoteClient);
                // server.SendTo()
                Console.WriteLine("Recv from: {0}   Data: {1}",
                    remoteClient.ToString(), Encoding.ASCII.GetString(buffer, 0,
recv));
            }
            //server shutdown
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//using System.Net;
//using System.Net.Sockets;
//
//namespace AsyncServer
//{
//    class Program
//    {
//        private static byte[] buffer = new byte[512];
//        private static Socket server;
//        private static byte[] outBuffer = new byte[512];
//        private static string outMsg = "";
//        private static float[] pos;
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Starting the server....");
//            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
//ProtocolType.Tcp);
//            IPAddress ip = IPAddress.Parse("192.168.2.51");
//            server.Bind(new IPEndPoint(ip, 8888));
//            server.Listen(10);
//            server.BeginAccept(new AsyncCallback(AcceptCallback), null);
//            Console.Read();
//        }
//        private static void AcceptCallback(IAsyncResult result)
//        {
//            Socket client = server.EndAccept(result);
//            Console.WriteLine("Client connected!!!   IP:");
//            client.BeginReceive(buffer, 0, buffer.Length, 0,
//                new AsyncCallback(ReceiveCallback), client);
//        }
//        private static void ReceiveCallback(IAsyncResult result)
//        {
//            Socket socket = (Socket)result.AsyncState;
//            int rec = socket.EndReceive(result);
//            //lec05
//            //byte[] data = new byte[rec];
//            //Array.Copy(buffer, data, rec);
//            //lec06
//            pos = new float[rec / 4];
//            Buffer.BlockCopy(buffer, 0, pos, 0, rec);
//            //string msg = Encoding.ASCII.GetString(data);
//            //Console.WriteLine("Recv: " + msg);
//            socket.BeginSend(buffer, 0, buffer.Length, 0,
//                new AsyncCallback(SendCallback), socket);
//            Console.WriteLine("Received X:" + pos[0] + " Y:" + pos[1] + " Z:" +
//pos[2]);
//            //lec05
//            //socket.BeginSend(data, 0, data.Length, 0, 
//            //    new AsyncCallback(SendCallback), socket);
//            socket.BeginReceive(buffer, 0, buffer.Length, 0,
//                new AsyncCallback(ReceiveCallback), socket);
//        }
//        private static void SendCallback(IAsyncResult result)
//        {
//            Socket socket = (Socket)result.AsyncState;
//            socket.EndSend(result);
//        }
//    }
//}