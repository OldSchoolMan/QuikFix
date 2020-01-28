using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikFix
{
    class OrderMessage
    {
        public string ClOrdID { get; set; }
        public string TransactTime { get; set; }
        public string Side { get; set; }
        public char HandlInst { get; set; }
        public string Price { get; set; }
        public int OrderQty { get; set; }
        public char OrdType { get; set; }
        public string IDSource { get; set; }
        public string Symbol { get; set; }
        public string Account { get; set; }

        public string ClientID { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        private Random rnd = new Random();

        public OrderMessage(string price, string side )
        {
            ClOrdID = DateTime.Now.ToString("HHmmssfff"); //  rnd.Next(10000).ToString()       
            Side = side;
            TransactTime = DateTime.Now.AddHours(-3).ToString("yyyyMMdd-HH:mm:ss.fff");
            OrdType = '2';
            Price = price;
            OrderQty = 10;
            HandlInst = '2';
            IDSource = "8";     //  100
            Symbol = "SBER";        //  SBER    ISIN RU0009029540   Сбербанк    RTS-3.20    RIH0
            Account = "L01+00000F00";
            ClientID = "1234";   //  1234 SPBFUT00086
        }

        //  https://forum.moex.com/viewtopic.asp?t=31826    USD USD000UTSTOM, USD000UTSTOD 
        //  8=FIX.4.49=19835=D34=849=MD902030000152=20160815-16:52:17.79056=IFIX-CUR-UAT1=MB902030284511=038=140=144=72.000052=20160815-19:52:17.79054=155=USDRUB_SPT60=20160815-19:52:17.790336=CETS386=1460=410=036

        public override string ToString()
        {
            MessageString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001{6}={7}\u0001{8}={9}\u0001{10}={11}\u0001{12}={13}\u0001{14}={15}\u0001{16}={17}\u0001{18}={19}\u0001{20}={21}\u0001",   // {18}={19}\u0001
                (int)Tags.ClOrdID,
                ClOrdID,
                (int)Tags.Side,
                Side,
                (int)Tags.TransactTime,
                TransactTime,
                (int)Tags.OrdType,
                OrdType,
                (int)Tags.Price,
                Price,
                (int)Tags.OrderQty,
                OrderQty,
                (int)Tags.HandlInst,
                HandlInst,
                (int)Tags.IDSource,
                IDSource,
                (int)Tags.Symbol,
                Symbol,
                (int)Tags.Account,
                Account,
                (int)Tags.ClientID,
                ClientID
                //167,
                //"FXSPOT"
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }
        //  Сообщение для отправки 8=FIX.4.29=7035=D34=149=TEST56=QUIK11=123454=140=240=25038=121=222=810=097
        public int GetMessageSize()
        {
            string tmpString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001{6}={7}\u0001{8}={9}\u0001{10}={11}\u0001{12}={13}\u0001{14}={15}\u0001{16}={17}\u0001{18}={19}\u0001{20}={21}\u0001",   //  {18}={19}\u0001
                (int)Tags.ClOrdID,
                ClOrdID,
                (int)Tags.Side,
                Side,
                (int)Tags.TransactTime,
                TransactTime,
                (int)Tags.OrdType,
                OrdType,
                (int)Tags.Price,
                Price,
                (int)Tags.OrderQty,
                OrderQty,
                (int)Tags.HandlInst,
                HandlInst,
                (int)Tags.IDSource,
                IDSource,
                (int)Tags.Symbol,
                Symbol,
                (int)Tags.Account,
                Account,
                (int)Tags.ClientID,
                ClientID
                //167,
                //"FXSPOT"
            );
            return tmpString.Length;
        }
    }
}
