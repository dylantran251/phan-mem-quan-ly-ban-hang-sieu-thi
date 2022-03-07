using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyBanHang.View
{
    public partial class frmQuanLyKhachHang : Form
    {
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }
        private void RenameColumn()
        {      
            dgvQLKH.Columns[0].HeaderText = "Mã khách";
            dgvQLKH.Columns[1].HeaderText = "Họ và tên";
            dgvQLKH.Columns[2].HeaderText = "Giới tính";
            dgvQLKH.Columns[4].HeaderText = "Nhóm";
            dgvQLKH.Columns[3].HeaderText = "Địa chỉ";
            dgvQLKH.Columns[5].HeaderText = "Số điện thoại";
            dgvQLKH.Columns[6].HeaderText = "Số điểm";
            dgvQLKH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; ;
        }
        public void DisableText(bool check)
        {
            txtMaKhach.Enabled = check;
            txtTenKhach.Enabled = check;
            txtSDT.Enabled = check;
            txtDiaChi.Enabled = check;
            txtSoDiem.Enabled = check;
            cbGioiTinh.Enabled = check;
            cbNhomKhach.Enabled = check;
        }
        private void ResetValues()
        {
            txtMaKhach.Clear();
            txtTenKhach.Clear();
            cbGioiTinh.Text = null;
            txtDiaChi.Clear();
            txtSDT.Clear();
            cbNhomKhach.Text = null;
            txtSoDiem.Clear();
        }
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            DisableText(false);
            btnLuu.Hide();
            btnHuy.Hide();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            dgvQLKH.DataSource = Class.LoadTbl("select *from QLKHACH");
            RenameColumn();
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            DisableText(false);
            frmQuanLyKhachHang_Load(sender, e);
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnThem_Click_2(object sender, EventArgs e)
        {
            DisableText(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Show();
            btnHuy.Show();
            ResetValues();
            txtMaKhach.Text = Class.CreateKey("KH");
        }
        int i = 0;
        private void btnSua_Click(object sender, EventArgs e)
        {
            //
            i++;
            if (i % 2 == 0 && txtMaKhach.Text.Length != 0)
            {
                if (MessageBox.Show("Thông tin người này sẽ bị thay đổi?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Class.Connection();
                    SqlCommand cmd = new SqlCommand("Update QLKHACH set TenKhach=@TenKhach, GioiTinh=@GioiTinh, DiaChi=@DiaChi, NhomKhach=@NhomKhach, SDT=@SDT, SoDiem=@SoDiem where MaKhach = @MaKhach", Class.Con);
                    cmd.Parameters.AddWithValue("MaKhach", txtMaKhach.Text);
                    cmd.Parameters.AddWithValue("TenKhach", txtTenKhach.Text);
                    cmd.Parameters.AddWithValue("GioiTinh", cbGioiTinh.Text);
                    cmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("NhomKhach", cbNhomKhach.Text);
                    cmd.Parameters.AddWithValue("SoDiem", txtSoDiem.Text);
                    cmd.ExecuteNonQuery();
                    DisableText(false);
                    frmQuanLyKhachHang_Load(sender, e);//sau khi them cap nhat lai form
                    ResetValues();
                }
                else i = i + 1;
            }
            else if (i % 2 == 0 && txtMaKhach.Text.Length == 0)
                MessageBox.Show("Chọn khách hàng bạn muốn đổi thông tin");
            else if (i % 2 != 0 && txtMaKhach.Text.Length == 0)
            {
                i = i + 1;
                MessageBox.Show("Chọn khách hàng bạn muốn đổi thông tin");
            }
            else if (i % 2 != 0 && txtMaKhach.Text.Length != 0)
                DisableText(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from QLKHACH where MaKhach='" + txtMaKhach.Text + "'", Class.Con);
            if(MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                frmQuanLyKhachHang_Load(sender, e);
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            DisableText(false);
            SqlCommand sqlCmd = new SqlCommand("Insert into QLKHACH values(@MaKhach, @TenKhach, @GioiTinh, @DiaChi,@NhomKhach, @SDT, @SoDiem)", Class.Con);
            sqlCmd.Parameters.AddWithValue("MaKhach", txtMaKhach.Text);
            sqlCmd.Parameters.AddWithValue("TenKhach", txtTenKhach.Text);
            sqlCmd.Parameters.AddWithValue("GioiTinh", cbGioiTinh.Text);
            sqlCmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text);
            sqlCmd.Parameters.AddWithValue("SDT", txtSDT.Text);
            sqlCmd.Parameters.AddWithValue("NhomKhach", cbNhomKhach.Text);
            sqlCmd.Parameters.AddWithValue("SoDiem", txtSoDiem.Text);
            sqlCmd.ExecuteNonQuery();
            ResetValues();
            frmQuanLyKhachHang_Load(sender, e);//sau khi them cap nhat lai form;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            frmQuanLyKhachHang_Load(sender, e);
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select *from QLKHACH where @MaKhach=MaKhach or @TenKhach=TenKhach", Class.Con);
            cmd.Parameters.AddWithValue("MaKhach", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("TenKhach", txtTimKiem.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dTbl = new DataTable();
            dTbl.Load(dr);
            dgvQLKH.DataSource = dTbl;
        }

        private void dgvQLKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvQLKH.CurrentRow.Index;
            txtMaKhach.Text = dgvQLKH.Rows[i].Cells[0].Value.ToString();
            txtTenKhach.Text = dgvQLKH.Rows[i].Cells[1].Value.ToString();
            cbGioiTinh.Text = dgvQLKH.Rows[i].Cells[2].Value.ToString();
            cbNhomKhach.Text = dgvQLKH.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = dgvQLKH.Rows[i].Cells[3].Value.ToString();
            txtSDT.Text = dgvQLKH.Rows[i].Cells[5].Value.ToString();
            txtSoDiem.Text = dgvQLKH.Rows[i].Cells[6].Value.ToString();
        }

        private void frmQuanLyKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.DisConnection();
        }

        private void txtSoDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
