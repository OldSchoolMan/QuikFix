using System;

namespace QuikFix
{
    class LogonMessage
    {
        public int EncryptMethod { get; set; }  //  метод шифрования сообщения
        public int HeartBtInt { get; set; }     //  интервал обмена сообщениями
        public bool ResetSeqNumFlag { get; set; }   //  флаг сброса счетчика сообщений
        public string MessageString { get; set; }
        public int MessageSize { get; set; }


        public LogonMessage(int encryptMethod, int heartBtInt, bool resetSeqNumFlag = true) //  было false
        {
            EncryptMethod = encryptMethod;
            HeartBtInt = heartBtInt;
            ResetSeqNumFlag = resetSeqNumFlag;
        }
        public override string ToString()   //  формирование строки сообщения
        {
            MessageString = String.Format("{0}={1}\u0001{2}={3}\u0001{4}={5}\u0001",
                (int)Tags.EncryptMethod,
                EncryptMethod.ToString(),
                (int)Tags.HearBitInt,
                HeartBtInt.ToString(),
                (int)Tags.ResetSeqNumFlag,
                ResetSeqNumFlag == true ? "Y" : "N"
                );
            MessageSize = MessageString.Length;
            return MessageString;
        }

        public int GetMessageSize()     //  подсчет длины строки сообщения
        {
            string tmpString = String.Format("98={0}\u0001108={1}\u0001141={2}\u0001",
                    EncryptMethod.ToString(),
                    HeartBtInt.ToString(),
                    ResetSeqNumFlag == true ? "Y" : "N");
            return tmpString.Length;
        }

    }
}

