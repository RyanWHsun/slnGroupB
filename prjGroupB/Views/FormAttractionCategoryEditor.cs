using Attractions.Models;
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

namespace Attractions.Views {
    public partial class FormAttractionCategoryEditor : Form {
        private CAttractionCategory _attractionCategory;
        
        public DialogResult isOk {  get; set; }
        public FormAttractionCategoryEditor() {
            InitializeComponent();
        }

        public CAttractionCategory attractionCategory {
            get {
                if (_attractionCategory == null) _attractionCategory = new CAttractionCategory();
                
                _attractionCategory.fAttractionCategoryName = fbAttractionCategoryName.fieldValue;
                _attractionCategory.fDescription = fbAttractionCategoryDescription.fieldValue;
                _attractionCategory.fCreateDate = DateTime.Now;
                
                return _attractionCategory; 
            }
            set { 
                _attractionCategory = value;
                fbAttractionCategoryName.fieldValue = _attractionCategory.fAttractionCategoryName;
                fbAttractionCategoryDescription.fieldValue = _attractionCategory.fDescription;
                lbCreatedDate.Text = _attractionCategory.fCreateDate.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            this.isOk = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
