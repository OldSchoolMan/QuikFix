﻿using System;

namespace QuikFix
{
    class HeaderMessage
    {
        public string BeginString { get; set; }
        public int BodyLength { get; set; }
        public string MsgType { get; set; }
        public string SenderCompID { get; set; }
        public string TargetCompID { get; set; }
        public int MsgSeqNum { get; set; }
        public DateTime SendingTime { get; set; }
        public string MessageString { get; set; }
        public int MessageSize { get; set; }

        public HeaderMessage(string msgType, int msgSeqNum)
        {
            BeginString = "FIX.4.2";
            MsgType = msgType;
            SendingTime = DateTime.Now;
            SenderCompID = "T";
            TargetCompID = "Q";
            MsgSeqNum = msgSeqNum;
        }

        public override string ToString()   //  формирование строки заголовка
        {
            MessageString = String.Format("8={0}\u00019={1}\u000135={2}\u000134={3}\u000149={4}\u000152={5}\u000156={6}\u0001",
                BeginString,
                BodyLength.ToString(),
                MsgType,
                MsgSeqNum.ToString(),
                SenderCompID,
                SendingTime.AddHours(-3).ToString("yyyyMMdd-HH:mm:ss.fff"),
                TargetCompID
                );
            MessageSize = MessageString.Length; //  для HeaderMessage MessageSize не использовать!
            return MessageString;
        }

        public int GetHeaderSize()  //  подсчет длины строки заголовка без первых двух полей
        {
            string tmpString = String.Format("35={0}\u000156={1}\u000149={2}\u000134={3}\u000152={4}\u0001",
                MsgType,
                SenderCompID,
                TargetCompID,
                MsgSeqNum.ToString(),
                SendingTime.AddHours(-3).ToString("yyyyMMdd-HH:mm:ss.fff")
                );
            return tmpString.Length;
        }
    }
}
