using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QLNS.PhongBan
{
    public partial class DSNhanVienPhongBan : Form
    {
        string connectionString = "Data Source=LAPTOP-JTJE2T8G\\SQLEXPRESS;Initial Catalog=QLNhanSu;Integrated Security=True;";
        SqlConnection connection;

        public DSNhanVienPhongBan()
        {
            InitializeComponent();
            connection = new SqlConnection(KetNoi.kn);
            txtMaNV.Enabled = false;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT N.MaNV, P.MaPB, N.HoTenNV, C.MaCV, P.TenPB, C.TenCV, L.TenLoai, N.MaLoai " +
                               "FROM NhanVien N " +
                               "JOIN PhongBan P ON N.MaPB = P.MaPB " +
                               "JOIN ChucVu C ON N.MaCV = C.MaCV " +
                               "JOIN MaLoaiNV L ON N.MaLoai = L.MaLoai " +
                               "WHERE N.MaLoai != 'NVNV'";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dGVListNVPB.DataSource = dataTable;

                if (dGVListNVPB.Rows.Count > 0)
                {
                    dGVListNVPB.Rows[0].Selected = true;
                    dGVListNVPB.CurrentCell = dGVListNVPB.Rows[0].Cells[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                string pbQuery = "SELECT MaPB, TenPB FROM PhongBan";
                SqlDataAdapter pbAdapter = new SqlDataAdapter(pbQuery, connection);
                DataTable pbTable = new DataTable();
                pbAdapter.Fill(pbTable);
                cBPhongBan.DataSource = pbTable;
                cBPhongBan.DisplayMember = "TenPB";
                cBPhongBan.ValueMember = "MaPB";

                string cvQuery = "SELECT MaCV, TenCV FROM ChucVu";
                SqlDataAdapter cvAdapter = new SqlDataAdapter(cvQuery, connection);
                DataTable cvTable = new DataTable();
                cvAdapter.Fill(cvTable);
                cBChucVu.DataSource = cvTable;
                cBChucVu.DisplayMember = "TenCV";
                cBChucVu.ValueMember = "MaCV";

                string loaiNVQuery = "SELECT MaLoai, TenLoai FROM MaLoaiNV";
                SqlDataAdapter loaiNVAdapter = new SqlDataAdapter(loaiNVQuery, connection);
                DataTable loaiNVTable = new DataTable();
                loaiNVAdapter.Fill(loaiNVTable);
                cBLoaiNV.DataSource = loaiNVTable;
                cBLoaiNV.DisplayMember = "TenLoai";
                cBLoaiNV.ValueMember = "MaLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DSNhanVienPhongBan_Load(object sender, EventArgs e)
        {
            dGVListNVPB.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dGVListNVPB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DisableTextBox();
            LoadData();
            LoadComboBoxes();
            HideColumns();
            CustomizeColumnHeaders();
            CustomizeColumnHeadersStyle();
            SetColumnWidths();
        }

        private void SetColumnWidths()
        {
            dGVListNVPB.Columns["MaNV"].Width = 150;
            dGVListNVPB.Columns["MaPB"].Width = 150;
            dGVListNVPB.Columns["HoTenNV"].Width = 250;
        }

        private void CustomizeColumnHeadersStyle()
        {
            dGVListNVPB.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dGVListNVPB.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void HideColumns()
        {
            if (dGVListNVPB.Columns.Contains("MaLoai"))
            {
                dGVListNVPB.Columns["MaLoai"].Visible = false;
            }
            if (dGVListNVPB.Columns.Contains("MaCV"))
            {
                dGVListNVPB.Columns["MaCV"].Visible = false;
            }
        }

        private void dGVListNVPB_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.CellStyle.Padding = new Padding(5, 0, 5, 0);
            }
        }

        private void dGVListNVPB_SelectionChanged(object sender, EventArgs e)
        {
            if (dGVListNVPB.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dGVListNVPB.SelectedRows[0];

                txtMaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["HoTenNV"].Value.ToString();

                cBPhongBan.SelectedValue = selectedRow.Cells["MaPB"].Value;
                cBChucVu.SelectedValue = selectedRow.Cells["MaCV"].Value;
                cBLoaiNV.SelectedValue = selectedRow.Cells["MaLoai"].Value;
            }
        }

        private void CustomizeColumnHeaders()
        {
            if (dGVListNVPB.Columns.Contains("MaNV"))
                dGVListNVPB.Columns["MaNV"].HeaderText = "Mã nhân viên";

            if (dGVListNVPB.Columns.Contains("MaPB"))
                dGVListNVPB.Columns["MaPB"].HeaderText = "Mã phòng ban";

            if (dGVListNVPB.Columns.Contains("HoTenNV"))
                dGVListNVPB.Columns["HoTenNV"].HeaderText = "Họ tên nhân viên";

            if (dGVListNVPB.Columns.Contains("TenLoai"))
                dGVListNVPB.Columns["TenLoai"].HeaderText = "Loại nhân viên";

            if (dGVListNVPB.Columns.Contains("TenPB"))
                dGVListNVPB.Columns["TenPB"].HeaderText = "Phòng ban";

            if (dGVListNVPB.Columns.Contains("TenCV"))
                dGVListNVPB.Columns["TenCV"].HeaderText = "Chức vụ";
        }

        private void dGVListNVPB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn clickedColumn = dGVListNVPB.Columns[e.ColumnIndex];

                if (clickedColumn.Name == "MaNV")
                {
                    DataGridViewRow selectedRow = dGVListNVPB.Rows[e.RowIndex];

                    txtMaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
                    txtHoTen.Text = selectedRow.Cells["HoTenNV"].Value.ToString();

                    cBPhongBan.SelectedValue = selectedRow.Cells["MaPB"].Value;
                    cBChucVu.SelectedValue = selectedRow.Cells["MaCV"].Value;
                    cBLoaiNV.SelectedValue = selectedRow.Cells["MaLoai"].Value;

                    string tenPB = selectedRow.Cells["TenPB"].Value.ToString();
                    string tenCV = selectedRow.Cells["TenCV"].Value.ToString();

                    if (cBPhongBan.Items.Contains(tenPB))
                    {
                        cBPhongBan.SelectedItem = tenPB;
                    }

                    if (cBChucVu.Items.Contains(tenCV))
                    {
                        cBChucVu.SelectedItem = tenCV;
                    }
                }
            }
        }
        private void EnableTextBox()
        {
            cBChucVu.Enabled = true;
            cBPhongBan.Enabled = true;
        }
        private void DisableTextBox()
        {
            txtHoTen.Enabled = false;
            cBLoaiNV.Enabled = false;
            cBChucVu.Enabled = false;
            cBPhongBan.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dGVListNVPB.CellClick -= dGVListNVPB_CellClick;
            dGVListNVPB.SelectionChanged -= dGVListNVPB_SelectionChanged;
            btnUpdate.Enabled = false;
            EnableTextBox();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác và đóng form?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DisableTextBox();
                btnUpdate.Enabled = true;
                dGVListNVPB.CellClick += dGVListNVPB_CellClick;
                dGVListNVPB.SelectionChanged += dGVListNVPB_SelectionChanged;
                this.Close();
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
                if (string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    MessageBox.Show("Mã nhân viên không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
        UPDATE NhanVien
        SET MaPB = @MaPB, MaCV = @MaCV
        WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@MaPB", cBPhongBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaCV", cBChucVu.SelectedValue);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        DisableTextBox();
                        btnUpdate.Enabled = true;
                        dGVListNVPB.CellClick += dGVListNVPB_CellClick;
                        dGVListNVPB.SelectionChanged += dGVListNVPB_SelectionChanged;
                    }
                    else
                    {
                        DisableTextBox();
                        btnUpdate.Enabled = true;
                        dGVListNVPB.CellClick += dGVListNVPB_CellClick;
                        dGVListNVPB.SelectionChanged += dGVListNVPB_SelectionChanged;
                        MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableTextBox();
                btnUpdate.Enabled = true;
                dGVListNVPB.CellClick += dGVListNVPB_CellClick;
                dGVListNVPB.SelectionChanged += dGVListNVPB_SelectionChanged;
            }
            finally
            {
                // Đảm bảo kết nối đóng lại
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchValue))
            {
                (dGVListNVPB.DataSource as DataTable).DefaultView.RowFilter = string.Format("MaNV LIKE '%{0}%'", searchValue);
            }
            else
            {
                (dGVListNVPB.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                bool found = false;

                foreach (DataGridViewRow row in dGVListNVPB.Rows)
                {
                    if (row.Cells["MaNV"].Value != null &&
                        row.Cells["MaNV"].Value.ToString().Trim() == searchValue)
                    {
                        row.Selected = true;
                        dGVListNVPB.CurrentCell = row.Cells[0];
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Không có nhân viên với mã NV này.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để tìm.");
            }
        }

    }
}
