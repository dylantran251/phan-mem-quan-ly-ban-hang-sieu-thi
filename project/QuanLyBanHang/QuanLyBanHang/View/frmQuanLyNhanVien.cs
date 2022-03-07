using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.View
{
    public partial class frmQuanLyNhanVien : Form
    {
        bool isCheck = true; //Kiem tra click vao them hoac sua chua
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }
        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            DisableText(false);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Hide();
            btnHuy.Hide();
            dgvQLNV.DataSource = Class.LoadTbl("select *from QLNHANVIEN");
            RenameColumn();
        }
        public void DisableText(bool check)
        {
            txtMaNV.Enabled = check;
            txtTenNV.Enabled = check;
            txtGhiChu.Enabled = check;
            txtSDT.Enabled = check;
            txtQueQuan.Enabled = check;
            cbGioiTinh.Enabled = check;
            cbNhiemVu.Enabled = check;
        }
        private void RenameColumn()
        {
            dgvQLNV.Columns[0].HeaderText = "Mã nhân viên";
            dgvQLNV.Columns[1].HeaderText = "Họ và tên";
            dgvQLNV.Columns[2].HeaderText = "Giới tính";
            dgvQLNV.Columns[3].HeaderText = "Ngày sinh";
            dgvQLNV.Columns[4].HeaderText = "Địa chỉ";
            dgvQLNV.Columns[5].HeaderText = "Số điện thoại";
            dgvQLNV.Columns[6].HeaderText = "Nhiệm vụ";
            dgvQLNV.Columns[7].HeaderText = "Ghi chú";
            dgvQLNV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
        }
        private void ResetValues()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtGhiChu.Clear();
            txtSDT.Clear();
            txtQueQuan.Clear();
            cbGioiTinh.Text = null;
            cbNhiemVu.Text = null;
        }
        ////
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            DisableText(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            ResetValues();
            btnLuu.Show();
            btnHuy.Show();
            txtMaNV.Text = Class.CreateKey("NV");
        }
        int i = 0;
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            //sau khi them cap nhat lai form
            i++;
            if (i % 2 == 0 && txtMaNV.Text.Length != 0)
            {
                if (MessageBox.Show("Bạn có chắn chắn muốn thay đổi thông tin nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlCommand Cmd = new SqlCommand("Update QLNHANVIEN set TenNhanVien=@TenNhanVien, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, DiaChi=@DiaChi, SDT=@SDT, NhiemVu=@NhiemVu, GhiChu=@GhiChu where @MaNhanVien = MaNhanVien", Class.Con);
                    Cmd.Parameters.AddWithValue("MaNhanVien", txtMaNV.Text);
                    Cmd.Parameters.AddWithValue("TenNhanVien", txtTenNV.Text);
                    Cmd.Parameters.AddWithValue("GioiTinh", cbGioiTinh.Text);
                    Cmd.Parameters.AddWithValue("NgaySinh", Convert.ToDateTime(dtpNgaySinh.Text));
                    Cmd.Parameters.AddWithValue("DiaChi", txtQueQuan.Text);
                    Cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
                    Cmd.Parameters.AddWithValue("NhiemVu", cbNhiemVu.Text);
                    Cmd.Parameters.AddWithValue("GhiChu", txtGhiChu.Text);
                    Cmd.ExecuteNonQuery();
                    DisableText(false);
                    frmQuanLyNhanVien_Load(sender, e);//sau khi them cap nhat lai form
                    ResetValues();
                }
                else i = i + 1;
            }
            else if (i % 2 == 0 && txtMaNV.Text.Length == 0)
                MessageBox.Show("Chọn nhân viên bạn muốn đổi thông tin");
            else if (i % 2 != 0 && txtMaNV.Text.Length == 0)
            {
                i = i + 1;
                MessageBox.Show("Chọn nhân viên bạn muốn đổi thông tin");
            }
            else if (i % 2 != 0 && txtMaNV.Text.Length != 0)
            {
                DisableText(true);
                cbNhiemVu.Enabled = false;
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            SqlCommand Cmd;
            string id = txtMaNV.Text;
            string lay_id = id.Substring(0, 2);
            string manv = Class.GetFieldValues("select MaNhanVien from HOADON");
            if (lay_id == "AD")
            {
                MessageBox.Show("Không thể xóa admin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cmd = new SqlCommand("Delete from TAIKHOAN where  MaNhanVien='" + txtMaNV.Text + "'", Class.Con);
                Cmd.ExecuteNonQuery();
                Cmd = new SqlCommand("Delete from QLNHANVIEN where MaNhanVien='" + txtMaNV.Text + "'", Class.Con); 
                if(txtMaNV.Text != manv)
                {
                    Cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Nhân viên này đang được lưu trong hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                frmQuanLyNhanVien_Load(sender, e);
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string id = txtMaNV.Text;
            string lay_id = id.Substring(0, 2);
            SqlCommand Cmd = new SqlCommand("Insert into QLNHANVIEN values(@MaNhanVien, @TenNhanVien, @GioiTinh, @NgaySinh,@DiaChi, @SDT, @NhiemVu, @GhiChu)", Class.Con);
            Cmd.Parameters.AddWithValue("MaNhanVien", txtMaNV.Text);
            Cmd.Parameters.AddWithValue("TenNhanVien", txtTenNV.Text);
            Cmd.Parameters.AddWithValue("GioiTinh", cbGioiTinh.Text);
            Cmd.Parameters.AddWithValue("NgaySinh", dtpNgaySinh.Value.ToString("yyyy-MM-dd"));
            Cmd.Parameters.AddWithValue("DiaChi", txtQueQuan.Text);
            Cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
            Cmd.Parameters.AddWithValue("NhiemVu", cbNhiemVu.Text);
            Cmd.Parameters.AddWithValue("GhiChu", txtGhiChu.Text);
            if(cbNhiemVu.Text.Length == 0 && lay_id == "NV")
            {
                MessageBox.Show("Bạn phải phân quyền cho nhân viên này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Cmd.ExecuteNonQuery();
                frmQuanLyNhanVien_Load(sender, e);//sau khi them cap nhat lai form;
                ResetValues();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            frmQuanLyNhanVien_Load(sender, e);
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand Cmd = new SqlCommand("Select *from QLNHANVIEN where @MaNhanVien=MaNhanVien  or @TenNhanVien= TenNhanVien  ", Class.Con);
            Cmd.Parameters.AddWithValue("MaNhanVien", txtTimKiem.Text);
            Cmd.Parameters.AddWithValue("TenNhanVien", txtTimKiem.Text);
            Cmd.ExecuteNonQuery();
            SqlDataReader Dr = Cmd.ExecuteReader();
            DataTable dTbl = new DataTable();
            dTbl.Load(Dr);
            dgvQLNV.DataSource = dTbl;
            //sau khi them cap nhat lai form;
        }
        //Hiển thị thông tin từ dgv ra textbox
        private void dgvQLNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvQLNV.CurrentRow.Index;
            txtMaNV.Text = dgvQLNV.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = dgvQLNV.Rows[i].Cells[1].Value.ToString();
            cbGioiTinh.Text = dgvQLNV.Rows[i].Cells[2].Value.ToString();
            dtpNgaySinh.Text = dgvQLNV.Rows[i].Cells[3].Value.ToString();
            txtQueQuan.Text = dgvQLNV.Rows[i].Cells[4].Value.ToString();
            txtSDT.Text = dgvQLNV.Rows[i].Cells[5].Value.ToString();
            cbNhiemVu.Text = dgvQLNV.Rows[i].Cells[6].Value.ToString();
            txtGhiChu.Text = dgvQLNV.Rows[i].Cells[7].Value.ToString();
        }
        //Tạo 2 ký tự đầu của mã nv theo từng nhiệm vụ, chức vụ khác nhau
        private void cbNhiemVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbNhiemVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            if (cbNhiemVu.Text == "Admin")
            {
                txtMaNV.Text = Class.CreateKey("AD");
            }
            else if (cbNhiemVu.Text == "Manager")
            {
                txtMaNV.Text = Class.CreateKey("MN");
            }
            else
                txtMaNV.Text = Class.CreateKey("NV");
           
        }
        //Không cho nhập vào combobox

        private void frmQuanLyNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.DisConnection();
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
