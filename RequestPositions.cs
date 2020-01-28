using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikFix
{
    class RequestPositions
    {
        public string PosReqType { get; set; }
        public string PosReqID { get; set; }
        public string SubscriptionRequestType { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public RequestPositions()
        {
            PosReqID = DateTime.Now.ToString("HHmmssfff");
            PosReqType = "0";
            SubscriptionRequestType = "0";
        }


        public override string ToString()   //  формирование строки сообщения
        {
            MessageString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001",
                (int)Tags.PosReqID,
                PosReqID.ToString(),
                (int)Tags.PosReqType,
                PosReqType.ToString(),
                (int)Tags.SubscriptionRequestType,
                SubscriptionRequestType
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()     //  подсчет длины строки сообщения
        {
            string tmpString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001",
                (int)Tags.PosReqID,
                PosReqID.ToString(),
                (int)Tags.PosReqType,
                PosReqType.ToString(),
                (int)Tags.SubscriptionRequestType,
                SubscriptionRequestType
                );
            return tmpString.Length;
        }
    }
}
