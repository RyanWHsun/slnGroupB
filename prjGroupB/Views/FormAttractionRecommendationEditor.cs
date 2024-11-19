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
    public partial class FormAttractionRecommendationEditor : Form {
        private CAttractionRecommendation _attractionRecommendation;

        public DialogResult isOk { get; set; }
        public FormAttractionRecommendationEditor() {
            InitializeComponent();
        }

        public CAttractionRecommendation attractionRecommendation {
            get {
                if (_attractionRecommendation == null) _attractionRecommendation = new CAttractionRecommendation();

                if (int.TryParse(fbAttractionId.fieldValue, out int attractionId)) _attractionRecommendation.fAttractionId = attractionId;
                else _attractionRecommendation.fAttractionId = 0;
                if (int.TryParse(tbRecommendationId.Text, out int recommendationId)) _attractionRecommendation.fRecommendationId = recommendationId;
                else _attractionRecommendation.fRecommendationId = 0;
                _attractionRecommendation.fReason = tbReason.Text;
                _attractionRecommendation.fCreatedDate = DateTime.Now;

                return _attractionRecommendation;
            }
            set {
                _attractionRecommendation = value;
                fbAttractionId.fieldValue = _attractionRecommendation.fAttractionId.ToString();
                tbRecommendationId.Text = _attractionRecommendation.fRecommendationId.ToString();
                tbReason.Text = _attractionRecommendation.fReason.ToString();
                lbCreatedDate.Text = _attractionRecommendation.fCreatedDate.ToString();
            }
        }
        // 按下"確認"按鈕
        private void btnConfirm_Click(object sender, EventArgs e) {
            this.isOk = DialogResult.OK;
            Close();
        }

        // 按下"取消"按鈕
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        } 
    }
}
