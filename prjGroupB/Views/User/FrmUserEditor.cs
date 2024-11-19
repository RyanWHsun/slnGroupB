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
    public partial class FrmUserEditor : Form
    {

        public DialogResult _isok {  get; set; }
        
        private CUser _user;

        public CUser user { get 
            {
                if ( _user ==null)
                {
                    _user = new CUser();
                }
                _user.fUserId = Convert.ToInt32(txtId.Text);
                _user.fUserRankId = Convert.ToInt32(CboxRank.SelectedItem);//下拉式選單(但沒問題
                _user.fUserName = txtUserName.Text;
                _user.fUserNickName = txtNickName.Text;
                 _user.fUserSex = (String)CboxUserSex.SelectedItem;//下拉式選單
                _user.fUserBirthday = DateTime.Parse(dateTimeUserBirthday.Text);//日期選單
                _user.fUserPhone = Convert.ToInt32(txtUserPhone.Text);
                _user.fUserEmail = txtUserEmail.Text;
                _user.fUserAddress = txtUserAddress.Text;
                _user.fUserComeDate = DateTime.Now;
                _user.fUserPassword = txtUserPassword.Text;
                _user.fUserNotify = chkNotify.Checked;
                _user.fUserOnLine = chkOnLine.Checked;

                return _user;
            }
            set
            {
                _user = value;
                txtId.Text =  _user.fUserId.ToString();
                CboxRank.SelectedItem = _user.fUserRankId;
                txtUserName.Text = _user.fUserName;
                txtNickName.Text = _user.fUserNickName;
                CboxUserSex.SelectedItem = _user.fUserSex;
                dateTimeUserBirthday.Text = _user.fUserBirthday.ToString();
                txtUserPhone.Text = _user.fUserPhone.ToString();
                txtUserEmail.Text = _user.fUserEmail;
                txtUserAddress.Text = _user.fUserAddress;
                txtUserPassword.Text = _user.fUserPassword;
                chkNotify.Checked = _user.fUserNotify;
                chkOnLine.Checked = _user.fUserOnLine;

                if(_user.fUserImage != null)
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

        public FrmUserEditor()
        {
            InitializeComponent();
        }

        



        private void OKToCreate_Click(object sender, EventArgs e)
        {
            string message = "";
            if (string.IsNullOrEmpty(CboxRank.Text))
                message += "\n請選擇該用戶的rank，1:一般用戶 99: 管理員";
            if (string.IsNullOrEmpty(CboxUserSex.Text))
                message += "\n性別不可為空";
            if (string.IsNullOrEmpty(txtUserPhone.Text))
                message += "\n電話不可為空";
            if (string.IsNullOrEmpty(txtUserEmail.Text))
                message += "\n電子信箱不可為空";
            if (string.IsNullOrEmpty(txtUserPassword.Text))
                message += "\n密碼不可為空";
            else
            {
                if (!isNumber(txtUserPhone.Text))
                {
                    message += "\n電話須為數字";
                }
            }
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }
            this._isok = DialogResult.OK;
            Close();
        }

        private bool isNumber(string p)
        {
            try
            {
                double d = Convert.ToDouble(p);
                return true;
            }catch { return false; }
        }

        private void CancelCreate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmDataEntry_Load(object sender, EventArgs e)
        {//預設圖片
            //pictureBox1.Image = Bitmap.FromFile(@"C:\GroupB\userimage\smill.png");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {//更改圖片
            //openFileDialog1.Filter = "頭像| *.jpg|頭像|  *.png";
            openFileDialog1.Filter = "頭像| *.jpg; *.jpeg; *.png; *.bmp";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            { return; }
            pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);

            FileStream imgStram = new FileStream(openFileDialog1.FileName,FileMode.Open,FileAccess.Read);
            
            BinaryReader reader = new BinaryReader(imgStram);
            this.user.fUserImage = reader.ReadBytes((int)imgStram.Length);

            reader.Close();
            imgStram.Close(); 


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
