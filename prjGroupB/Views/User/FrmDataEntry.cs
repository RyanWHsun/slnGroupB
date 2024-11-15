using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace prjGroupB.Views.User
{
    public partial class FrmDataEntry : Form
    {

        public DialogResult _isok {  get; set; }
        
        private CUser _user;
        //弄個_user出來，型別CUser

        public FrmDataEntry()
        {
            InitializeComponent();
        }

        public CUser UserEntry
        {//存輸入的數
            get
            {
                if (_user == null)
                { _user = new CUser(); }

                _user.fUserId = Convert.ToInt32(txtId.Text);
                _user.fUserRankId = 1;//大家初始都是1
                _user.fUserName = txtUserName.Text;
                //_user.fUserImage = pictureBox1.Image;
                _user.fUserNickName = txtNickName.Text;
                _user.fUserSex = CboxUserSex.Text;
                _user.fUserBirthday = dateTimeUserBirthday.Value;
                _user.fUserPhone = Convert.ToInt32(txtUserPhone.Text);
                _user.fUserEmail = txtUserEmail.Text;
                _user.fUserAddress = txtUserAddress.Text;
                _user.fUserComeDate = DateTime.Now;
                _user.fUserPassword = txtUserPassword.Text;
                //_user.fUserNotify = 1;
                //_user.fUserOnLine = 1;

                return _user;

            }
            set
            {
                _user = value;
                txtId.Text = _user.fUserId.ToString();
                txtUserName.Text = _user.fUserName;
                txtNickName.Text = _user.fUserNickName;
                CboxUserSex.Text = _user.fUserSex;
                dateTimeUserBirthday.Value = _user.fUserBirthday;
                txtUserPhone.Text = _user.fUserPhone.ToString();
                txtUserEmail.Text = _user.fUserEmail;
                txtUserAddress.Text = _user.fUserAddress;
                txtUserPassword.Text = _user.fUserPassword;

                if (_user.fUserImage != null)
                {
                    try
                    {
                        Stream streamImage = new MemoryStream(_user.fUserImage);
                        pictureBox1.Image = Bitmap.FromStream(streamImage);
                    }
                    catch { }
                }
            }
        }



        private void OKToCreate_Click(object sender, EventArgs e)
        {
            this._isok = DialogResult.OK;
            Close();
        }

        private void CancelCreate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmDataEntry_Load(object sender, EventArgs e)
        {//預設圖片
            pictureBox1.Image = Bitmap.FromFile(@"C:\GroupB\userimage\smill.png");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {//更改圖片
            openFileDialog1.Filter = "頭像|*.jpg|頭像|*.png";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            { return; }
            pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);

            FileStream imgStram = new FileStream(openFileDialog1.FileName,FileMode.Open,FileAccess.Read);
            
            BinaryReader reader = new BinaryReader(imgStram);
            this.UserEntry.fUserImage = reader.ReadBytes((int)imgStram.Length);

            reader.Close();
            imgStram.Close();


        }
    }
}
