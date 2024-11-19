using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models {
    public class CAttractionTicket {
        public int fAttractionTicketId { get; set; }
        public int fAttractionId { get; set; }
        public string fTicketType { get; set; }
        public decimal fPrice { get; set; }
        public string fDiscountInformation { get; set; }
        public DateTime fCreatedDate { get; set; }
    }
}
