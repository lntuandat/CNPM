using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS.NhanSu.ChildForm
{
    public partial class formHopDongNhanVien : Form
    {
        string connectionString = "Data Source=LAPTOP-JTJE2T8G\\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True;";
        SqlConnection connection;
        public formHopDongNhanVien()
        {
            InitializeComponent();
            connection = new SqlConnection(KetNoi.kn);
            txtMaNV.Enabled = false;
            txtMaHD.Enabled = false;
        }

        private void formHopDongNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            CustomizeColumnHeadersStyle();
            dGVHDNV.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dGVHDNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CustomizeColumnHeaders();
            SetColumnWidths();
            DisableTextBox();
            btnCapNhat.Visible = false;
            btnHuy.Visible = false;
        }

        private void LoadData()
        {
            try
            {
                string query = @"
            SELECT 
                NV.MaNV,
                HD.MaHD,
                NV.HoTenNV,  
                HD.ThoiHanHopDong, 
                HD.NgayBatDau, 
                HD.NgayKetThuc, 
                HD.MucLuong
            FROM 
                NhanVien NV
            INNER JOIN 
                HopDongNhanVien HD ON NV.MaNV = HD.MaNV";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                System.Data.DataTable dataTable = new System.Data.DataTable();

                dataAdapter.Fill(dataTable);

                dGVHDNV.DataSource = dataTable;

                if (dGVHDNV.Rows.Count > 0)
                {
                    dGVHDNV.Rows[0].Selected = true;
                    dGVHDNV.CurrentCell = dGVHDNV.Rows[0].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dGVHDNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.CellStyle.Padding = new Padding(5, 0, 5, 0);
            }
        }
        private void CustomizeColumnHeadersStyle()
        {
            dGVHDNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dGVHDNV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void CustomizeColumnHeaders()
        {
            if (dGVHDNV.Columns.Contains("MaNV"))
                dGVHDNV.Columns["MaNV"].HeaderText = "Mã nhân viên";

            if (dGVHDNV.Columns.Contains("HoTenNV"))
                dGVHDNV.Columns["HoTenNV"].HeaderText = "Họ tên nhân viên";

            if (dGVHDNV.Columns.Contains("MaHD"))
                dGVHDNV.Columns["MaHD"].HeaderText = "Mã họp đồng";

            if (dGVHDNV.Columns.Contains("ThoiHanHopDong"))
                dGVHDNV.Columns["ThoiHanHopDong"].HeaderText = "Thời hạn họp đồng";

            if (dGVHDNV.Columns.Contains("NgayBatDau"))
                dGVHDNV.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";

            if (dGVHDNV.Columns.Contains("NgayKetThuc"))
                dGVHDNV.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";

            if (dGVHDNV.Columns.Contains("MucLuong"))
                dGVHDNV.Columns["MucLuong"].HeaderText = "Mức lương";
        }
        private void SetColumnWidths()
        {
            dGVHDNV.Columns["MaNV"].Width = 150;
            dGVHDNV.Columns["MaHD"].Width = 150;
            dGVHDNV.Columns["HoTenNV"].Width = 250;
        }

        private void dGVHDNV_SelectionChanged(object sender, EventArgs e)
        {
            if (dGVHDNV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dGVHDNV.SelectedRows[0];

                txtMaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
                txtMaHD.Text = selectedRow.Cells["MaHD"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["HoTenNV"].Value.ToString();
                txtThoiHanHD.Text = selectedRow.Cells["ThoiHanHopDong"].Value.ToString();

                DateTime ngayBatDau;
                if (DateTime.TryParse(selectedRow.Cells["NgayBatDau"].Value.ToString(), out ngayBatDau))
                {
                    txtNgayBD.Text = ngayBatDau.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtNgayBD.Text = string.Empty;
                    MessageBox.Show("Ngày sinh không hợp lệ.");
                }

                DateTime ngayKetThuc;
                if (DateTime.TryParse(selectedRow.Cells["NgayKetThuc"].Value.ToString(), out ngayKetThuc))
                {
                    txtNgayKT.Text = ngayKetThuc.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtNgayKT.Text = string.Empty;
                    MessageBox.Show("Ngày sinh không hợp lệ.");
                }

                txtMucLuong.Text = selectedRow.Cells["MucLuong"].Value.ToString();
            }
        }

        private void dGVHDNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn clickedColumn = dGVHDNV.Columns[e.ColumnIndex];

                if (clickedColumn.Name == "MaNV")
                {
                    DataGridViewRow selectedRow = dGVHDNV.Rows[e.RowIndex];

                    txtMaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
                    txtMaHD.Text = selectedRow.Cells["MaHD"].Value.ToString();
                    txtHoTen.Text = selectedRow.Cells["HoTenNV"].Value.ToString();
                    txtThoiHanHD.Text = selectedRow.Cells["ThoiHanHopDong"].Value.ToString();

                    DateTime ngayBatDau;
                    if (DateTime.TryParse(selectedRow.Cells["NgayBatDau"].Value.ToString(), out ngayBatDau))
                    {
                        txtNgayBD.Text = ngayBatDau.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtNgayBD.Text = string.Empty;
                        MessageBox.Show("Ngày sinh không hợp lệ.");
                    }

                    DateTime ngayKetThuc;
                    if (DateTime.TryParse(selectedRow.Cells["NgayKetThuc"].Value.ToString(), out ngayKetThuc))
                    {
                        txtNgayKT.Text = ngayKetThuc.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtNgayKT.Text = string.Empty;
                        MessageBox.Show("Ngày sinh không hợp lệ.");
                    }

                    txtMucLuong.Text = selectedRow.Cells["MucLuong"].Value.ToString();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableTextBox();
            btnSua.Enabled = false;
            btnCapNhat.Visible = true;
            btnHuy.Visible = true;
            btnHuyHD.Visible = false;
            btnThemHD.Visible = false;
            dGVHDNV.CellClick -= dGVHDNV_CellClick;
            dGVHDNV.SelectionChanged -= dGVHDNV_SelectionChanged;
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();

            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng nhập mã hợp đồng để hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy hợp đồng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(KetNoi.kn))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM HopDongNhanVien WHERE MaHD = @MaHD", connection);
                    command.Parameters.AddWithValue("@MaHD", maHD);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Hợp đồng đã bị hủy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hợp đồng với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void EnableTextBox()
        {
            txtHoTen.Enabled = true;
            txtThoiHanHD.Enabled = true;
            txtNgayBD.Enabled = true;
            txtNgayKT.Enabled = true;
            txtMucLuong.Enabled = true;
        }

        private void DisableTextBox()
        {
            txtHoTen.Enabled = false;
            txtThoiHanHD.Enabled = false;
            txtNgayBD.Enabled = false;
            txtNgayKT.Enabled = false;
            txtMucLuong.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác sửa nhân viên", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DisableTextBox();
                btnSua.Enabled = true;
                btnCapNhat.Visible = false;
                btnHuy.Visible = false;
                btnHuyHD.Visible = true;
                btnThemHD.Visible = true;
                dGVHDNV.CellClick += dGVHDNV_CellClick;
                dGVHDNV.SelectionChanged += dGVHDNV_SelectionChanged;
            }
            else
            {
                return;
            }

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtMaHD.Text))
                {
                    MessageBox.Show("Mã nhân viên và Mã hợp đồng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
            UPDATE HopDongNhanVien
            SET 
                ThoiHanHopDong = @ThoiHanHopDong,
                NgayBatDau = @NgayBatDau,
                NgayKetThuc = @NgayKetThuc,
                MucLuong = @MucLuong
            WHERE MaHD = @MaHD;

            UPDATE NhanVien
            SET 
                HoTenNV = @HoTenNV
            WHERE MaNV = @MaNV;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@MaHD", txtMaHD.Text);
                    cmd.Parameters.AddWithValue("@HoTenNV", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@ThoiHanHopDong", txtThoiHanHD.Text);

                    DateTime ngayBatDau;
                    if (DateTime.TryParse(txtNgayBD.Text, out ngayBatDau))
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    else
                        cmd.Parameters.AddWithValue("@NgayBatDau", DBNull.Value);

                    DateTime ngayKetThuc;
                    if (DateTime.TryParse(txtNgayKT.Text, out ngayKetThuc))
                        cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    else
                        cmd.Parameters.AddWithValue("@NgayKetThuc", DBNull.Value);

                    decimal mucLuong;
                    if (decimal.TryParse(txtMucLuong.Text, out mucLuong))
                        cmd.Parameters.AddWithValue("@MucLuong", mucLuong);
                    else
                        cmd.Parameters.AddWithValue("@MucLuong", 0);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisableTextBox();
                        LoadData();
                        btnSua.Enabled = true;
                        btnCapNhat.Visible = false;
                        btnHuy.Visible = false;
                        btnHuyHD.Visible = true;
                        btnThemHD.Visible = true;
                        dGVHDNV.CellClick += dGVHDNV_CellClick;
                        dGVHDNV.SelectionChanged += dGVHDNV_SelectionChanged;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DisableTextBox();
                        btnSua.Enabled = true;
                        btnCapNhat.Visible = false;
                        btnHuy.Visible = false;
                        btnHuyHD.Visible = true;
                        btnThemHD.Visible = true;
                        dGVHDNV.CellClick += dGVHDNV_CellClick;
                        dGVHDNV.SelectionChanged += dGVHDNV_SelectionChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableTextBox();
                btnSua.Enabled = true;
                btnCapNhat.Visible = false;
                btnHuy.Visible = false;
                btnHuyHD.Visible = true;
                btnThemHD.Visible = true;
                dGVHDNV.CellClick += dGVHDNV_CellClick;
                dGVHDNV.SelectionChanged += dGVHDNV_SelectionChanged;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            ThemHopDong thd = new ThemHopDong();
            thd.StartPosition = FormStartPosition.CenterScreen;
            thd.ShowDialog();
        }
    }
}
