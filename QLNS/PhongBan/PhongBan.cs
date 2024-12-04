using QLNS.NhanSu.ChildForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS.PhongBan
{
    public partial class formPhongBan : Form
    {
        public formPhongBan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doiMauText();
            button1.ForeColor = Color.Blue;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            doiMauText();
            button2.ForeColor = Color.Blue;
            panelPage.Controls.Clear();
            DSNhanVienPhongBan nvpb = new DSNhanVienPhongBan();
            nvpb.TopLevel = false;
            nvpb.FormBorderStyle = FormBorderStyle.None;
            nvpb.Dock = DockStyle.Fill;
            panelPage.Controls.Add(nvpb);
            nvpb.Show();
        }
        public void doiMauText()
        {
            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
        }
    }
}
