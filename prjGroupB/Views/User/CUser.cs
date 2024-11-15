using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Views.User
{
    public class CUser
    {
        public int fUserId {  get; set; }
        public int fUserRankId { get; set; }
        public string fUserName { get; set; }
        public byte[] fUserImage {  get; set; }
        public string fUserNickName { get; set; }
        public string fUserSex { get; set; }
        public DateTime fUserBirthday { get; set; }
        public int fUserPhone {  get; set; }
        public string fUserEmail {  get; set; }
        public string fUserAddress {  get; set; }
        public DateTime fUserComeDate {  get; set; }
        public string fUserPassword {  get; set; }


        public bool fUserNotify { get; set; }
        public bool fUserOnLine {  get; set; }

    }
}
