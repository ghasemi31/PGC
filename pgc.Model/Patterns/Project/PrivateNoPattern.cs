using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class PrivateNoPattern:BasePattern
    {
        public PrivateNoPattern()
        {
            Number = "";
        }

        public PrivateNoStatus Status { get; set; }
        public long ID { get; set; }
        public string Number { get; set; }
    }
}