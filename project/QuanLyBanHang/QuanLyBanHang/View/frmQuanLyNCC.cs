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
    public partial class frmQuanLyNCC : Form
    {
        public frmQuanLyNCC()
        {
            InitializeComponent();
        }

        private void ResetValues()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtGhiChu.Clear();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnHuy.Show();
            btnLuu.Show();
            ResetValues();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNCC.Text = Class.CreateKey("NCC");
            DisableText(true);
        }
        private void RenameColumn()
        {
            dgvQLNCC.Columns[0].HeaderText = "Mã NCC";
            dgvQLNCC.Columns[1].HeaderText = "Tên NCC";
            dgvQLNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvQLNCC.Columns[3].HeaderText = "Số điện thoại";
            dgvQLNCC.Columns[4].HeaderText = "Email";
            dgvQLNCC.Columns[5].HeaderText = "Ghi chú";
            dgvQLNCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void DisableText(bool check)
        {
            txtMaNCC.Enabled = check;
            txtTenNCC.Enabled = check;
            txtDiaChi.Enabled = check;
            txtSDT.Enabled = check;
            txtEmail.Enabled = check;
            txtGhiChu.Enabled = check;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Hide();
            btnLuu.Hide();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            ResetValues();
            DisableText(false);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlCommand Cmd = new SqlCommand("Insert into QLNHACUNGCAP values(@MaNCC, @TenNCC, @DiaChi, @SDT, @Email, @GhiChu)", Class.Con);
            Cmd.Parameters.AddWithValue("MaNCC", txtMaNCC.Text);
            Cmd.Parameters.AddWithValue("TenNCC", txtTenNCC.Text);
            Cmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text);
            Cmd.Parameters.AddWithValue("SDT", txtSDT.Text);
            Cmd.Parameters.AddWithValue("Email", txtEmail.Text);
            Cmd.Parameters.AddWithValue("GhiChu", txtGhiChu.Text);
            Cmd.ExecuteNonQuery();
            ResetValues();
            DisableText(false);
            frmQuanLyNCC_Load(sender, e);//sau khi them cap nhat lai form;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            SqlCommand sqlCmd = new SqlCommand("Delete from QLNHACUNGCAP where MaNCC='" + txtMaNCC.Text + "'", Class.Con);
            sqlCmd.ExecuteNonQuery();
            frmQuanLyNCC_Load(sender, e);
            }
        }
        int i = 0;
        private void btnSua_Click(object sender, EventArgs e)
        {
            i++;
            if (i % 2 == 0 && txtMaNCC.Text.Length != 0)
            {
                if (MessageBox.Show("Thông tin NCC sẽ bị thay đổi?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Class.Connection();
                    SqlCommand sqlCmd = new SqlCommand("Update QLNHACUNGCAP set TenNCC = @TenNCC, DiaChi= @DiaChi, SDT = @SDT, Email = @Email, GhiChu = @GhiChu where MaNCC = @MaNCC", Class.Con);
                    sqlCmd.Parameters.AddWithValue("MaNCC", txtMaNCC.Text);
                    sqlCmd.Parameters.AddWithValue("TenNCC", txtTenNCC.Text);
                    sqlCmd.Parameters.AddWithValue("DiaChi", txtDiaChi.Text);
                    sqlCmd.Parameters.AddWithValue("SDT", txtSDT.Text);
                    sqlCmd.Parameters.AddWithValue("Email", txtEmail.Text);
                    sqlCmd.Parameters.AddWithValue("GhiChu", txtGhiChu.Text);
                    sqlCmd.ExecuteNonQuery();
                    frmQuanLyNCC_Load(sender, e);//sau khi them cap nhat lai form;
                    DisableText(false);
                    ResetValues();
                }
                else i = i + 1;
            }
            else if (i % 2 == 0 && txtMaNCC.Text.Length == 0)
                MessageBox.Show("Chọn NCC bạn muốn đổi thông tin");
            else if (i % 2 != 0 && txtMaNCC.Text.Length == 0)
            {
                i = i + 1;
                MessageBox.Show("Chọn NCC bạn muốn đổi thông tin");
            }
            else if (i % 2 != 0 && txtMaNCC.Text.Length != 0)
                DisableText(true);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand Cmd = new SqlCommand("select *from QLNHACUNGCAP where MaNCC = @MaNCC or TenNCC = @TenNCC" , Class.Con);
            Cmd.Parameters.AddWithValue("MaNCC", txtTimKiem.Text);
            Cmd.Parameters.AddWithValue("TenNCC", txtTimKiem.Text);
            Cmd.ExecuteNonQuery();
            Cmd.ExecuteNonQuery();
            SqlDataReader sqlDr = Cmd.ExecuteReader();
            DataTable dTbl = new DataTable();
            dTbl.Load(sqlDr);
            dgvQLNCC.DataSource = dTbl;
        }
        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvQLNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvQLNCC.CurrentRow.Index;
            txtMaNCC.Text = dgvQLNCC.Rows[i].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvQLNCC.Rows[i].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvQLNCC.Rows[i].Cells[2].Value.ToString();
            txtSDT.Text = dgvQLNCC.Rows[i].Cells[3].Value.ToString();
            txtEmail.Text = dgvQLNCC.Rows[i].Cells[4].Value.ToString();
            txtGhiChu.Text = dgvQLNCC.Rows[i].Cells[5].Value.ToString();
        }
        private void frmQuanLyNCC_Load(object sender, EventArgs e)
        {
            DisableText(false);
            btnLuu.Hide();
            btnHuy.Hide();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            dgvQLNCC.DataSource = Class.LoadTbl("select *from QLNHACUNGCAP where MaNCC != '"+0+"'");
            RenameColumn();
        }
        private void frmQuanLyNCC_FormClosing(object sender, FormClosingEventArgs e)
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
