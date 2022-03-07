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
using System.Configuration;
using COMExcel = Microsoft.Office.Interop.Excel;


namespace QuanLyBanHang.View
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        SqlCommand cmd = new SqlCommand();
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            Class.Connection();
            btnThem.Enabled = true;
            btnLuu.Hide();
            btnHuy.Hide();
            GetDataCB();
            LoadInfoHoaDon();
            //btnXoa.Enabled = true;
            //btnInHoaDon.Enabled = true;
            LoadHoaDon();
            Class.RenameColumnCTHD(dgvHoaDonBan);
        }
        // Không thể nhập vào textbox nếu chưa nhấn thêm
        private void ResetValuesHoaDon()
        {
            txtSoHD.Clear();
            cbMaKhach.Text = null;
            cbMaNV.Text = null;
            txtTenKhach.Clear();
            txtTongTien.Clear();
        }
        private void ResetValuesHang()
        {
            cbMaHang.Text = null;
            txtTenHang.Clear();
            txtSoLuong.Clear();
            txtKhuyenMai.Clear();
            txtDonGia.Clear();
            txtThanhTien.Clear();
        }
        public void GetDataCB()
        {
            string str;
            Class.FillCombo("SELECT MaKhach, TenKhach FROM QLKHACH", cbMaKhach, "MaKhach", "MaKhach");
            cbMaKhach.SelectedIndex = -1;
            str = "SELECT DonGiaBan FROM QLHANGHOA WHERE MaHang =N'" + cbMaHang.SelectedValue + "'";
            txtDonGia.Text = Class.GetFieldValues(str);
            Class.FillCombo("SELECT MaNhanVien, TenNhanVien FROM QLNHANVIEN", cbMaNV, "MaNhanVien", "MaNhanVien");
            cbMaNV.SelectedIndex = -1;
            Class.FillCombo("SELECT MaHang, TenHang FROM QLHANGHOA", cbMaHang, "MaHang", "MaHang");
            cbMaHang.SelectedIndex = -1;
        }
        private void LoadHoaDon()
        {
            string sql;
            sql = "SELECT a.MaHang, b.TenHang, a.SoLuong, b.DonGiaBan, a.KhuyenMai,a.ThanhTien FROM CHITIETHOADON AS a, QLHANGHOA AS b WHERE a.SoHD = N'" + txtSoHD.Text + "' AND a.MaHang=b.MaHang";
            dgvHoaDonBan.DataSource = Class.LoadTbl(sql);
            //RenameColumn();
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT MaNhanVien FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'";
            cbMaNV.Text = Class.GetFieldValues(str);
            str = "SELECT MaKhach FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'";
            cbMaKhach.Text = Class.GetFieldValues(str);
            str = "SELECT TongTien FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'";
            txtTongTien.Text = Class.GetFieldValues(str);
            //lblBangChu.Text = "Bằng chữ: " + Class.ChuyenSoSangChu(txtTongTien.Text);
        }

        private void cbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TenHang FROM QLHANGHOA WHERE MaHang =N'" + cbMaHang.SelectedValue + "'";
            txtTenHang.Text = Class.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM QLHANGHOA WHERE MaHang =N'" + cbMaHang.SelectedValue + "'";
            txtDonGia.Text = Class.GetFieldValues(str);
        }

        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtKhuyenMai.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtKhuyenMai.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = (sl * dg) - ((sl * dg) * (gg / 100));
            txtThanhTien.Text = tt.ToString();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtKhuyenMai.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtKhuyenMai.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = (sl * dg) - ((sl * dg) * (gg / 100));
            txtThanhTien.Text = tt.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Show();
            btnHuy.Show();
            txtSoHD.Text = Class.CreateKeyHoaDon("HDB");
            LoadHoaDon();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        { 
            string sql;
            double sl, slcon;
            sql = "SELECT SoHD FROM HOADON WHERE SoHD=N'" + txtSoHD.Text + "'";
            if (!Class.CheckKey(sql))
            {
                //So hd chưa có, tiến hành lưu các thông tin chung
                // So hd được sinh tự động do đó không có trường hợp trùng khóa
                //KH0000000001
                if (cbMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaNV.Focus();
                    return;
                }
                if (!Class.CheckKey("SELECT SoHD FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'"))
                {
                    // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                    // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                    if (cbMaNV.Text.Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbMaNV.Focus();
                        return;
                    }
                    if(cbMaKhach.Text.Length == 0)
                    {
                        cbMaKhach.Text = "KH2021703";
                        txtTenKhach.Text = "Khách lẻ";
                    }
                }
                cmd = new SqlCommand("Insert into HOADON values(@SoHD, @MaKhach,  @MaNhanVien, @NgayTao,@TongTien)", Class.Con);
                cmd.Parameters.AddWithValue("SoHD", txtSoHD.Text);
                cmd.Parameters.AddWithValue("MaKhach", cbMaKhach.Text);
                cmd.Parameters.AddWithValue("MaNhanVien", cbMaNV.Text);
                cmd.Parameters.AddWithValue("NgayTao", dtpNgayTao.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("TongTien", txtTongTien.Text);
                cmd.ExecuteNonQuery();
                // Cập nhật lại tổng tiền cho hóa đơn bán
            }
            // Lưu thông tin của các mặt hàng
            if (cbMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "SELECT MaHang FROM CHITIETHOADON WHERE MaHang=N'" + cbMaHang.SelectedValue + "' AND SoHD = N'" + txtSoHD.Text.Trim() + "'";
            if (Class.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaHang.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Class.GetFieldValues("SELECT SoLuong FROM QLHANGHOA WHERE MaHang = N'" + cbMaHang.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            cmd = new SqlCommand("Insert into CHITIETHOADON values(@SoHD, @MaHang,  @DonGia, @SoLuong,@KhuyenMai, @ThanhTien)", Class.Con);
            cmd.Parameters.AddWithValue("SoHD", txtSoHD.Text);
            cmd.Parameters.AddWithValue("MaHang", cbMaHang.Text);
            cmd.Parameters.AddWithValue("DonGia", txtDonGia.Text);
            cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
            cmd.Parameters.AddWithValue("KhuyenMai", txtKhuyenMai.Text);
            cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien.Text);
            cmd.ExecuteNonQuery();
            dgvHoaDonBan.DataSource = Class.LoadTbl("select *from CHITIETHOADON");
            LoadHoaDon();
            // Cập nhật lại số lượng của mặt hàng vào bảng HANGHOA
            slcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE QLHANGHOA SET SoLuong =" + slcon + " WHERE MaHang= N'" + cbMaHang.SelectedValue + "'";
            Class.LoadTbl(sql);
            //
            // Cập nhật lại tổng tiền cho hóa đơn bán
            double tong, tongtien;
            tong = double.Parse(Class.GetFieldValues("SELECT TongTien FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'"));
            tongtien = tong + Convert.ToDouble(txtThanhTien.Text);
            txtTongTien.Text = tongtien.ToString();
            sql = "UPDATE HOADON SET TongTien =" + tongtien + " WHERE SoHD = N'" + txtSoHD.Text + "'";
            Class.LoadTbl(sql);
            //
          //  lbTong.Text = "Bằng chữ: " + Class.ChuyenSoSangChu(tongtien.ToString());
            /*  
        */
        }

        private void dgvHoaDonBan_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (dgvHoaDonBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa món hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvHoaDonBan.CurrentRow.Cells["MaHang"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvHoaDonBan.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvHoaDonBan.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE CHITIETHOADON WHERE SoHD=N'" + txtSoHD.Text + "' AND MaHang = N'" + MaHangxoa + "'";
                Class.LoadTbl(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(Class.GetFieldValues("SELECT SoLuong FROM QLHANGHOA WHERE MaHang = N'" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE QLHANGHOA SET SoLuong =" + slcon + " WHERE MaHang= N'" + MaHangxoa + "'";
                Class.LoadTbl(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Class.GetFieldValues("SELECT TongTien FROM HOADON WHERE SoHD = N'" + txtSoHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE HOADON SET TongTien =" + tongmoi + " WHERE SoHD = N'" + txtSoHD.Text + "'";
                Class.LoadTbl(sql);
                txtTongTien.Text = tongmoi.ToString();
            //    lbTong.Text = Class.ChuyenSoSangChu(txtTongTien.Text);
                LoadHoaDon();
            }
        }
        private void cbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaNV.Text == "")
                txtTenNhanVien.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNhanVien from QLNHANVIEN where MaNhanVien =N'" + cbMaNV.SelectedValue + "'";
            txtTenNhanVien.Text = Class.GetFieldValues(str);
        }
        private void cbMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaKhach.Text == "")
                txtTenKhach.Text = "";
            // Khi chọn Mã khach thì tên KH tự động hiện ra
            str = "Select TenKhach from QLKHACH where MaKhach =N'" + cbMaKhach.SelectedValue + "'";
            txtTenKhach.Text = Class.GetFieldValues(str);
        }




        private void btnIn_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:C3"].Font.Size = 13;
            exRange.Range["A1:C3"].Font.Bold = true;
            exRange.Range["A1:C3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:C1"].ColumnWidth = 7;
            exRange.Range["B1:C1"].ColumnWidth = 15;
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:C1"].Value = "Nhóm 4";
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Đại học CNTT và TT Thái Nguyên";
            exRange.Range["A3:C3"].MergeCells = true;
            exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:C3"].Value = "Điện thoại: (03)89852511";
            exRange.Range["D2:F2"].Font.Size = 20;
            exRange.Range["D2:F2"].Font.Bold = true;
            exRange.Range["D2:F2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D2:F2"].MergeCells = true;
            exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D2:F2"].Value = "HÓA ĐƠN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.SoHD, a.NgayTao , b.TenKhach, c.TenNhanVien, a.TongTien FROM HOADON AS a, QLKHACH AS b, QLNHANVIEN AS c WHERE a.SoHD = N'" + txtSoHD.Text + "' AND a.MaKhach = b.MaKhach AND a.MaNhanVien = c.MaNhanVien";
            tblThongtinHD = Class.LoadTbl(sql);
            exRange.Range["B6:C9"].Font.Size = 13;
            exRange.Range["B6:B6"].Value = "Số hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Ngày tạo: ";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][1].ToString();
            exRange.Range["B8:B8"].Value = "Khách hàng: ";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][2].ToString();
            exRange.Range["B9:B9"].Value = "Nhân viên :";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][3].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenHang, a.SoLuong, b.DonGiaBan, a.KhuyenMai, a.ThanhTien " +
                  "FROM CHITIETHOADON AS a , QLHANGHOA AS b WHERE a.SoHD = N'" +
                  txtSoHD.Text + "' AND a.MaHang = b.MaHang";
            tblThongtinHang = Class.LoadTbl(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][4].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            // exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Thái Nguyên, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year; ;
            exSheet.Name = "Hóa đơn bán hàng";
            exApp.Visible = true;
        }
        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.DisConnection();
        }
        private void btnMoDSHD_Click(object sender, EventArgs e)
        {
            frmDanhSachHoaDon f2 = new frmDanhSachHoaDon();
            f2.TopLevel = true;
            f2.Show();
        }
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void txtKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Thông báo", "Hóa đơn này sẽ bị hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetValuesHoaDon();
                ResetValuesHang();
                btnLuu.Hide();
                btnHuy.Hide();
                LoadHoaDon();
            }
        }
    }
}
