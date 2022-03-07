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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            Class.Connection();
            Class.FillCombo("SELECT MaNhanVien, MaNhanVien FROM TAIKHOAN", cbID, "MaNhanVien", "MaNhanVien");
            cbID.SelectedIndex = -1;
        }
        private void btnDangKi_Click(object sender, EventArgs e)
        {
            frmDangKy f = new frmDangKy();
            f.Show();
            this.Hide();
        }
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            //  string id = Class.GetFieldValues("Select MaNhanVien from TAIKHOAN");
            // string pass = Class.GetFieldValues("Select Pass from TAIKHOAN");
            Class.Connection();
            SqlCommand cmd = new SqlCommand("select *from TAIKHOAN where MaNhanVien = '"+cbID.Text+"' and pass = '"+txtPass.Text+"'", Class.Con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                frmMain f = new frmMain();
                f.lbXinChao.Text = cbID.Text;
                f.Show();
                this.Hide();
            }
            else if (cbID.Text.Length == 0 || txtPass.Text.Length == 0)
                MessageBox.Show("Tài khoản và mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Class.DisConnection();
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }
    }
}
