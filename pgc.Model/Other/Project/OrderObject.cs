using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Other.Project
{
    [Serializable]

    public class OrderObject
    {
        public long ID { get; set; }

        public string Address { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }

        public string Date { get; set; }

        public string BranchTitle { get; set; }
        public string RefNum { get; set; }
        public string OrderStatus { get; set; }
        public bool IsPaid { get; set; }

        public string Comment { get; set; }
        public long PayableAmount { get; set; }

        public long UserID { get; set; }
        public int PaymentType { get; set; }
        public long? Branch_ID { get; set; }
        public Dictionary<long, int> Details { get; set; }

        public List<OrderDetailObject> OrderDetails { get; set; }
        public OrderObject()
        {
            this.Details = new Dictionary<long, int>();
            this.OrderDetails = new List<OrderDetailObject>();
            this.IsPaid = false;
        }
    }
}
