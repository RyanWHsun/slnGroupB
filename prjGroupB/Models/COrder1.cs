using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models.Namespace.B
{
    public partial class COrder
    {
        public int fOrderId { get; set; }
        public string fUserName { get; set; }
        public DateTime fOrderDate { get; set; }
        public String fShipAddress { get; set; }
        public int fOrderStatusId { get; set; }
        public String fStatusName { get; set; }        
        public decimal fTotalAmount { get; set; }
    }
}
