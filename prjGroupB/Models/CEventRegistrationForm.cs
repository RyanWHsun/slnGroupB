using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CEventRegistrationForm
    {
        public int fEventIdfEventRegistrationFormId { get; set; }
        public int fEventId { get; set; }

        // 會員ID
        public int fUserId { get; set; }

        // 報名人數
        public int fEventRegistrationCount { get; set; }

        // 聯絡人
        public string fEventContact { get; set; }

        // 連絡人電話
        public string fEventContactPhone { get; set; }

        // 報名日期
        public DateTime fEregistrationDate { get; set; }
    }
}