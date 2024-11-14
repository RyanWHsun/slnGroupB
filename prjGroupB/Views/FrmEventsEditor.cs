using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmEventsEditor : Form
    {
        public DialogResult IsOk { get; set; }
        private CEvents _Events;
        public CEvents Events
        { 
            get
            {
                if (_Events == null)
                    _Events = new CEvents();
                _Events.fEventId = Convert.ToInt32(textBox1.Text);              
                _Events.fEventName =textBox2.Text;
                _Events.fEventDescription = textBox3.Text;
                _Events.fEventStartDate = textBox5.Text;
                _Events.fEventEndDate = textBox6.Text;
                _Events.fEventLocation = textBox4.Text;
                _Events.fEventCreatedDate = DateTime.UtcNow;
                _Events.fEventUpdatedDate = DateTime.UtcNow;
                _Events.fEventActivityfee = Convert.ToDecimal(textBox9.Text);
                _Events.fEventURL = textBox10.Text;
              return _Events;
            }
    set
            {
                _Events = value;
                textBox1.Text = _Events.fEventId.ToString();
                textBox2.Text = _Events.fEventName;
                textBox3.Text = _Events.fEventDescription.ToString();
                textBox4.Text = _Events.fEventStartDate.ToString();
                textBox5.Text = _Events.fEventEndDate.ToString();
                textBox6.Text = _Events.fEventLocation.ToString();
                textBox7.Text =_Events.fEventCreatedDate.ToString();
                textBox8.Text= _Events.fEventUpdatedDate.ToString();
                textBox9.Text= _Events.fEventActivityfee.ToString();
                textBox10.Text= _Events.fEventURL.ToString();

            
            }
        }
        public FrmEventsEditor()
        {
            InitializeComponent();
        }

        private void FrmEventsEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
