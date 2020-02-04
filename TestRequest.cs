using System;

namespace QuikFix
{
    class TestRequest
    {
        public string TestReqID { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public TestRequest ()
        {
            TestReqID = NewRandom.Rnd.Next(999999).ToString();
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
            string tmpString = String.Format("112={0}\u0001",
                TestReqID);
            return tmpString.Length;
        }
    }
}
