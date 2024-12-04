using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS.TinhLuong.ChildForm
{
    public partial class ChamCong : Form
    {
        //string connectionString = "Data Source=LAPTOP-JTJE2T8G\\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True;";
        string connectionString = KetNoi.kn;
        SqlConnection connection;
        public ChamCong()
        {
            InitializeComponent();
            
        }

        private void ChamCong_Load(object sender, EventArgs e)
        {
            
        }
    }
}
