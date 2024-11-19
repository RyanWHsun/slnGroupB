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
    public partial class FormAttractionTicketEditor : Form {
        private CAttractionTicket _attractionTicket;
        public DialogResult isOk { get; set; }
        public FormAttractionTicketEditor() {
            InitializeComponent();
        }
        public CAttractionTicket attractionTicket {
            get {
                if (_attractionTicket == null) _attractionTicket = new CAttractionTicket();

                if (int.TryParse(fbAttractionId.fieldValue, out int attractionId)) _attractionTicket.fAttractionId = attractionId;
                else _attractionTicket.fAttractionId = 0;
                _attractionTicket.fTicketType = fbTicketType.fieldValue;
                if (decimal.TryParse(fbPrice.fieldValue, out decimal result)) _attractionTicket.fPrice = result;
                else _attractionTicket.fPrice = 0;
                _attractionTicket.fDiscountInformation = tbDiscountInformation.Text;
                _attractionTicket.fCreatedDate = DateTime.Now;
                return _attractionTicket;
            }
            set {
                _attractionTicket = value;
                fbAttractionId.fieldValue = _attractionTicket.fAttractionId.ToString();
                fbTicketType.fieldValue = _attractionTicket.fTicketType;
                fbPrice.fieldValue = _attractionTicket.fPrice.ToString();
                tbDiscountInformation.Text = _attractionTicket.fDiscountInformation;
                lbCreatedDate.Text = _attractionTicket.fCreatedDate.ToString();
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
