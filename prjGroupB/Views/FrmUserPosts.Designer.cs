namespace prjGroupB.Views
{
    partial class FrmUserPosts
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnInsertCategory = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.flpUserPosts = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSelectCategory = new System.Windows.Forms.Button();
            this.flpBtnCategory = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(188, 49);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "新增";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnInsertCategory
            // 
            this.btnInsertCategory.Location = new System.Drawing.Point(32, 49);
            this.btnInsertCategory.Name = "btnInsertCategory";
            this.btnInsertCategory.Size = new System.Drawing.Size(75, 23);
            this.btnInsertCategory.TabIndex = 1;
            this.btnInsertCategory.Text = "新增分類";
            this.btnInsertCategory.UseVisualStyleBackColor = true;
            this.btnInsertCategory.Click += new System.EventHandler(this.btnInsertCategory_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(269, 49);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "刪除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // flpUserPosts
            // 
            this.flpUserPosts.Location = new System.Drawing.Point(160, 102);
            this.flpUserPosts.Name = "flpUserPosts";
            this.flpUserPosts.Size = new System.Drawing.Size(550, 469);
            this.flpUserPosts.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdate.Location = new System.Drawing.Point(350, 49);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSelectCategory
            // 
            this.btnSelectCategory.Location = new System.Drawing.Point(32, 78);
            this.btnSelectCategory.Name = "btnSelectCategory";
            this.btnSelectCategory.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCategory.TabIndex = 5;
            this.btnSelectCategory.Text = "所有文章";
            this.btnSelectCategory.UseVisualStyleBackColor = true;
            // 
            // flpBtnCategory
            // 
            this.flpBtnCategory.Location = new System.Drawing.Point(41, 107);
            this.flpBtnCategory.Name = "flpBtnCategory";
            this.flpBtnCategory.Size = new System.Drawing.Size(86, 464);
            this.flpBtnCategory.TabIndex = 6;
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Location = new System.Drawing.Point(32, 20);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteCategory.TabIndex = 7;
            this.btnDeleteCategory.Text = "刪除分類";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // FrmUserPosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 594);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.flpBtnCategory);
            this.Controls.Add(this.btnSelectCategory);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.flpUserPosts);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsertCategory);
            this.Controls.Add(this.btnInsert);
            this.Name = "FrmUserPosts";
            this.Text = "FrmUserPosts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmUserPosts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnInsertCategory;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.FlowLayoutPanel flpUserPosts;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSelectCategory;
        private System.Windows.Forms.FlowLayoutPanel flpBtnCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
    }
}