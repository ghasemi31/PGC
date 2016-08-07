using System;
using pgc.Model.Enums;
using System.Linq;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class MessagePattern:BasePattern
    {
        public int SMSCount { get; set; }
        public int CharCount { get; set; }
        public string Body { get; set; }
        public Enums.MessageType MessageType { get; set; }

        private const int FaUnitCount = 70;
        private const int EnUnitCount = 160;

        private const int MinFarsiCharCode = 1548;
        private const int MaxFarsiCharCode = 1790;

        public static MessageType RetriveMessageType(string txt)
        {
            foreach (char C in txt)
                if (C >= MinFarsiCharCode && C <= MaxFarsiCharCode)
                    return MessageType.Persian;
            return MessageType.Latin;
        }

        public static int RetriveSMSCount(int CharCount, MessageType Type)
        {
            if (Type == MessageType.Persian)
                return CharCount / FaUnitCount + 1;
            else
                return CharCount / EnUnitCount + 1;
        }

        public static int RetriveCharCount(string txt)
        {
            int EnterCount = txt.Where(S => S.CompareTo(Convert.ToChar(13)) == 0).Count();
            return txt.Length - EnterCount;
        }

    }
}