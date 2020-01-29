using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuikFix
{
    class FixWorker
    {
        private Socket sSender;
        private int _messageCount = 1;   //  номер отправляемого сообщения
        public List<string> messagesReceive = new List<string>();

        private string Server { get; set; }
        private int Port { get; set; }
        private List<string> GetFieldsFromMessage(string message)
        {
            List<string> result = new List<string>();
            foreach (string part in message.Split(new char[] { '\u0001' }))
                result.Add(part);
            return result;
        }
        private string GetTagValueFromMessage(Tags tag, string message)
        {
            string result = "";
            string pattern = string.Format(@"{0}(?<tagdata>.*?)\u0001", Enum.Format(typeof(Tags), tag, "d"));
            MatchCollection matches = new Regex(pattern, RegexOptions.ExplicitCapture).Matches(message);
            if (matches.Count > 0)
                result = (matches[0].Groups["tagData"].Value);
            return result;
        }
        private List<string> GetValuesFromFields(string field)
        {
            List<string> result = new List<string>();
            foreach (string part in field.Split(new char[] { '=' }))
            {
                result.Add(part);
            }
            return result;
        }
        public FixWorker(string server, int port)
        {
            Server = server;
            Port = port;
        }

        public void ReadSocket()
        {
            //Получаем ответ от сервера
            byte[] bytes = new byte[1024];
            int bytesRec = 0;
            bytesRec = sSender.Receive(bytes);
            Console.WriteLine("{0}: Ответ от сервера: {1}", DateTime.Now.ToString("HH:mm:ss.fff"), Encoding.UTF8.GetString(bytes, 0, bytesRec));

            #region пока не работает
            /*
            if (bytesRec > 0)
            {
                //Разбираем список на отдельные сообщения
                foreach (Match mat in Regex.Matches(Encoding.UTF8.GetString(bytes, 0, bytesRec), @"8=FIX.*", RegexOptions.IgnoreCase))
                {
                    HeaderMessage msHeader = new HeaderMessage
                    {
                        BeginString = GetTagValueFromMessage(Tags.BeginString, mat.Value),
                        MsgType = GetTagValueFromMessage(Tags.MsgType, mat.Value)[0],
                        SenderCompID = GetTagValueFromMessage(Tags.SenderCompID, mat.Value),
                        TargetCompID = GetTagValueFromMessage(Tags.TargetCompID, mat.Value),
                        MsgSeqNum = Convert.ToInt32(GetTagValueFromMessage(Tags.MsgSeqNum, mat.Value)),
                    };
                    switch (GetTagValueFromMessage(Tags.MsgType, mat.Value)[0])
                    {
                        case (char)Messages.Logon:
                            if (OnLogon != null) 
                                OnLogon(msHeader);
                            break;
                        case (char)Messages.Logout:
                            if (OnLogout != null)
                                OnLogout(msHeader);
                            break;
                    }
                    messagesReceive.Add(mat.Value);
                }
            }
            */
            #endregion
        }

        public void ShowMessage(int nMessage)   //  пока не работает
        {
            if (messagesReceive.Count > 0)
            {
                /*
                string message = messagesReceive.Where(p => p.Contains(String.Format("\u0001{0}={1}", (int)Tags.MsgSeqNum, nMessage))).First();
                if (message.Length > 0)
                {
                    foreach (string field in GetFieldsFromMessage(message))
                    {
                        var values = GetValuesFromFields(field);
                        try
                        {
                            Console.WriteLine(String.Format("{0}={1}", Enum.GetName(typeof(Tags), Convert.ToInt32(values[0])).ToString(), values[1].ToString()));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
                */
            }
        }

        public void SocketConnect()     //  ++
        {
            //Получаем ip сервера
            IPAddress ipAddr = IPAddress.Parse(Server);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, Port);

            //Создаем сокет для подключения
            sSender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Подключаемся
            sSender.Connect(ipEndPoint);
            Console.WriteLine("{0}: Сокет соединился с {1}", DateTime.Now.ToString("HH:mm:ss.fff"), sSender.RemoteEndPoint.ToString());
        }

        public void SocketDisconnect()
        {
            sSender.Disconnect(false);
            Console.WriteLine("{0}: Сокет отключился", DateTime.Now.ToString("HH:mm:ss.fff"));
        }
        public void Logon(int encryptMethod = 0, int heartBtInt = 5, bool resetSeqNumFlag = true) // 108=3000 это слишком много, обычно используется 90. Часто меньше.
        {
            //Создаем заголовок
            HeaderMessage msHeader = new HeaderMessage("A", _messageCount++); //Тип сообщения на установку сессии "A"
            
            //Создаем сообщение на подключение onLogon
            LogonMessage msLogon = new LogonMessage(encryptMethod, heartBtInt, resetSeqNumFlag);

            #region 
            /*
            {
                EncryptMethod = 0,
                HeartBtInt = 3000,
                ResetSeqNumFlag = true
            };
            */
            #endregion

            //  8=FIX.4.29=4835=A34=149=TEST56=QUIK98=0108=90141=N10=206
            //Вычисляем длину сообщения
            msHeader.BodyLength = msHeader.GetHeaderSize() + msLogon.GetMessageSize();  //  было msLogon.GetMessageSize()    ? MessageSize
            
            //Создаем концовку сообщения
            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msLogon.ToString()); //  ??? MessageString

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msLogon.ToString() + msTrailer.ToString();    //  ???
            SendMessage(fullMessage);
        }

        public void Logout()
        {
            //  8=FIX.4.2‡9=54‡35=5‡34=20‡49=REMOTE‡52=20120330-19:23:20‡ 56=TT_ORDER‡10=134‡
            //  8=FIX.4.3^9=84^35=5^49=SellSide^56=BuySide^34=3^52=20190606-09:30:03.796^58=Logout acknowledgement^10=050^

            // 8 = FIX.4.29 = 4035 = 534 = 149 = TEST56 = QUIK58 = Logout10 = 158
            //Создаем заголовок
            HeaderMessage msHeader = new HeaderMessage("5", _messageCount++);

            LogoutMessage msLogout = new LogoutMessage("Logout");
            msHeader.BodyLength = msHeader.GetHeaderSize() + msLogout.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msLogout.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msLogout.ToString() + msTrailer.ToString();
            Console.WriteLine("Сообщение для отправки {0}", fullMessage);
            byte[] msg = Encoding.UTF8.GetBytes(fullMessage);   // 
            //Отправляем сообщение
            int bytesSent = sSender.Send(msg);
            Console.WriteLine("Отправил {0} байт", bytesSent.ToString());
        }

        public void HeartBeat()
        {
            HeaderMessage msHeader = new HeaderMessage("0", _messageCount++);

            HeartBeatMessage msHeartBeat = new HeartBeatMessage("CRT");
            msHeader.BodyLength = msHeader.GetHeaderSize() + msHeartBeat.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msHeartBeat.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msHeartBeat.ToString() + msTrailer.ToString();
            Console.WriteLine("Сообщение для отправки {0}", fullMessage);
            byte[] msg = Encoding.UTF8.GetBytes(fullMessage);   // 
            //Отправляем сообщение
            int bytesSent = sSender.Send(msg);
            Console.WriteLine("Отправил {0} байт", bytesSent.ToString());
        }

        public void Order(string price, string side, char ordType)
        {
            HeaderMessage msHeader = new HeaderMessage("D", _messageCount++);

            OrderMessage msOrder = new OrderMessage(price, side, ordType);
            msHeader.BodyLength = msHeader.GetHeaderSize() + msOrder.GetMessageSize();
            
            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msOrder.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msOrder.ToString() + msTrailer.ToString();
            Console.WriteLine("Сообщение для отправки {0}", fullMessage);
            byte[] msg = Encoding.UTF8.GetBytes(fullMessage);   // 
            //Отправляем сообщение
            int bytesSent = sSender.Send(msg);
            Console.WriteLine("Отправил {0} байт", bytesSent.ToString());
        }

        public void SendSecurityDefinition()
        {
            HeaderMessage msHeader = new HeaderMessage("c", _messageCount++);

            SecurityDefinition msSecurityDefinition = new SecurityDefinition();
            msHeader.BodyLength = msHeader.GetHeaderSize() + msSecurityDefinition.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msSecurityDefinition.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msSecurityDefinition.ToString() + msTrailer.ToString();

            SendMessage(fullMessage);
        }

        void SendMessage(string fullMessage)
        {
            
            Console.WriteLine("{0}: Сообщение для отправки {1}", DateTime.Now.ToString("HH:mm:ss.fff"), fullMessage);
            byte[] msg = Encoding.UTF8.GetBytes(fullMessage);   // 
            //Отправляем сообщение
            int bytesSent = sSender.Send(msg);
            
            //Console.WriteLine("Отправил {0} байт", bytesSent.ToString());

        }

        public void SendRequestPosition()
        {
            HeaderMessage msHeader = new HeaderMessage("AN", _messageCount++);

            RequestPositions msRequestPositions = new RequestPositions();
            msHeader.BodyLength = msHeader.GetHeaderSize() + msRequestPositions.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msRequestPositions.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msRequestPositions.ToString() + msTrailer.ToString();

            SendMessage(fullMessage);

        }

        public delegate void LogonHandler(HeaderMessage header);
        public event LogonHandler OnLogon;
        
        internal void OnLogonCall(HeaderMessage header)
        {
            OnLogon?.Invoke((header));
        }

        delegate void LogoutHandler(HeaderMessage header);
        event LogoutHandler OnLogout;

        internal void OnLogoutCall(HeaderMessage header)
        {
            OnLogout?.Invoke((header));
        }
    }
}
