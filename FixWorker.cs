using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace QuikFix
{
    class FixWorker
    {
        private Socket sSender;
        private int _messageCount = 1;   //  номер отправляемого сообщения
        public List<string> messagesReceive = new List<string>();
        public bool IsConnected = false;

        private string Server { get; set; }
        private int Port { get; set; }
        /// <summary>
        /// Разбитие сообщений на отдельные поля
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private List<string> GetFieldsFromMessage(string message)   //  разбираем сообщение на tag=value
        {
            List<string> result = new List<string>();
            foreach (string part in message.Split(new char[] { '\u0001' }))
            {
                result.Add(part);
            }
            return result;
        }
        /// <summary>
        /// разбивка поля на 2 значения (код поля и его значение)
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private List<string> GetValuesFromFields(string field)  //  разбираем строку tag=value на отдельные tag и value в [0] tsg, в [1] value
        {
            List<string> result = new List<string>();
            foreach (string part in field.Split(new char[] { '=' }))
            {
                result.Add(part);
            }
            return result;
        }
        /// <summary>
        /// Получение из сообщения значение заданного поля (Tag)
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetTagValueFromMessage(Tags tag, string message)
        {
            string result = "";
            string pattern = string.Format(@"{0}=(?<tagData>.*?)\u0001", Enum.Format(typeof(Tags), tag, "d"));
            MatchCollection matches = new Regex(pattern, RegexOptions.ExplicitCapture).Matches(message);
            if (matches.Count > 0)
                result = (matches[0].Groups["tagData"].Value);
            return result;
        }

        public FixWorker(string server, int port)
        {
            Server = server;
            Port = port;
        }

        public void SocketConnect()     //  ++
        {
            //Получаем ip сервера
            IPAddress ipAddr = IPAddress.Parse(Server);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, Port);

            //Создаем сокет для подключения
            sSender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Подключаемся
            try
            {
                sSender.Connect(ipEndPoint);
                if (sSender.Connected)
                {
                    Console.WriteLine("{0}: Сокет соединился с {1}", DateTime.Now.ToString("HH:mm:ss.fff"),
                        sSender.RemoteEndPoint.ToString());
                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //  http://www.cyberforum.ru/csharp-net/thread308481.html
        //  https://docs.microsoft.com/ru-ru/dotnet/framework/network-programming/using-an-asynchronous-client-socket
        public void ReadSocket()
        {
            //Получаем ответ от сервера
            byte[] bytes = new byte[1024];
            int bytesRec = 0;
            bytesRec = sSender.Receive(bytes);
            Console.WriteLine("{0}: <= {1}", DateTime.Now.ToString("HH:mm:ss.fff"), Encoding.UTF8.GetString(bytes, 0, bytesRec));    //  Ответ от сервера:

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
                string message = messagesReceive.Where(p => p.Contains(String.Format("\u0001{0}={1}", (int)Tags.MsgSeqNum, nMessage))).First();
                if (message.Length > 0)
                {
                    foreach (string field in GetFieldsFromMessage(message))
                    {
                        var values = GetValuesFromFields(field);
                        if (values.Count() > 1) //  или не проверять для быстродействия?
                        {
                            try
                            {
                                                                            //  зачем конвертировать, если только печатаем. может перейти на const?
                                Console.WriteLine(String.Format("{0}={1}", Enum.GetName(typeof(Tags), Convert.ToInt32(values[0])).ToString(), values[1].ToString()));   
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);     //  не ловим, т.к. последний разделитель
                            }
                        }
                    }
                }
            }
        }

        public void SocketDisconnect()
        {
            sSender.Disconnect(false);
            Console.WriteLine("{0}: Сокет отключился", DateTime.Now.ToString("HH:mm:ss.fff"));
        }
        public void Logon(int encryptMethod = 0, int heartBtInt = 5, bool resetSeqNumFlag = true) // 108=3000 это слишком много, обычно используется 90. Часто меньше.
        {
            //Создаем заголовок
            HeaderMessage msHeader = new HeaderMessage(SessionLevel.Logon, _messageCount++); //Тип сообщения на установку сессии "A"

            //Создаем сообщение на подключение onLogon
            LogonMessage msLogon = new LogonMessage(encryptMethod, heartBtInt, resetSeqNumFlag);

            //Вычисляем длину сообщения
            msHeader.BodyLength = msHeader.GetHeaderSize() + msLogon.GetMessageSize();  //  было msLogon.GetMessageSize()    ? MessageSize

            //Создаем концовку сообщения
            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msLogon.ToString()); //  ??? MessageString

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msLogon.ToString() + msTrailer.ToString();    //  ???  ToString()
            SendMessage(fullMessage);
        }

        public void Logout()
        {
            //Создаем заголовок
            HeaderMessage msHeader = new HeaderMessage(SessionLevel.Logout, _messageCount++);

            LogoutMessage msLogout = new LogoutMessage("Logout");
            msHeader.BodyLength = msHeader.GetHeaderSize() + msLogout.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msLogout.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msLogout.ToString() + msTrailer.ToString();
            SendMessage(fullMessage);
        }

        public void TestRequest()
        {
            //Создаем заголовок
            HeaderMessage msHeader = new HeaderMessage(SessionLevel.TestRequest, _messageCount++);

            TestRequest msTestRequest = new TestRequest();
            msHeader.BodyLength = msHeader.GetHeaderSize() + msTestRequest.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msTestRequest.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msTestRequest.ToString() + msTrailer.ToString();
            SendMessage(fullMessage);
        }

        public void HeartBeat()
        {
            HeaderMessage msHeader = new HeaderMessage(SessionLevel.HeartBeat, _messageCount++);

            HeartBeatMessage msHeartBeat = new HeartBeatMessage("CRT"); //  ???
            msHeader.BodyLength = msHeader.GetHeaderSize() + msHeartBeat.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msHeartBeat.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msHeartBeat.ToString() + msTrailer.ToString();
            SendMessage(fullMessage);
        }

        public void Order(string price, string operation, OrdType ordType)
        {
            HeaderMessage msHeader = new HeaderMessage(ApplicationLevel.NewOrderSingle, _messageCount++);

            OrderMessage msOrder = new OrderMessage(price, operation, ordType);

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msOrder.ToString());

            msHeader.BodyLength = msHeader.MessageSize + msOrder.MessageSize;  //  GetMessageSize()

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.MessageString + msOrder.MessageString + msTrailer.ToString();   // ToString() 
            SendMessage(fullMessage);
        }

        public void SendSecurityDefinition()
        {
            HeaderMessage msHeader = new HeaderMessage(ApplicationLevel.SecurityDefinitionRequest, _messageCount++);

            SecurityDefinition msSecurityDefinition = new SecurityDefinition();
            msHeader.BodyLength = msHeader.GetHeaderSize() + msSecurityDefinition.GetMessageSize();

            TrailerMessage msTrailer = new TrailerMessage(msHeader.ToString() + msSecurityDefinition.ToString());

            //Формируем полное готовое сообщение
            string fullMessage = msHeader.ToString() + msSecurityDefinition.ToString() + msTrailer.ToString();

            SendMessage(fullMessage);
        }

        void SendMessage(string fullMessage)
        {
            Console.WriteLine("{0}: => {1}", DateTime.Now.ToString("HH:mm:ss.fff"), fullMessage);   //  Сообщение для отправки
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

        public void SampleFields()
        {
            string str = "8=FIX.4.2\u00019=55\u000135=1\u000149=QQQQ\u000156=TTTT\u000134=2\u000152=20200126-10:43:13\u0001112=CRT\u000110=054\u0001";
            var res = GetFieldsFromMessage(str);
        }

        public void SampleTag()
        {
            string str = "56=TEST";
            var res = GetValuesFromFields(str);
        }

        public void SampleTagValue()
        {
            string str = "8=FIX.4.2\u00019=55\u000135=1\u000149=QQQQ\u000156=TTTT\u000134=2\u000152=20200126-10:43:13\u0001112=CRT\u000110=054\u0001";
            var res = GetTagValueFromMessage(Tags.SenderCompID, str);
        }

        public void SampleReadSocket()
        {
            
            string str2 = "8=FIX.4.29=23335=849=QQQQ56=TTTT34=352=20200126-16:20:3137=011=123417=9lgUQrcpkj20=0150=839=81=123422=10055=USDRUB_TOM54=138=1151=014=040=232=031=0.0000006=060=20200126-16:20:31.12421=258=Specified security is not found []10=056";

            foreach (Match mat in Regex.Matches(str2, @"8=FIX.*", RegexOptions.IgnoreCase))
            {
                var m = mat.Value;
                var match = mat;

                var beginString = GetTagValueFromMessage(Tags.BeginString, mat.Value);

                var senderCompID = GetTagValueFromMessage(Tags.SenderCompID, mat.Value);
                var targetCompID = GetTagValueFromMessage(Tags.TargetCompID, mat.Value);
                var msgSeqNum = Convert.ToInt32(GetTagValueFromMessage(Tags.MsgSeqNum, mat.Value));
                ;
                //HeaderMessage msHeader = new HeaderMessage(msgType.ToString(), _messageCount)
                //{
                //    BeginString = GetTagValueFromMessage(Tags.BeginString, mat.Value),

                //    SenderCompID = GetTagValueFromMessage(Tags.SenderCompID, mat.Value),
                //    TargetCompID = GetTagValueFromMessage(Tags.TargetCompID, mat.Value),
                //    MsgSeqNum = Convert.ToInt32(GetTagValueFromMessage(Tags.MsgSeqNum, mat.Value)),
                //};
                var mT = GetTagValueFromMessage(Tags.MsgType, mat.Value);
                var msgType = GetTagValueFromMessage(Tags.MsgType, mat.Value)[0].ToString();

                switch (msgType)
                {
                    case "a": //(char)Messages.Logon:
                        //if (OnLogon != null)
                        //    OnLogon(msHeader);
                        Console.WriteLine("Logon");
                        break;
                    case "b": // (char)Messages.Logout:
                        //if (OnLogout != null)
                        //    OnLogout(msHeader);
                        Console.WriteLine("Logout");
                        break;
                    case SessionLevel.TestRequest:
                        Console.WriteLine("TestRequest");
                        break;
                    case ApplicationLevel.ExecutionReport:
                        Console.WriteLine("ExecutionReport");
                        break;
                }
                messagesReceive.Add(mat.Value);
            }
        }

        public void SampleShowMessage()
        {
            string str3 = "8=FIX.4.29=43135=849=QQQQ56=TTTT34=1352=20200129-18:48:4637=200129-TQBR-1883721576311=214814305109=123417=200129-TQBR-KYp2Kh-4-EXTQx20=0150=439=41=L01+00000F0022=848=SBER55=Сбербанк100=MOEX207=MOEX167=CS54=138=10152=2505.00151=014=040=244=250.5015=SUR59=032=031=0.0000006=060=20200129-21:48:14.54221=258=1234//5018=MC01234000005015=8845017=-3:00:00453=1448=MC0123400000447=D452=135002=188372157635060=510=046";

            messagesReceive.Add(str3);
            ShowMessage(13);
        }

        public void CheckSum()
        {
            string str = "8=FIX.4.29=6935=A34=149=TTTT52=20200203-19:34:13.16656=QQQQ98=0108=30141=Y10=102";

            string str2 = "";

            int sumChar = 0;
            for (int i = 0; i < str2.Length; i++)
                sumChar += (int)str2[i];
            var res = String.Format("10={0}\u0001", Convert.ToString(sumChar % 256).PadLeft(3).Replace(" ", "0"));
        }
    }
}
