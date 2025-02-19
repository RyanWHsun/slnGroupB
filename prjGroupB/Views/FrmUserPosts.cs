using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmUserPosts : Form
    {
        private CPost _selected;
        private Button _selectedBtn;
        private CPostManager _manager = new CPostManager();
        public FrmUserPosts()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmPostEditor f = new FrmPostEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                _manager.insert(f.post);
                displayUserPosts();
            }
        }

        private void btnInsertCategory_Click(object sender, EventArgs e)
        {
            FrmPostCategoryEditor f = new FrmPostCategoryEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(f.message))
                    return;
                _manager.insertCategory(f.message);
                setBtnCategory();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null)
                return;
            FrmPostDelete f = new FrmPostDelete();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                _manager.delete(_selected);
                _selected = null;
                displayUserPosts();
            }
        }
        private void selectUserPost(CPost post)
        {
            _selected = post;
        }
        private void doubleClickUserPost(CPost post)
        {
            if (_selected == null)
                return;
            FrmPostEditor f = new FrmPostEditor();
            f.post = _selected;
            f.readOnly();
            f.ShowDialog();
            _selected = null;
        }

        private void FrmUserPosts_Load(object sender, EventArgs e)
        {
            displayUserPosts();
        }
        private void displayUserPosts(string s)
        {
            flpUserPosts.Controls.Clear();
            flpUserPosts.SuspendLayout();
            List<CPost> userPosts = _manager.getUserPosts();
            List<CPost> userCategoryPosts = new List<CPost>();
            foreach (CPost post in userPosts)
            {
                if (post.fCategory == s)
                {
                    userCategoryPosts.Add(post);
                }
            }
            userCategoryPosts.Reverse();
            foreach (CPost userPost in userCategoryPosts)
            {
                PostBox postBox = new PostBox();
                postBox.post = userPost;
                postBox.DselectUserPost += this.selectUserPost;
                postBox.DdoubliClickPost += this.doubleClickUserPost;
                flpUserPosts.Controls.Add(postBox);
            }
            flpUserPosts.ResumeLayout();
        }
        private void displayUserPosts()
        {
            flpUserPosts.Controls.Clear();
            flpUserPosts.SuspendLayout();
            List<CPost> userPosts = _manager.getUserPosts();
            userPosts.Reverse();
            foreach (CPost userPost in userPosts)
            {
                PostBox postBox = new PostBox();
                postBox.post = userPost;
                postBox.DselectUserPost += this.selectUserPost;
                postBox.DdoubliClickPost += this.doubleClickUserPost;
                flpUserPosts.Controls.Add(postBox);
            }
            flpUserPosts.ResumeLayout();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selected == null)
                return;
            FrmPostEditor f = new FrmPostEditor();
            f.post = _selected;
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                _manager.update(f.post);
                displayUserPosts();
            }
            _selected = null;
        }
        private void setBtnCategory()
        {
            flpBtnCategory.Controls.Clear();
            foreach (string category in (new CPostManager()).getCategory())
            {
                Button btn = new Button();
                btn.Text = category;
                btn.Click += this.btnCategory_click;
                btn.MouseDown += this.btnCategory_MouseDown;
                btn.Width = flpBtnCategory.Width - SystemInformation.VerticalScrollBarWidth - 3;
                btn.Height = flpBtnCategory.ClientSize.Height / 10;
                btn.Font = new Font("微軟正黑體", btn.Height / 3, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.ForeColor = Color.Black;
                btn.BackColor = Color.FromArgb(250, 243, 224);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                flpBtnCategory.Controls.Add(btn);
            }
            Button btnInsertCategory = new Button();
            btnInsertCategory.Text = "+";
            btnInsertCategory.Width = flpBtnCategory.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 3;
            btnInsertCategory.Height = (int)(flpBtnCategory.ClientSize.Height / 10 / 1.5);
            btnInsertCategory.Font = new Font("微軟正黑體", btnInsertCategory.Height / 2, FontStyle.Bold);
            btnInsertCategory.Click += this.btnInsertCategory_Click;
            flpBtnCategory.Controls.Add(btnInsertCategory);
        }
        private void btnCategory_click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            displayUserPosts(clickedButton.Text);
        }

        private void btnCategory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectedBtn = sender as Button;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedBtn == null)
                return;
            FrmPostCategoryEditor f = new FrmPostCategoryEditor();
            f.message = _selectedBtn.Text;
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                _manager.updateCategory(_selectedBtn.Text, f.message);
                setBtnCategory();
                displayUserPosts(f.message);
            }
        }

        private void 刪除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedBtn == null)
                return;
            _manager.deleteCategory(_selectedBtn.Text);
            setBtnCategory();
            flpUserPosts.Controls.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            flpUserPosts.Controls.Clear();
            List<CPost> userPosts = _manager.getUserPosts();
            userPosts.Reverse();
            foreach (CPost userPost in userPosts)
            {
                PostBox postBox = new PostBox();
                postBox.post = userPost;
                postBox.DselectUserPost += this.selectUserPost;
                postBox.DdoubliClickPost += this.doubleClickUserPost;
                if (userPost.fTags.Count == 0 && userPost.fTitle.Contains(txtFind.Text))
                {
                    flpUserPosts.Controls.Add(postBox);
                    continue;
                }
                foreach (string tag in postBox.post.fTags)
                {
                    if (tag.Contains(txtFind.Text) || userPost.fTitle.Contains(txtFind.Text))
                    {
                        flpUserPosts.Controls.Add(postBox);
                    }
                }
            }
        }
        private void pcbUserPosts_Click(object sender, EventArgs e)
        {
            displayUserPosts();
        }

        private void FrmUserPosts_Paint(object sender, PaintEventArgs e)
        {
            setBtnCategory();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
