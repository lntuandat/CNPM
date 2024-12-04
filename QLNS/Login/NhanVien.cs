using QLNS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QLNS.Login
{
    public partial class NhanVien : Form
    {
       
        SqlConnection connection;
        public NhanVien()
        {
            InitializeComponent();
            connection = new SqlConnection(KetNoi.kn);
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            // Kiểm tra giá trị UserID từ SessionManager
            string userName = SessionManager.Username;
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Không tìm thấy UserID trong session.");
                return;
            }

            // Câu truy vấn SQL
            string query = @"SELECT DISTINCT
                        N.MaNV, N.HoTenNV, N.NgaySinh, N.GioiTinh, N.DiaChi, N.CCCD, 
                        N.SoDienThoai, N.Email, N.HocVan, N.ChungChiNN, 
                        L.TenLoai, N.NgayVaoLam, B.SoBHXH, H.MaHD, 
                        H.NgayBatDau, H.NgayKetThuc
                    FROM 
                        NhanVien N
                    LEFT JOIN 
                        MaLoaiNV L ON N.MaLoai = L.MaLoai
                    LEFT JOIN 
                        BaoHiemXaHoi B ON N.MaNV = B.MaNV
                    LEFT JOIN 
                        HopDongNhanVien H ON N.MaNV = H.MaNV
                    WHERE 
                        N.MaNV = @MaNV"; // So sánh trực tiếp với MaNV

            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(KetNoi.kn))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", userName); // Dùng UserID từ SessionManager

                try
                {
                    // Mở kết nối và thực hiện truy vấn
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) // Kiểm tra nếu có dữ liệu
                    {
                        // Gán dữ liệu vào các TextBox, kiểm tra nếu có giá trị
                        txtMaNV.Text = reader["MaNV"]?.ToString() ?? string.Empty;
                        txtHoTen.Text = reader["HoTenNV"]?.ToString() ?? string.Empty;
                        txtNgaySinh.Text = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy") : string.Empty;
                        txtGioiTinh.Text = reader["GioiTinh"]?.ToString() ?? string.Empty;
                        txtDiaChi.Text = reader["DiaChi"]?.ToString() ?? string.Empty;
                        txtCCCD.Text = reader["CCCD"]?.ToString() ?? string.Empty;
                        txtSDT.Text = reader["SoDienThoai"]?.ToString() ?? string.Empty;
                        txtEmail.Text = reader["Email"]?.ToString() ?? string.Empty;
                        txtHocVan.Text = reader["HocVan"]?.ToString() ?? string.Empty;
                        txtCCNN.Text = reader["ChungChiNN"]?.ToString() ?? string.Empty;
                        txtLoaiNV.Text = reader["TenLoai"]?.ToString() ?? string.Empty;
                        txtNgayVaoLam.Text = reader["NgayVaoLam"] != DBNull.Value ? Convert.ToDateTime(reader["NgayVaoLam"]).ToString("dd/MM/yyyy") : string.Empty;
                        txtSoBHXH.Text = reader["SoBHXH"]?.ToString() ?? string.Empty;
                        txtMaHD.Text = reader["MaHD"]?.ToString() ?? string.Empty;
                        txtBDHD.Text = reader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(reader["NgayBatDau"]).ToString("dd/MM/yyyy") : string.Empty;
                        txtKTHD.Text = reader["NgayKetThuc"] != DBNull.Value ? Convert.ToDateTime(reader["NgayKetThuc"]).ToString("dd/MM/yyyy") : string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho nhân viên.");
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị lỗi nếu có
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void NhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.UserID = null;
            SessionManager.Username = null;
            SessionManager.UserRole = null;
        }
        

    }
}
