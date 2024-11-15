using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Views.User
{
    public class CUserManager
    {
        public void creatUser(CUser p)
        {//在CUserManager中建creatUser
            string sql = "Insert into tUser(";
            sql += "fUserRankId,";
            sql += "fUserName,";
            sql += "fUserImage,";
            sql += "fUserNickName,";
            sql += "fUserSex,";
            sql += "fUserBirthday,";
            sql += "fUserPhone,";
            sql += "fUserEmail,";
            sql += "fUserAddress,";
            sql += "fUserComeDate,";
            sql += "fUserPassword,";
            sql += "fUserComeDate";

            sql += ")Values(";

            sql += "@K_fName,"; 
            sql += "@K_fPhone,";
            sql += "@K_fEmail,";
            sql += "@K_fAddress,";
            sql += "@K_fPassword)";



        }





    }
}
