using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace QuikFix
{
    public static class NewRandom
    {
        public static Random Rnd { get; set; }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            NewRandom.Rnd = new Random();

            var fw = new FixWorker("127.0.0.1", 8001);

            //fw.SampleShowMessage();
            ;
            fw.OnLogon += fw_onLogon;
            fw.OnLogon += fw_onLogout;
            try
            {
                fw.SocketConnect();
                if (!fw.IsConnected)
                {
                    Console.WriteLine("{0}: Нет доступного подключения!", DateTime.Now.ToString("HH:mm:ss.fff"));
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("{0}: Запрос на подключение", DateTime.Now.ToString("HH:mm:ss.fff"));
                fw.Logon();
                Thread.Sleep(500);

                fw.ReadSocket();
                Thread.Sleep(500);

                //fw.TestRequest();
                /*
                Thread.Sleep(100);
                for (int i = 0; i < 50; i++)
                {
                    fw.ReadSocket();
                    Thread.Sleep(1000);
                }
                */

                fw.HeartBeat();

                //fw.SendRequestPosition();
                //Thread.Sleep(250);
                /*
                fw.Order("256.50", Operation.Buy, OrdType.Limit);
                Thread.Sleep(250);

                fw.ReadSocket();
                Thread.Sleep(250);

                fw.Order("269.00", Operation.Sell, OrdType.Limit);
                Thread.Sleep(250);
                fw.ReadSocket();
                */
                //fw.SendSecurityDefinition();


                for (int j = 0; j < 240; j++)
                {
                    fw.Order("256.50", Operation.Buy, OrdType.Limit);
                    fw.Order("269.50", Operation.Sell, OrdType.Limit);
                }
                
                //Thread.Sleep(500);
                
                for (int i = 0; i < 450; i++)
                {
                    fw.ReadSocket();
                    Thread.Sleep(200);
                    if (i % 25 == 0)
                    {
                        fw.HeartBeat();
                    }

                    //Console.WriteLine(NewRandom.rnd.Next(99999999));
                }

                /*
                while (true)
                {
                    fw.ReadSocket();
                    Thread.Sleep(5250);
                    fw.HeartBeat();
                }
                */

                fw.HeartBeat();
                Thread.Sleep(500);
                fw.ReadSocket();
                Console.WriteLine("{0}: Запрос на отключение", DateTime.Now.ToString("HH:mm:ss.fff"));
                fw.Logout();
                Thread.Sleep(500);
                fw.ReadSocket();
                Thread.Sleep(500);
                fw.SocketDisconnect();

                //fw.ShowMessage(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        private static void fw_onLogon(HeaderMessage hm)
        {
            Console.WriteLine("{0}: Принято сообщение на подключение.\n{1},{2},{3},{4}", DateTime.Now.ToString("HH:mm:ss.fff"), hm.SenderCompID, hm.TargetCompID, hm.MsgSeqNum, hm.SendingTime);
        }

        public static void fw_onLogout(HeaderMessage hm)
        {
            Console.WriteLine("{0}: Принято сообщение на отключение.\n{1},{2},{3},{4}", DateTime.Now.ToString("HH:mm:ss.fff"), hm.SenderCompID, hm.TargetCompID, hm.MsgSeqNum, hm.SendingTime);
        }

        public static void fw_onSocketConnect(Socket sSocket)
        {
            Console.WriteLine("{0} Сокет подключен.", DateTime.Now.ToString("HH:mm:ss.fff"));
        }

    }
}