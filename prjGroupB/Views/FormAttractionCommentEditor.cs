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
    public partial class FormAttractionCommentEditor : Form {
        private CAttractionComment _attractionComment;
        public DialogResult isOk { get; set; }
        public FormAttractionCommentEditor() {
            InitializeComponent();
        }
        public CAttractionComment attractionComment {
            get {
                if (_attractionComment == null) _attractionComment = new CAttractionComment();

                if (int.TryParse(fbAttractionId.fieldValue, out int attractionId)) _attractionComment.fAttractionId = attractionId;
                else _attractionComment.fAttractionId = 0;
                
                if (int.TryParse(fbUserId.fieldValue, out int result)) _attractionComment.fUserId = result;
                else _attractionComment.fUserId = 0;

                switch (cbRating.SelectedItem.ToString()) {
                    case "⭐⭐⭐⭐⭐":
                        _attractionComment.fRating = 5;
                        break;
                    case "⭐⭐⭐⭐":
                        _attractionComment.fRating = 4;
                        break;
                    case "⭐⭐⭐":
                        _attractionComment.fRating = 3;
                        break;
                    case "⭐⭐":
                        _attractionComment.fRating = 2;
                        break;
                    case "⭐":
                        _attractionComment.fRating = 1;
                        break;
                    default:
                        _attractionComment.fRating = 0;
                        break;
                }

                _attractionComment.fComment = tbComment.Text;
                _attractionComment.fCreatedDate = DateTime.Now;
                return _attractionComment;
            }
            set {
                _attractionComment = value;
                fbAttractionId.fieldValue = _attractionComment.fAttractionId.ToString();
                fbUserId.fieldValue = _attractionComment.fUserId.ToString();

                switch (_attractionComment.fRating) {
                    case 5:
                        cbRating.SelectedItem = "⭐⭐⭐⭐⭐";
                        break;
                    case 4:
                        cbRating.SelectedItem = "⭐⭐⭐⭐";
                        break;
                    case 3:
                        cbRating.SelectedItem = "⭐⭐⭐";
                        break;
                    case 2:
                        cbRating.SelectedItem = "⭐⭐";
                        break;
                    case 1:
                        cbRating.SelectedItem = "⭐";
                        break;
                    default:
                        cbRating.SelectedItem = "";
                        break;
                }

                tbComment.Text = _attractionComment.fComment;
                lbCreatedDate.Text = _attractionComment.fCreatedDate.ToString();
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
