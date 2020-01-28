using System;

namespace QuikFix
{
    class LogoutMessage
    {
        public string Text { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public LogoutMessage(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            MessageString = String.Format("{0}={1}\u0001",
                (int)Tags.Text,
                Text
            );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()
        {
            string tmpString = String.Format("58={0}\u000134",
                Text);
            return tmpString.Length;
        }
    }
}
