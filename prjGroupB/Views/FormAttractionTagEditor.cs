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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Attractions.Views {
    public partial class FormAttractionTagEditor : Form {
        private CAttractionTag _attractionTag;
        public DialogResult isOk { get; set; }
        public FormAttractionTagEditor() {
            InitializeComponent();
        }

        public CAttractionTag attractionTag {
            get {
                if (_attractionTag == null) _attractionTag = new CAttractionTag();

                _attractionTag.fTagName = fbAttractionTagName.fieldValue[0] == '#' ? fbAttractionTagName.fieldValue : "#" + fbAttractionTagName.fieldValue;
                _attractionTag.fCreatedDate = DateTime.Now;

                return _attractionTag;
            }
            set {
                _attractionTag = value;
                fbAttractionTagName.fieldValue = _attractionTag.fTagName[0] == '#' ? _attractionTag.fTagName : "#" + _attractionTag.fTagName;
                lbCreatedDate.Text = _attractionTag.fCreatedDate.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            if (fbAttractionTagName.fieldValue.Length > 10) {
                MessageBox.Show("標籤名稱不可超過10個字");
                return;
            }
            this.isOk = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
