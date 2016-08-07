using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchOrderShipmentStatePattern : BasePattern
    {
        public BranchOrderShipmentStatePattern()
        {
        }

        public long ID { get; set; }       
    }
}