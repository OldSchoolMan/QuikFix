using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikFix
{
    class HeartBeatMessage
    {
        public string TestReqID { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public HeartBeatMessage(string text)
        {
            TestReqID = text;
        }

        public override string ToString()
        {
            MessageString = String.Format("{0}={1}\u0001",
                (int)Tags.TestReqID,
                TestReqID
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()
        {
            string tmpString = String.Format("112={0}\u000134",
                TestReqID);
            return tmpString.Length;
        }
    }
}
