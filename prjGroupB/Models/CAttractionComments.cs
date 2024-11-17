using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models {
    public class CAttractionComments {
        public int fCommentId {  get; set; }
        public int fAttracitonId {  get; set; }
        public int fUserId {  get; set; }
        public int fRating {  get; set; }
        public string fComment {  get; set; }
        public DateTime fCreatedDate { get; set; }
    }
}
