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
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            Class.Connection();
        }
        SqlDataReader dr;
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if(txtPassCu.Text.Length == 0)
            {
                MessageBox.Show("Nhập mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (txtPass.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(txtPass.Text != txtNhapLaiPass.Text)
            {
                MessageBox.Show("Mật khẩu nhập không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
            else if(txtNhapLaiPass.Text.Length == 0) 
            {
                MessageBox.Show("Bạn chưa xác nhận mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(txtPassCu.Text.Length !=0 )
            {
                SqlCommand cmd = new SqlCommand("select *from TAIKHOAN where MaNhanVien = '"+txtID.Text+"' and Pass = '"+txtPassCu.Text+"'", Class.Con);
                dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    dr.Close();
                    cmd = new SqlCommand("Update TAIKHOAN set Pass=@Pass", Class.Con);
                    cmd.Parameters.AddWithValue("Pass", txtNhapLaiPass.Text);
                    cmd.ExecuteNonQuery();
                    Class.LoadTbl("select *from TAIKHOAN");
                    MessageBox.Show("Thay đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNhapLaiPass.Clear();
                    txtPass.Clear();
                    txtPassCu.Clear();
                }
                else MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtPassCu.Text.Length != 0)
            {
                if (txtPassCu.Text == txtPass.Text)
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
