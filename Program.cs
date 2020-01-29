using System;
using System.Net.Sockets;
using System.Threading;

namespace QuikFix
{
    class Program
    {
        // запрос маркет даты 8=FIXT.1.1.9=85.35=V.1128=9.49=SimpleClient.56=MOEX.34=2.52=20141113-10:04:49.1180=OLR.1182=1.1183=7.10=054.
        static void Main(string[] args)
        {
            // 8=FIX.4.4;9=78;35=A;49=FG;56=tgFhcfx901U05;34=1;52=20160212-11:42:51.812;98=0;108=3000;141=Y;10=047;

            string str = "8=FIX.4.2\u00019=55\u000135=1\u000149=QUIK\u000156=TEST\u000134=2\u000152=20200126-10:43:13\u0001112=CRT\u000110=054\u0001";

            var fw = new FixWorker("", 0);  //  127.0.0.1   8001

            fw.OnLogon += fw_onLogon;
            fw.OnLogon += fw_onLogout;
            try
            {
                fw.SocketConnect();
                Console.WriteLine("{0}: Запрос на подключение", DateTime.Now.ToString("HH:mm:ss.fff"));
                fw.Logon();
                Thread.Sleep(250);
                
                //fw.ReadSocket();
                Thread.Sleep(250);
                
                fw.HeartBeat();
                Thread.Sleep(250);
                
                /*
                fw.SendRequestPosition();
                Thread.Sleep(250);
                while (true)
                {
                    fw.ReadSocket();
                    Thread.Sleep(5);
                }
                */
                
                //fw.Order("250.50", "1", '2');
                Thread.Sleep(100);

                //fw.ReadSocket();
                Thread.Sleep(100);

                //fw.Order("264.00", "1", '2');
                Thread.Sleep(100);
                

                //fw.SendSecurityDefinition();

                /*
                for (int i =0; i<5; i++)
                {
                    fw.Order("277.00", "1");
                    fw.ReadSocket();
                    fw.Order("292.00", "2");
                    fw.ReadSocket();
                    //Console.WriteLine("{0}: Запрос на чтение", DateTime.Now.ToString("HH:mm:ss.fff"));
                    //fw.ReadSocket();
                    //Thread.Sleep(1000);
                }
                */
                //fw.ReadSocket();
                Thread.Sleep(1000);
                /*
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(5000);
                    fw.ReadSocket();
                    fw.HeartBeat();
                }
                */
                fw.ReadSocket();

                Console.WriteLine("{0}: Запрос на отключение", DateTime.Now.ToString("HH:mm:ss.fff"));
                fw.Logout();
                Thread.Sleep(250);
                //fw.ReadSocket();
                Thread.Sleep(250);
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


