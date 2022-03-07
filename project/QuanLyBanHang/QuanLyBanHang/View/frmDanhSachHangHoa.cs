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
    public partial class frmDanhSachHangHoa : Form
    {
        public frmDanhSachHangHoa()
        {
            InitializeComponent();
        }
        private void frmDanhSachHangHoa_Load(object sender, EventArgs e)
        {
            btnLuu.Hide();
            btnHuy.Hide();
            dgvDSHH.DataSource = Class.LoadTbl("select *from QLHANGHOA");
            RenameColumn();
            DisableText(false);
        }
        private void DisableText(bool check)
        {
            txtMaHang.Enabled = check;
            txtTenHang.Enabled = check;
            txtSoLuong.Enabled = check;
            txtDVT.Enabled = check;
            cbNhomHang.Enabled = check;
            txtHinhAnh.Enabled = check;
            txtDonGiaBan.Enabled = check;
            txtDonGiaNhap.Enabled = check;
            txtSoLuongKho.Enabled = check;
        }
        private void RenameColumn()
        {
            dgvDSHH.Columns[0].HeaderText = "Mã hàng";
            dgvDSHH.Columns[1].HeaderText = "Tên hàng";
            dgvDSHH.Columns[2].HeaderText = "Số lượng";
            dgvDSHH.Columns[3].HeaderText = "Đơn vị tính";
            dgvDSHH.Columns[4].HeaderText = "Nhóm hàng";
            dgvDSHH.Columns[5].HeaderText = "File ảnh";
            dgvDSHH.Columns[6].HeaderText = "Đơn giá bán";
            dgvDSHH.Columns[7].HeaderText = "Đơn giá nhập";
            dgvDSHH.Columns[8].HeaderText = "Ngày tạo";
            dgvDSHH.Columns[9].HeaderText = "Số lượng kho";
        }
        private void ResetValues()
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtSoLuong.Clear();
            txtDVT.Clear();
            cbNhomHang.Text = null;
            txtHinhAnh.Clear();
            txtDonGiaBan.Clear();
            txtDonGiaNhap.Clear();
            txtSoLuongKho.Clear();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DisableText(true);
            btnHuy.Show();
            btnLuu.Show();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaHang.Text = Class.CreateKey("HANG");
        }
        int i = 0;
        private void btnSua_Click(object sender, EventArgs e)
        {

            //sau khi them cap nhat lai form
            i++;
            if (i % 2 == 0 && txtMaHang.Text.Length != 0)
            {
                SqlCommand cmd = new SqlCommand("update QLHANGHOA set TenHang=@TenHang, SoLuong=@SoLuong,DVT= @DVT, NhomHang=@NhomHang, HinhAnh=@HinhAnh,DonGiaBan= @DonGiaBan,DonGiaMua= @DonGiaMua,NgayTao= @NgayTao,SoLuongKho=@SoLuongKho where @MaHang = MaHang", Class.Con); ;
                cmd.Parameters.AddWithValue("MaHang", txtMaHang.Text);
                cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
                cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("DVT", txtDVT.Text);
                cmd.Parameters.AddWithValue("NhomHang", cbNhomHang.Text);
                cmd.Parameters.AddWithValue("HinhAnh", txtHinhAnh.Text);
                cmd.Parameters.AddWithValue("DonGiaBan", txtDonGiaBan.Text);
                cmd.Parameters.AddWithValue("DonGiaMua", txtDonGiaNhap.Text);
                cmd.Parameters.AddWithValue("NgayTao", dtpNgayTao.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("SoLuongKho", txtSoLuongKho.Text);
                cmd.ExecuteNonQuery();
                frmDanhSachHangHoa_Load(sender, e);
                ResetValues();
                DisableText(false);
            }
            else if (i % 2 == 0 && txtMaHang.Text.Length == 0)
                MessageBox.Show("Chọn vật phẩm bạn muốn đổi thông tin");
            else if (i % 2 != 0 && txtMaHang.Text.Length == 0)
            {
                i = i + 1;
                MessageBox.Show("Chọn vật phẩm bạn muốn đổi thông tin");
            }
            else if (i % 2 != 0 && txtMaHang.Text.Length != 0)
                DisableText(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Class.Connection();
                SqlCommand cmd = new SqlCommand("delete from QLHANGHOA where MaHang = '"+txtMaHang.Text+"'", Class.Con);
                cmd.ExecuteNonQuery();
                frmDanhSachHangHoa_Load(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into QLHANGHOA values(@MaHang, @TenHang, @SoLuong, @DVT, @NhomHang, @HinhAnh, @DonGiaBan, @DonGiaMua, @NgayTao,@SoLuongKho)", Class.Con); ;
            cmd.Parameters.AddWithValue("MaHang", txtMaHang.Text);
            cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
            cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
            cmd.Parameters.AddWithValue("DVT", txtDVT.Text);
            cmd.Parameters.AddWithValue("NhomHang", cbNhomHang.Text);
            cmd.Parameters.AddWithValue("HinhAnh", txtHinhAnh.Text);
            cmd.Parameters.AddWithValue("DonGiaBan", txtDonGiaBan.Text);
            cmd.Parameters.AddWithValue("DonGiaMua", txtDonGiaNhap.Text);
            cmd.Parameters.AddWithValue("NgayTao", dtpNgayTao.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("SoLuongKho", txtSoLuongKho.Text);
            cmd.ExecuteNonQuery();
            frmDanhSachHangHoa_Load(sender, e);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            ResetValues();
            DisableText(false);
        }

        private void dgvDSHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDSHH.CurrentRow.Index;
            txtMaHang.Text = dgvDSHH.Rows[i].Cells[0].Value.ToString();
            txtTenHang.Text = dgvDSHH.Rows[i].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvDSHH.Rows[i].Cells[2].Value.ToString();
            txtDVT.Text = dgvDSHH.Rows[i].Cells[3].Value.ToString();
            cbNhomHang.Text = dgvDSHH.Rows[i].Cells[4].Value.ToString();
            txtHinhAnh.Text = dgvDSHH.Rows[i].Cells[5].Value.ToString();
            txtDonGiaBan.Text = dgvDSHH.Rows[i].Cells[6].Value.ToString();
            txtDonGiaNhap.Text = dgvDSHH.Rows[i].Cells[7].Value.ToString();
            dtpNgayTao.Text = dgvDSHH.Rows[i].Cells[8].Value.ToString();
            txtSoLuongKho.Text = dgvDSHH.Rows[i].Cells[9].Value.ToString();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DisableText(false);
            frmDanhSachHangHoa_Load(sender, e);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            ResetValues();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select *from QLHANGHOA where MaHang = @MaHang or TenHang = @TenHang", Class.Con);
            cmd.Parameters.AddWithValue("MaHang", txtTimKiem.Text);
            cmd.Parameters.AddWithValue("TenHang", txtTimKiem.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSHH.DataSource = dt;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
