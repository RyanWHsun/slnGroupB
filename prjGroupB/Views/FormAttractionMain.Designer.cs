namespace Attractions.Views {
    partial class FormAttractionMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnTicket = new System.Windows.Forms.Button();
            this.btnComment = new System.Windows.Forms.Button();
            this.btnAttraction = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnTag = new System.Windows.Forms.Button();
            this.btnRecommendation = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnFavorite = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnImage, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnTicket, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnComment, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAttraction, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 386);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnImage
            // 
            this.btnImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnImage.Location = new System.Drawing.Point(219, 131);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(210, 122);
            this.btnImage.TabIndex = 7;
            this.btnImage.Text = "圖片管理";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            this.btnImage.MouseLeave += new System.EventHandler(this.btnImage_MouseLeave);
            this.btnImage.MouseHover += new System.EventHandler(this.btnImage_MouseHover);
            // 
            // btnTicket
            // 
            this.btnTicket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTicket.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTicket.Location = new System.Drawing.Point(219, 259);
            this.btnTicket.Name = "btnTicket";
            this.btnTicket.Size = new System.Drawing.Size(210, 124);
            this.btnTicket.TabIndex = 5;
            this.btnTicket.Text = "票務管理";
            this.btnTicket.UseVisualStyleBackColor = true;
            this.btnTicket.Click += new System.EventHandler(this.btnTicket_Click);
            this.btnTicket.MouseLeave += new System.EventHandler(this.btnTicket_MouseLeave);
            this.btnTicket.MouseHover += new System.EventHandler(this.btnTicket_MouseHover);
            // 
            // btnComment
            // 
            this.btnComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnComment.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnComment.Location = new System.Drawing.Point(3, 259);
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(210, 124);
            this.btnComment.TabIndex = 4;
            this.btnComment.Text = "評論管理";
            this.btnComment.UseVisualStyleBackColor = true;
            this.btnComment.Click += new System.EventHandler(this.btnComment_Click);
            this.btnComment.MouseLeave += new System.EventHandler(this.btnComment_MouseLeave);
            this.btnComment.MouseHover += new System.EventHandler(this.btnComment_MouseHover);
            // 
            // btnAttraction
            // 
            this.btnAttraction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAttraction.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAttraction.Location = new System.Drawing.Point(3, 3);
            this.btnAttraction.Name = "btnAttraction";
            this.btnAttraction.Size = new System.Drawing.Size(210, 122);
            this.btnAttraction.TabIndex = 0;
            this.btnAttraction.Text = "景點管理";
            this.btnAttraction.UseVisualStyleBackColor = true;
            this.btnAttraction.Click += new System.EventHandler(this.button1_Click);
            this.btnAttraction.MouseLeave += new System.EventHandler(this.btnAttraction_MouseLeave);
            this.btnAttraction.MouseHover += new System.EventHandler(this.btnAttraction_MouseHover);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(219, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCategory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnTag);
            this.splitContainer1.Size = new System.Drawing.Size(210, 122);
            this.splitContainer1.SplitterDistance = 99;
            this.splitContainer1.TabIndex = 6;
            // 
            // btnCategory
            // 
            this.btnCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCategory.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCategory.Location = new System.Drawing.Point(0, 0);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(99, 122);
            this.btnCategory.TabIndex = 1;
            this.btnCategory.Text = "分類管理";
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            this.btnCategory.MouseLeave += new System.EventHandler(this.btnCategory_MouseLeave);
            this.btnCategory.MouseHover += new System.EventHandler(this.btnCategory_MouseHover);
            // 
            // btnTag
            // 
            this.btnTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTag.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTag.Location = new System.Drawing.Point(0, 0);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(107, 122);
            this.btnTag.TabIndex = 3;
            this.btnTag.Text = "標籤管理";
            this.btnTag.UseVisualStyleBackColor = true;
            this.btnTag.Click += new System.EventHandler(this.btnTag_Click);
            this.btnTag.MouseLeave += new System.EventHandler(this.btnTag_MouseLeave);
            this.btnTag.MouseHover += new System.EventHandler(this.btnTag_MouseHover);
            // 
            // btnRecommendation
            // 
            this.btnRecommendation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecommendation.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRecommendation.Location = new System.Drawing.Point(0, 0);
            this.btnRecommendation.Name = "btnRecommendation";
            this.btnRecommendation.Size = new System.Drawing.Size(103, 122);
            this.btnRecommendation.TabIndex = 2;
            this.btnRecommendation.Text = "推薦管理";
            this.btnRecommendation.UseVisualStyleBackColor = true;
            this.btnRecommendation.Click += new System.EventHandler(this.btnRecommendation_Click);
            this.btnRecommendation.MouseLeave += new System.EventHandler(this.btnRecommendation_MouseLeave);
            this.btnRecommendation.MouseHover += new System.EventHandler(this.btnRecommendation_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 418);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 32);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(184, 386);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(616, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(184, 386);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(184, 32);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(432, 386);
            this.panel5.TabIndex = 6;
            // 
            // btnFavorite
            // 
            this.btnFavorite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFavorite.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFavorite.Location = new System.Drawing.Point(0, 0);
            this.btnFavorite.Name = "btnFavorite";
            this.btnFavorite.Size = new System.Drawing.Size(103, 122);
            this.btnFavorite.TabIndex = 3;
            this.btnFavorite.Text = "收藏";
            this.btnFavorite.UseVisualStyleBackColor = true;
            this.btnFavorite.Click += new System.EventHandler(this.btnFavorite_Click);
            this.btnFavorite.MouseLeave += new System.EventHandler(this.btnFavorite_MouseLeave);
            this.btnFavorite.MouseHover += new System.EventHandler(this.btnFavorite_MouseHover);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 131);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnFavorite);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnRecommendation);
            this.splitContainer2.Size = new System.Drawing.Size(210, 122);
            this.splitContainer2.SplitterDistance = 103;
            this.splitContainer2.TabIndex = 8;
            // 
            // FormAttractionMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormAttractionMain";
            this.Text = "FormAttractionMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnTicket;
        private System.Windows.Forms.Button btnComment;
        private System.Windows.Forms.Button btnTag;
        private System.Windows.Forms.Button btnRecommendation;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnAttraction;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnFavorite;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}