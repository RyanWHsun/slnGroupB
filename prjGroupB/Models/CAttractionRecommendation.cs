using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models {
    public class CAttractionRecommendation {
        public int fAttractionRecommendationId { get; set; }
        public int fAttractionId { get; set; }
        public int fRecommendationId {  get; set; }
        public string fReason { get; set; }
        public DateTime fCreatedDate { get; set; }
    }
}
