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
        }
        private void FrmPostEditor_Load(object sender, EventArgs e)
        {
            setRichTextBoxSize();
            setCmbIsPublic();
            setCmbCategory();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int lineCount = rtbContent.GetLineFromCharIndex(rtbContent.TextLength) + 1;
            int maxLines = 5;
            int lineHeight = rtbContent.Font.Height;
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
        private void setRichTextBoxSize()
        {
            Font font = new Font("新細明體", 12);
            rtbContent.Font = font;
            string sampleText = "This is a sample text in RichTextBox.";
            int textWidth = TextRenderer.MeasureText(sampleText, font).Width;
            rtbContent.Width = textWidth + rtbContent.Margin.Left + rtbContent.Margin.Right;
            int lineHeight = font.Height;
            rtbContent.Height = lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;
        }
        private void btnIsPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "房間照片|*.png|房間照片|*.jpg";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            //picPost.Image = Bitmap.FromFile(openFileDialog1.FileName);

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
        private void setCmbIsPublic()
        {
            cmbIsPublic.Items.Add("私人");
            cmbIsPublic.Items.Add("公開");
            cmbIsPublic.SelectedIndex = 0;
        }
        private void setCmbCategory()
        {
            CPostManager manager = new CPostManager();
            manager.getCategory();
            foreach (string category in manager.Categories)
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
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            getTagByContent(rtbContent, post);
            this.isOK = DialogResult.OK;
            Close();
        }
        private void DisplayPostImage()
        {
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
        private void islblUpdatedVisiable(bool isVisiable)
        {
            lblUpdatedName.Visible = isVisiable;
            lblUpdated.Visible = isVisiable;
        }
        private void getTagByContent(RichTextBox richTextBox, CPost post)
        {
            string pattern = @"#\S+";
            MatchCollection matches = Regex.Matches(richTextBox.Text, pattern);
            foreach (Match match in matches)
            {
                post.fTags.Add(match.ToString());
            }
        }
    }
}
