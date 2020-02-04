using System;

namespace QuikFix
{
    class SecurityDefinition
    {
        public string SecurityReqID { get; set; }
        public string SecurityReqType { get; set; }
        public string IDSource { get; set; }
        public string Symbol { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public SecurityDefinition()
        {
            SecurityReqID = "12345";
            SecurityReqType = "0";
            IDSource = "8";
            Symbol = "SBER";
        }

        public override string ToString()
        {
            MessageString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001{6}={7}\u0001",   // {18}={19}\u0001
                (int)Tags.SecurityReqID,
                SecurityReqID,
                (int)Tags.SecurityReqType,
                SecurityReqType,
                (int)Tags.IDSource,
                IDSource,
                (int)Tags.Symbol,
                Symbol
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()
        {
            string tmpString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001{6}={7}\u0001",   // {18}={19}\u0001
                (int)Tags.SecurityReqID,
                SecurityReqID,
                (int)Tags.SecurityReqType,
                SecurityReqType,
                (int)Tags.IDSource,
                IDSource,
                (int)Tags.Symbol,
                Symbol
            );
            return tmpString.Length;
        }

    }
}
