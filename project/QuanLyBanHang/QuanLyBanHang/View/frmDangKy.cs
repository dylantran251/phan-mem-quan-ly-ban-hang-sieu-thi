using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyBanHang.View
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmDangNhap f = new frmDangNhap();
            this.Close();
            f.Show();
        }
        private void frmDangKy_Load(object sender, EventArgs e)
        {
            Class.Connection();
            Class.FillCombo("SELECT MaNhanVien, TenNhanVien FROM QLNHANVIEN", cbID, "MaNhanVien", "MaNhanVien");
            cbID.SelectedIndex = -1;
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string id = (Class.GetFieldValues("select MaNhanVien from TAIKHOAN"));

            if (cbID.Text.Length == 0) MessageBox.Show("Nhập ID", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(cbID.Text == id)MessageBox.Show("Tài khoản này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (cbID.Text != id)
            {
                SqlCommand cmd = new SqlCommand("insert into TAIKHOAN values (@MaNhanVien, @Pass)",Class.Con);
                cmd.Parameters.AddWithValue("MaNhanVien", cbID.Text);
                cmd.Parameters.AddWithValue("Pass", txtNhapLaiPass.Text);
                if (txtNhapLaiPass.Text == txtPass.Text && txtPass.Text.Length != 0)
                {
                    cmd.ExecuteNonQuery();
                    Class.LoadTbl("select *from TAIKHOAN");
                    MessageBox.Show("Đăng ký tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmDangNhap f = new frmDangNhap();
                    f.Show();
                    this.Close();
                }
                else if (txtNhapLaiPass.Text != txtPass.Text && txtPass.Text.Length != 0) MessageBox.Show("Mật khẩu không khớp. Nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (txtPass.Text.Length == 0)MessageBox.Show("Bạn chưa nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
