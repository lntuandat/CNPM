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


namespace QLNS.NhanSu.ChildForm
{
    public partial class formThemNhanVien : Form
    {
      
        SqlConnection connection;
        public formThemNhanVien()
        {
            InitializeComponent();
            connection = new SqlConnection(KetNoi.kn);
            txtMaNV.Enabled = false;
            txtNgayVaoLam.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void loadGioiTinh()
        {
            cBGioiTinh.Items.Clear();
            cBGioiTinh.Items.Add("Nam");
            cBGioiTinh.Items.Add("Nữ");
        }

        private void loadHocVan()
        {
            cBHocVan.Items.Clear();
            cBHocVan.Items.Add("Không có");
            cBHocVan.Items.Add("Cao Đẳng");
            cBHocVan.Items.Add("Đại Học");
        }

        private void loadCTNN()
        {
            cBCCNN.Items.Clear();
            cBCCNN.Items.Add("Không có");
            cBCCNN.Items.Add("Tiếng Anh");
            cBCCNN.Items.Add("Tiếng Trung");
            cBCCNN.Items.Add("Tiếng Nhật");
            cBCCNN.Items.Add("Tiếng Pháp");
            cBCCNN.Items.Add("Tiếng Hàn");
            cBCCNN.Items.Add("Tiếng Nga");
        }

        private void formThemNhanVien_Load(object sender, EventArgs e)
        {
            loadCTNN();
            loadGioiTinh();
            loadHocVan();
            LoadPhongBan();
            LoadChucVu();
        }

        private void LoadComboBox(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(KetNoi.kn))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = displayMember;
                        comboBox.ValueMember = valueMember;
                        comboBox.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để hiển thị!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu vào {comboBox.Name}: {ex.Message}");
            }
        }

        private void LoadPhongBan()
        {
            string query = "SELECT MaPB, TenPB FROM PhongBan";
            LoadComboBox(cBPhongBan, query, "TenPB", "MaPB");
        }

        private void LoadChucVu()
        {
            string query = "SELECT MaCV, TenCV FROM ChucVu";
            LoadComboBox(cBChucVu, query, "TenCV", "MaCV");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác và đóng form?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (/*string.IsNullOrWhiteSpace(txtMaNV.Text) ||*/
                string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(mTBNgaySinh.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                cBGioiTinh.SelectedIndex == -1 || cBHocVan.SelectedIndex == -1 || cBCCNN.SelectedIndex == -1 ||
                cBPhongBan.SelectedIndex == -1 || cBChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                string maPB = cBPhongBan.SelectedValue.ToString();
                string maCV = cBChucVu.SelectedValue.ToString();
                //string maNV = txtMaNV.Text;
                string hoTen = txtHoTen.Text;
                string cccd = txtCCCD.Text;
                string gioiTinh = cBGioiTinh.SelectedItem.ToString();
                DateTime ngaySinh = DateTime.Parse(mTBNgaySinh.Text);
                string diaChi = txtDiaChi.Text;
                string hocVan = cBHocVan.SelectedItem.ToString();
                string chungChiNN = cBCCNN.SelectedItem.ToString();
                DateTime ngayVaoLam = DateTime.Parse(txtNgayVaoLam.Text);
                double luongCoBan = 5000000;
                double heSoLuong = 1;
                string soDienThoai = txtSDT.Text;
                string email = txtEmail.Text;

                using (SqlConnection connection = new SqlConnection(KetNoi.kn))
                {
                    SqlCommand command = new SqlCommand("sp_ThemNhanVien", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaPB", maPB);
                    command.Parameters.AddWithValue("@MaCV", maCV);
                    command.Parameters.AddWithValue("@MaLoai", "NVTV");
                    //command.Parameters.AddWithValue("@MaNV", maNV);
                    command.Parameters.AddWithValue("@HoTenNV", hoTen);
                    command.Parameters.AddWithValue("@CCCD", cccd);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    command.Parameters.AddWithValue("@DiaChi", diaChi);
                    command.Parameters.AddWithValue("@HocVan", hocVan);
                    command.Parameters.AddWithValue("@ChungChiNN", chungChiNN);
                    command.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                    command.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    command.Parameters.AddWithValue("@HeSoLuong", heSoLuong);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
