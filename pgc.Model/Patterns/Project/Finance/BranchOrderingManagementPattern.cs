using System;
using pgc.Model.Enums;


namespace pgc.Model.Patterns
{
    [Serializable]
    public class BranchOrderingManagementPattern : BasePattern
    {
        public BranchOrderingManagementPattern()
        {
        }

        public long Branch_ID { get; set; }
        
        /// <summary>
        /// Can Order This Order Title
        /// </summary>
        public long OrderTitle_ID { get; set; }
    }
}