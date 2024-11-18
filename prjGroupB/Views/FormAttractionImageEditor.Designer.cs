namespace Attractions.Views {
    partial class FormAttractionImageEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAttractionImageEditor));
            this.fbAttractionId = new Attractions.Views.fieldBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreviousImage = new System.Windows.Forms.Button();
            this.btnNextImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // fbAttractionId
            // 
            this.fbAttractionId.fieldName = "景點ID";
            this.fbAttractionId.fieldValue = "";
            this.fbAttractionId.Location = new System.Drawing.Point(33, 29);
            this.fbAttractionId.Name = "fbAttractionId";
            this.fbAttractionId.Size = new System.Drawing.Size(316, 33);
            this.fbAttractionId.TabIndex = 0;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Silver;
            this.pbImage.Location = new System.Drawing.Point(33, 94);
            this.pbImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(315, 206);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 24;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirm.Location = new System.Drawing.Point(266, 368);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(83, 38);
            this.btnConfirm.TabIndex = 25;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.Location = new System.Drawing.Point(165, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 38);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPreviousImage
            // 
            this.btnPreviousImage.Font = new System.Drawing.Font("微軟正黑體", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPreviousImage.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousImage.Image")));
            this.btnPreviousImage.Location = new System.Drawing.Point(143, 304);
            this.btnPreviousImage.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnPreviousImage.Name = "btnPreviousImage";
            this.btnPreviousImage.Size = new System.Drawing.Size(35, 30);
            this.btnPreviousImage.TabIndex = 30;
            this.btnPreviousImage.UseVisualStyleBackColor = true;
            this.btnPreviousImage.Click += new System.EventHandler(this.btnPreviousImage_Click);
            // 
            // btnNextImage
            // 
            this.btnNextImage.Font = new System.Drawing.Font("微軟正黑體", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnNextImage.Image = ((System.Drawing.Image)(resources.GetObject("btnNextImage.Image")));
            this.btnNextImage.Location = new System.Drawing.Point(192, 304);
            this.btnNextImage.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(35, 30);
            this.btnNextImage.TabIndex = 31;
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.Click += new System.EventHandler(this.btnNextImage_Click);
            // 
            // FormAttractionImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 431);
            this.Controls.Add(this.btnNextImage);
            this.Controls.Add(this.btnPreviousImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.fbAttractionId);
            this.Name = "FormAttractionImageEditor";
            this.Text = "FormImageEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private fieldBox fbAttractionId;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreviousImage;
        private System.Windows.Forms.Button btnNextImage;
    }
}