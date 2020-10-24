using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCommExample
{
    public class TcpIpServer
    {
        static string dbStr = "Server=192.168.0.30;Database=SF1team;User Id=sa;Password=0924;";

        static TcpListener server = null;

        static Thread InProdThread = new Thread(new ThreadStart(InProdDataProc));    // 입고 송수신 스레드
        static Thread OutProdTread = new Thread(new ThreadStart(OutProdDataProc));   // 출고 송수신 스레드
        Thread connThread = new Thread(new ThreadStart(ConnectProc));                // 연결 처리 스레드

        public static TcpClient in_client;  // 입고 로봇
        public static TcpClient out_client; // 출고 로봇

        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        public TcpIpServer()
        {
            bool bServerStart = ServStart();
            if (bServerStart)
            {
                // 서버 시작
                connThread.Start();
            }
        }

        public static TcpClient GetInClient()
        {
            return in_client;
        }
        public static TcpClient GetOutClient()
        {
            return out_client;
        }


        public static void ConnectProc()
        {
            while (true)
            {
                Thread.Sleep(1);

                tcpClientConnected.Reset();
                server.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), server);
                tcpClientConnected.WaitOne();
            }
        }

        public static void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            //if (in_client == null)
            //{
            //    in_client = listener.EndAcceptTcpClient(ar);
            //    string ip = ((IPEndPoint)in_client.Client.RemoteEndPoint).Address.ToString();
            //    Console.WriteLine("접속 (1) :{0}", ip);
            //}
            //else 
            
            if (out_client == null)
            {
                out_client = listener.EndAcceptTcpClient(ar);
                string ip = ((IPEndPoint)out_client.Client.RemoteEndPoint).Address.ToString();
                Console.WriteLine("접속 (2) :{0}", ip);
                OutProdTread.Start();
            }

            tcpClientConnected.Reset();
            listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), listener);
            tcpClientConnected.WaitOne();
        }

        public static void InProdDataProc()
        {
            //while (true)
            //{
            //    Thread.Sleep(1);

            //    TcpClient client = TcpIpServer.GetInClient();

            //    Console.WriteLine("입고 로봇 :{0}", ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
            //    Console.WriteLine();

            //    NetworkStream stream = client.GetStream();

            //    int length;
            //    string data = null;
            //    byte[] bytes = new byte[256];

            //    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            //    {
            //        data = Encoding.Default.GetString(bytes, 0, length);
            //        Console.WriteLine(String.Format("수신 : {0}", data));

            //        byte[] msg = Encoding.Default.GetBytes(data);
            //        stream.Write(msg, 0, msg.Length);
            //        Console.WriteLine(String.Format("송신: {0}", data));
            //    }
            //    stream.Close();
            //    client.Close();
            //}
        }

        public static void OutProdDataProc()
        {
            while (true)
            {
                Thread.Sleep(1);

                TcpClient client = TcpIpServer.GetOutClient();

                Console.WriteLine("출고 로봇 :{0}", ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                Console.WriteLine();

                NetworkStream stream = client.GetStream();

                int length;
                string data = null;
                byte[] bytes = new byte[256];

                // 로봇으로 부터 메세지 수신하는 루틴

                string sRcvMessage="";
                
                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.Default.GetString(bytes, 0, length);
                    Console.WriteLine(String.Format("수신 : {0}", data));
                    sRcvMessage = data;

                    byte[] msg = Encoding.Default.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine(String.Format("송신: {0}", data));

                    if (sRcvMessage == "ROBOT_POS")
                    {
                        // 함수 호출
                    }
                    else if (sRcvMessage == "PROD_QRCODE")
                    {
                        // 함수 호출
                    }
                    else if (sRcvMessage == "hello")
                    {
                        string snd_msg = "hi hi";
                        byte[] msg2 = Encoding.Default.GetBytes(snd_msg);
                        stream.Write(msg2, 0, msg2.Length);
                        Console.WriteLine(String.Format("송신: {0}", snd_msg));
                    }
                }               


                stream.Close();
                client.Close();
            }
        }


        public static bool ServStart()
        {
            try
            {
                string bindIp = "127.0.0.1";
                const int bindPort = 5425;
                IPEndPoint localAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);
                server = new TcpListener(localAddress);
                server.Start();
                Console.WriteLine("서버 시작...");

                return true;
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}