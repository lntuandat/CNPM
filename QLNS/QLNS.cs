using QLNS.Helper;
using QLNS.Login;
using QLNS.NhanSu;
using QLNS.PhongBan;
using QLNS.TinhLuong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    public partial class formQLNS : Form
    {
        public formQLNS()
        {
            InitializeComponent();
        }
        private void formQLNS_Load(object sender, EventArgs e)
        {

        }
        private void buttonNhanSu_Click(object sender, EventArgs e)
        {
            doiMauSidebar();
            buttonNhanSu.BackColor = Color.DodgerBlue;
            panelContainer.Controls.Clear();
            formNhanSu ns = new formNhanSu();
            ns.TopLevel = false;
            ns.FormBorderStyle = FormBorderStyle.None;
            ns.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ns);
            ns.Show();
        }

        private void buttonPhongBan_Click(object sender, EventArgs e)
        {
            doiMauSidebar();
            buttonPhongBan.BackColor = Color.DodgerBlue;
            panelContainer.Controls.Clear();
            formPhongBan pb = new formPhongBan();
            pb.TopLevel = false;
            pb.FormBorderStyle = FormBorderStyle.None;
            pb.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(pb);
            pb.Show();
        }

        private void buttonTinhLuong_Click(object sender, EventArgs e)
        {
            doiMauSidebar();
            buttonTinhLuong.BackColor = Color.DodgerBlue;
            panelContainer.Controls.Clear();
            formTinhLuong tl = new formTinhLuong();
            tl.TopLevel = false;
            tl.FormBorderStyle = FormBorderStyle.None;
            tl.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(tl);
            tl.Show();
        }

        private void buttonBangLuong_Click(object sender, EventArgs e)
        {
            doiMauSidebar();
            buttonBangLuong.BackColor = Color.DodgerBlue;
        }

        private void buttonThongKe_Click(object sender, EventArgs e)
        {
            doiMauSidebar();
            buttonThongKe.BackColor = Color.DodgerBlue;
        }

        public void doiMauSidebar()
        {
            buttonNhanSu.BackColor = Color.Blue;
            buttonPhongBan.BackColor = Color.Blue;
            buttonTinhLuong.BackColor = Color.Blue;
            buttonBangLuong.BackColor = Color.Blue;
            buttonThongKe.BackColor = Color.Blue;
        }

        private void buttonDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                formDangNhap dn = new formDangNhap();
                dn.Show();
                this.Hide();
            }
        }

        private void formQLNS_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.UserID = null;
            SessionManager.Username = null;
            SessionManager.UserRole = null;
        }
    }
}
