using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Views
{
    public partial class FrmPostEditor : Form
    {
        private CPost _post;
        private CPostPictureManager _pictureManager;
        public DialogResult isOK { get; set; }
        public FrmPostEditor()
        {
            InitializeComponent();
            setCmbIsPublic();
            setCmbCategory();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            setRtb();
        }

        private void setRtb()
        {
            int lineCount = rtbContent.GetLineFromCharIndex(rtbContent.TextLength) + 1;
            int maxLines = 7;
            //int lineHeight = rtbContent.Font.Height;
            int lineHeight = 26;
            int maxHeight = maxLines * lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;
            if (lineCount * lineHeight > maxHeight)
            {
                rtbContent.Height = maxHeight;
                rtbContent.ScrollBars = RichTextBoxScrollBars.Vertical;
            }
            else
            {
                rtbContent.Height = lineCount * lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;
                rtbContent.ScrollBars = RichTextBoxScrollBars.None;
            }
            picPost.Top = rtbContent.Bottom + 10;
            btnFirst.Top = picPost.Bottom + 10;
            btnPrevious.Top = picPost.Bottom + 10;
            btnNext.Top = picPost.Bottom + 10;
            btnLast.Top = picPost.Bottom + 10;
        }

        private void btnIsPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文章照片|*.png|文章照片|*.jpg";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            FileStream imgStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imgStream);
            byte[] x = reader.ReadBytes((int)imgStream.Length);
            this.post.fImages.Add(x);
            _pictureManager = new CPostPictureManager(this.post.fImages);
            _pictureManager.afterImageMoved += this.DisplayPostImage;
            _pictureManager.moveLast();
            reader.Close();
            imgStream.Close();
        }
        private void picPost_DoubleClick(object sender, EventArgs e)
        {
            if(_pictureManager == null)
                return;
            _pictureManager.removeImage();
        }
        private void setCmbIsPublic()
        {
            cmbIsPublic.Items.Add("私人");
            cmbIsPublic.Items.Add("公開");
            cmbIsPublic.SelectedIndex = 0;
        }
        private void setCmbCategory()
        {
            CPostManager manager = new CPostManager();
            foreach (string category in manager.getCategory())
            {
                cmbCategory.Items.Add(category);
            }
        }
        public CPost post
        {
            get
            {
                if (_post == null)
                    _post = new CPost();
                _post.fTitle = txtTitle.Text;
                if (cmbCategory.SelectedItem != null)
                    _post.fCategory = cmbCategory.SelectedItem.ToString();
                if (cmbIsPublic.SelectedItem.ToString() == "公開")
                    _post.fIsPublic = true;
                else
                    _post.fIsPublic = false;
                _post.fContent = rtbContent.Text;
                return _post;
            }
            set
            {
                _post = value;
                txtTitle.Text = _post.fTitle;
                cmbCategory.SelectedItem = _post.fCategory;
                if (_post.fIsPublic)
                    cmbIsPublic.SelectedItem = "公開";
                else
                    cmbIsPublic.SelectedItem = "私人";
                rtbContent.Text = _post.fContent;
                lblUpdatedName.Visible = true;
                lblUpdated.Visible = true;
                lblUpdated.Text = _post.fCreatedAt.ToString();
                if (_post.fUpdatedAt > _post.fCreatedAt)
                    lblUpdated.Text = _post.fUpdatedAt.ToString();
                if (_post.fImages.Count > 0)
                {
                    _pictureManager = new CPostPictureManager(_post.fImages);
                    _pictureManager.afterImageMoved += this.DisplayPostImage;
                    _pictureManager.moveFirst();
                }
            }
        }
        public void readOnly()
        {
            txtTitle.ReadOnly = true;
            btnIsPicture.Enabled = false;
            cmbCategory.Enabled = false;
            cmbIsPublic.Enabled = false;
            rtbContent.ReadOnly = true;
            picPost.Enabled = false;
            btnPost.Enabled = false;
            setRtb();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            getTagByContent(rtbContent, post);
            this.isOK = DialogResult.OK;
            Close();
        }
        private void DisplayPostImage()
        {
            if (_pictureManager.current == null)
            {
                picPost.Image = null;
                return;
            }
            Stream s = new MemoryStream(_pictureManager.current);
            picPost.Image = Bitmap.FromStream(s);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (_pictureManager == null)
                return;
            _pictureManager.moveFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_pictureManager == null)
                return;
            _pictureManager.movePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_pictureManager == null)
                return;
            _pictureManager.moveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (_pictureManager == null)
                return;
            _pictureManager.moveLast();
        }
        private void getTagByContent(RichTextBox richTextBox, CPost post)
        {
            string pattern = @"#([^#\s]+)";
            MatchCollection matches = Regex.Matches(richTextBox.Text, pattern);
            foreach (Match match in matches)
            {
                post.fTags.Add(match.ToString());
            }
        }
    }
}
