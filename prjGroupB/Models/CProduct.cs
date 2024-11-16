using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CProduct
    {
        public int fProductId { get; set; }
        public int fUserId { get; set; }
        public int fProductCategoryId { get; set; }

        public string fProductName { get; set; }

        public string fProductDescription { get; set; }

        public decimal fProductPrice { get; set; }

        public bool fIsOnSales { get; set; }

        public string fProductDateAdd { get; set; }

        public string fProductUpdated { get; set; }

        public int fStock { get; set; }
               
    }


}

