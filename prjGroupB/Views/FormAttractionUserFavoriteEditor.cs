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

namespace prjGroupB.Views {
    public partial class FormAttractionUserFavoriteEditor : Form {
        private CAttractionUserFavorite _attractionUserFavorite;
        public DialogResult isOk { get; set; }
        public FormAttractionUserFavoriteEditor() {
            InitializeComponent();
        }

        public CAttractionUserFavorite attractionUserFavorite {
            get {
                if (_attractionUserFavorite == null) _attractionUserFavorite = new CAttractionUserFavorite();

                if (int.TryParse(fbUserId.fieldValue, out int userId)) _attractionUserFavorite.fUserId = userId;
                else _attractionUserFavorite.fUserId = 0;
                if (int.TryParse(fbAttractionId.fieldValue, out int attractionId)) _attractionUserFavorite.fAttractionId = attractionId;
                _attractionUserFavorite.fCreatedDate = DateTime.Now;
                return _attractionUserFavorite;
            }
            set {
                _attractionUserFavorite = value;
                fbUserId.fieldValue = _attractionUserFavorite.fUserId.ToString();
                fbAttractionId.fieldValue = _attractionUserFavorite.fAttractionId.ToString();
                lbCreatedDate.Text = _attractionUserFavorite.fCreatedDate.ToString();
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
