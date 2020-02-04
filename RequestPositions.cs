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
        public string PartyRole { get; set; }
        public string PartyID { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public RequestPositions()
        {
            PosReqID = DateTime.Now.ToString("HHmmssfff");
            PosReqType = "0";
            SubscriptionRequestType = "1";  //  «0» — SNAPSHOT; «1» — SNAPSHOT_PLUS_UPDATES;
            PartyRole = "3";    //  «3» — ClientID;
            PartyID = "1234";   //  Код клиента для денежных и бумажных лимитов;
        }

        public override string ToString()   //  формирование строки сообщения
        {
            MessageString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001",    //  {6}={7}\u0001{8}={9}\u0001
                (int)Tags.PosReqID,
                PosReqID.ToString(),
                (int)Tags.PosReqType,
                PosReqType.ToString(),
                (int)Tags.SubscriptionRequestType,
                SubscriptionRequestType
                //(int)Tags.PartyID,
                //PartyID.ToString(),
                //(int)Tags.PartyRole,
                //PartyRole.ToString()
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()     //  подсчет длины строки сообщения
        {
            string tmpString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001",   //  {6}={7}\u0001{8}={9}\u0001
                (int)Tags.PosReqID,
                PosReqID.ToString(),
                (int)Tags.PosReqType,
                PosReqType.ToString(),
                (int)Tags.SubscriptionRequestType,
                SubscriptionRequestType
                //(int)Tags.PartyID,
                //PartyID.ToString(),
                //(int)Tags.PartyRole,
                //PartyRole.ToString()
                );
            return tmpString.Length;
        }
    }
}
