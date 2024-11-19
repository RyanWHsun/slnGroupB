namespace Attractions.Views {
    partial class FormAttractionTicketEditor {
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
            this.fbAttractionId = new Attractions.Views.fieldBox();
            this.fbTicketType = new Attractions.Views.fieldBox();
            this.fbPrice = new Attractions.Views.fieldBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDiscountInformation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCreatedDate = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fbAttractionId
            // 
            this.fbAttractionId.fieldName = "景點ID";
            this.fbAttractionId.fieldValue = "";
            this.fbAttractionId.Location = new System.Drawing.Point(33, 35);
            this.fbAttractionId.Name = "fbAttractionId";
            this.fbAttractionId.Size = new System.Drawing.Size(316, 33);
            this.fbAttractionId.TabIndex = 0;
            // 
            // fbTicketType
            // 
            this.fbTicketType.fieldName = "票種";
            this.fbTicketType.fieldValue = "";
            this.fbTicketType.Location = new System.Drawing.Point(33, 105);
            this.fbTicketType.Name = "fbTicketType";
            this.fbTicketType.Size = new System.Drawing.Size(316, 33);
            this.fbTicketType.TabIndex = 1;
            // 
            // fbPrice
            // 
            this.fbPrice.fieldName = "票價";
            this.fbPrice.fieldValue = "";
            this.fbPrice.Location = new System.Drawing.Point(33, 174);
            this.fbPrice.Name = "fbPrice";
            this.fbPrice.Size = new System.Drawing.Size(316, 33);
            this.fbPrice.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(29, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "折扣資訊";
            // 
            // tbDiscountInformation
            // 
            this.tbDiscountInformation.Font = new System.Drawing.Font("微軟正黑體", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbDiscountInformation.Location = new System.Drawing.Point(129, 245);
            this.tbDiscountInformation.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tbDiscountInformation.Multiline = true;
            this.tbDiscountInformation.Name = "tbDiscountInformation";
            this.tbDiscountInformation.Size = new System.Drawing.Size(220, 84);
            this.tbDiscountInformation.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(29, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 39;
            this.label2.Text = "建立時間";
            // 
            // lbCreatedDate
            // 
            this.lbCreatedDate.AutoSize = true;
            this.lbCreatedDate.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCreatedDate.Location = new System.Drawing.Point(125, 359);
            this.lbCreatedDate.Name = "lbCreatedDate";
            this.lbCreatedDate.Size = new System.Drawing.Size(0, 24);
            this.lbCreatedDate.TabIndex = 40;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirm.Location = new System.Drawing.Point(267, 448);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 34);
            this.btnConfirm.TabIndex = 41;
            this.btnConfirm.Text = "確認";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.Location = new System.Drawing.Point(155, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 34);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAttractionTicketEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 512);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbCreatedDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDiscountInformation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fbPrice);
            this.Controls.Add(this.fbTicketType);
            this.Controls.Add(this.fbAttractionId);
            this.Name = "FormAttractionTicketEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTicketEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private fieldBox fbAttractionId;
        private fieldBox fbTicketType;
        private fieldBox fbPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDiscountInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbCreatedDate;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}