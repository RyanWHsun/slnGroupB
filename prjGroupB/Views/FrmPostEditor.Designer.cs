namespace prjGroupB.Views
{
    partial class FrmPostEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPostEditor));
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.picPost = new System.Windows.Forms.PictureBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblUpdatedName = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIsPicture = new System.Windows.Forms.Button();
            this.cmbIsPublic = new System.Windows.Forms.ComboBox();
            this.lblPublic = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picPost)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbContent
            // 
            this.rtbContent.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtbContent.Location = new System.Drawing.Point(51, 98);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(220, 28);
            this.rtbContent.TabIndex = 1;
            this.rtbContent.Text = "";
            this.rtbContent.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // picPost
            // 
            this.picPost.BackColor = System.Drawing.SystemColors.Control;
            this.picPost.Location = new System.Drawing.Point(51, 132);
            this.picPost.Name = "picPost";
            this.picPost.Size = new System.Drawing.Size(220, 122);
            this.picPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPost.TabIndex = 2;
            this.picPost.TabStop = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtTitle.Location = new System.Drawing.Point(51, 39);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(220, 27);
            this.txtTitle.TabIndex = 3;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(51, 72);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 20);
            this.cmbCategory.TabIndex = 4;
            // 
            // lblUpdatedName
            // 
            this.lblUpdatedName.AutoSize = true;
            this.lblUpdatedName.Location = new System.Drawing.Point(13, 493);
            this.lblUpdatedName.Name = "lblUpdatedName";
            this.lblUpdatedName.Size = new System.Drawing.Size(77, 12);
            this.lblUpdatedName.TabIndex = 5;
            this.lblUpdatedName.Text = "最後更新時間";
            // 
            // lblUpdated
            // 
            this.lblUpdated.AutoSize = true;
            this.lblUpdated.Location = new System.Drawing.Point(91, 493);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(33, 12);
            this.lblUpdated.TabIndex = 6;
            this.lblUpdated.Text = "label1";
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(237, 488);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(75, 23);
            this.btnPost.TabIndex = 7;
            this.btnPost.Text = "發送";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(29, 12);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "標題";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 75);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(29, 12);
            this.lblCategory.TabIndex = 9;
            this.lblCategory.Text = "分類";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "內文";
            // 
            // btnIsPicture
            // 
            this.btnIsPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnIsPicture.Image")));
            this.btnIsPicture.Location = new System.Drawing.Point(277, 39);
            this.btnIsPicture.Name = "btnIsPicture";
            this.btnIsPicture.Size = new System.Drawing.Size(35, 27);
            this.btnIsPicture.TabIndex = 11;
            this.btnIsPicture.UseVisualStyleBackColor = true;
            this.btnIsPicture.Click += new System.EventHandler(this.btnIsPicture_Click);
            // 
            // cmbIsPublic
            // 
            this.cmbIsPublic.FormattingEnabled = true;
            this.cmbIsPublic.Location = new System.Drawing.Point(212, 72);
            this.cmbIsPublic.Name = "cmbIsPublic";
            this.cmbIsPublic.Size = new System.Drawing.Size(59, 20);
            this.cmbIsPublic.TabIndex = 12;
            // 
            // lblPublic
            // 
            this.lblPublic.AutoSize = true;
            this.lblPublic.Location = new System.Drawing.Point(177, 75);
            this.lblPublic.Name = "lblPublic";
            this.lblPublic.Size = new System.Drawing.Size(29, 12);
            this.lblPublic.TabIndex = 13;
            this.lblPublic.Text = "分享";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmPostEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 517);
            this.Controls.Add(this.lblPublic);
            this.Controls.Add(this.cmbIsPublic);
            this.Controls.Add(this.btnIsPicture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.lblUpdatedName);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.picPost);
            this.Controls.Add(this.rtbContent);
            this.Name = "FrmPostEditor";
            this.Text = "FrmPostEditor";
            this.Load += new System.EventHandler(this.FrmPostEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.PictureBox picPost;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblUpdatedName;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIsPicture;
        private System.Windows.Forms.ComboBox cmbIsPublic;
        private System.Windows.Forms.Label lblPublic;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}