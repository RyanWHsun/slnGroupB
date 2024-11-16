using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class COrder
    {
        public int fUserId {  get; set; }
        public DateTime fOrderDate {  get; set; }
        public String fShipAddress { get; set; }
        public int fProductId { get; set; }
        public decimal fUnitPrice { get; set; }
        public int fQrderQty { get; set; }
        public int fOrderStatusId { get; set; }
        public string fProductName { get; set; }
        public decimal fTotalPrice { get; set; }
    }
}
