using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CProductDisplay
    {
        public int fSellerId { get; set; }
        public int fProductId { get; set; }
        public string fCategoryName { get; set; }
        public string fProductName { get; set; }
        public string fProductDescription { get; set; }        
        public decimal fPrice { get; set; }
        public int fStock { get; set; }
        public byte[] fProductImage1 { get; set; }
        public byte[] fProductImage2 { get; set; }
        public byte[] fProductImage3 { get; set; }
        
    }
}
