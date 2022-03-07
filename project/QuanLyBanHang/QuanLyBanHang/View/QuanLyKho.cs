using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanHang.View
{
    public partial class QuanLyKho : Form
    {
        public QuanLyKho()
        {
            InitializeComponent();
        }
        SqlCommand cmd = new SqlCommand();
        private void frmQuanLyNhapXuat_Load(object sender, EventArgs e)
        {
            txtLoaiPhieu.Text = lbLoaiPhieu.Text;
            if (lbLoaiPhieu.Text == "PHIẾU XUẤT")
            {
                // cbMaNCC.Enabled = false;
                cbMaNCC.Enabled = false;
                dtpNgayXuat.Enabled = false;
                txtTenNCC.Enabled = false;
            }
            else if (lbLoaiPhieu.Text == "PHIẾU NHẬP")
                dtpNgayXuat.Enabled = false;
            Class.Connection();
            btnThem.Enabled = true;
            btnLuu.Hide();
            btnHuy.Hide();
            GetDataCB();
            LoadInfoPhieu();
            LoadPhieu();
           // RenameColumn();
            //  Class.RenameColumnCTHD(dgvHangHoa);
        }
        private void ResetValuesPhieu()
        {
            cbMaNCC.Text = null;
            cbMaNV.Text = null;
            txtTenNCC.Clear();
            txtTenNV.Clear();
        }
        private void ResetValuesHang()
        {
            cbMaHang.Text = null;
            txtTenHang.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtThanhTien.Clear();
            txtTongTien.Clear();
        }
        public void GetDataCB()
        {
            string str;
            Class.FillCombo("SELECT MaNCC, TenNCC FROM QLNHACUNGCAP", cbMaNCC, "MaNCC", "MaNCC");
            cbMaNCC.SelectedIndex = -1;
            str = "SELECT DonGiaMua FROM QLHANGHOA WHERE MaHang =N'" + cbMaHang.SelectedValue + "'";
            txtDonGia.Text = Class.GetFieldValues(str);
            Class.FillCombo("SELECT MaNhanVien, TenNhanVien FROM QLNHANVIEN", cbMaNV, "MaNhanVien", "MaNhanVien");
            cbMaNV.SelectedIndex = -1;
            Class.FillCombo("SELECT MaHang, TenHang FROM QLHANGHOA", cbMaHang, "MaHang", "MaHang");
            cbMaHang.SelectedIndex = -1;
        }
        private void RenameColumn()
        {
            dgvHangHoa.Columns[0].HeaderText = "Mã hàng";
            dgvHangHoa.Columns[1].HeaderText = "Tên hàng";
            dgvHangHoa.Columns[2].HeaderText = "Đơn giá nhập";
            dgvHangHoa.Columns[3].HeaderText = "Số lượng";
            dgvHangHoa.Columns[4].HeaderText = "Thành tiền";
            dgvHangHoa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void LoadPhieu()
        {
            string sql;
            sql = "SELECT a.MaHang, b.TenHang,b.DonGiaMua, a.SoLuong, a.ThanhTien FROM CHITIETPHIEU AS a, QLHANGHOA AS b WHERE a.MaPhieu = N'" + txtMaPhieu.Text + "' AND a.MaHang=b.MaHang";
            dgvHangHoa.DataSource = Class.LoadTbl(sql);
            RenameColumn();
        }

        private void LoadInfoPhieu()
        {
            string str;
            str = "SELECT MaNhanVien FROM QLNHAPXUAT WHERE MaPhieu = N'" + txtMaPhieu.Text + "'";
            cbMaNV.Text = Class.GetFieldValues(str);
            str = "SELECT MaNCC FROM QLNHAPXUAT WHERE MaPhieu = N'" + txtMaPhieu.Text + "'";
            cbMaNCC.Text = Class.GetFieldValues(str);
            str = "SELECT TongTien FROM QLNHAPXUAT WHERE MaPhieu = N'" + txtMaPhieu.Text + "'";
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
            str = "SELECT DonGiaMua FROM QLHANGHOA WHERE MaHang =N'" + cbMaHang.SelectedValue + "'";
            txtDonGia.Text = Class.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();    
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValuesHang();
            ResetValuesPhieu();
            if (lbLoaiPhieu.Text == "PHIẾU NHẬP")
            {
                txtMaPhieu.Text = Class.CreateKeyHoaDon("NHAP");
            }
            else if (lbLoaiPhieu.Text == "PHIẾU XUẤT")
            {
                txtMaPhieu.Text = Class.CreateKeyHoaDon("XUAT");
            }
            btnLuu.Show();
            btnHuy.Show();
            LoadPhieu();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            int slkho, slmoi, slke, sl;
            sql = "SELECT MaPhieu FROM QLNHAPXUAT WHERE MaPhieu=N'" + txtMaPhieu.Text + "'";
            if (!Class.CheckKey(sql))
            {
                if (cbMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaNV.Focus();
                    return;
                }
                if (!Class.CheckKey("SELECT MaPhieu FROM QLNHAPXUAT WHERE MaPhieu = N'" + txtMaPhieu.Text + "'"))
                {
                    // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                    // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                    if (cbMaNV.Text.Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbMaNV.Focus();
                        return;
                    }
                }
                if(lbLoaiPhieu.Text == "PHIẾU XUẤT")
                {
                    cbMaNCC.Text = "0";
                }
                cmd = new SqlCommand("Insert into QLNHAPXUAT values(@MaPhieu, @LoaiPhieu,@MaNCC,  @MaNhanVien, @NgayNhap, @NgayXuat,@TongTien)", Class.Con);
                cmd.Parameters.AddWithValue("MaPhieu", txtMaPhieu.Text);
                cmd.Parameters.AddWithValue("LoaiPhieu", lbLoaiPhieu.Text);
                if (lbLoaiPhieu.Text == "PHIẾU XUẤT")
                {
                    cbMaNCC.Text = "0";
                }
                cmd.Parameters.AddWithValue("MaNCC", cbMaNCC.Text);
                cmd.Parameters.AddWithValue("MaNhanVien", cbMaNV.Text);
                cmd.Parameters.AddWithValue("NgayNhap", dtpNgayNhap.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("NgayXuat", dtpNgayXuat.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("TongTien", txtTongTien.Text);
                cmd.ExecuteNonQuery();
                // Cập nhật lại tổng tiền cho phieu
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
            sql = "SELECT MaHang FROM CHITIETPHIEU WHERE MaHang=N'" + cbMaHang.SelectedValue + "' AND MaPhieu = N'" + txtMaPhieu.Text.Trim() + "'";
            if (Class.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaHang.Focus();
                return;
            }
            cmd = new SqlCommand("Insert into CHITIETPHIEU values(@MaPhieu, @MaHang, @DonGia,@SoLuong, @ThanhTien)", Class.Con);
            cmd.Parameters.AddWithValue("MaPhieu", txtMaPhieu.Text);
            cmd.Parameters.AddWithValue("MaHang", cbMaHang.Text);
            cmd.Parameters.AddWithValue("DonGia", txtDonGia.Text);
            cmd.Parameters.AddWithValue("SoLuong", txtSoLuong.Text);
            cmd.Parameters.AddWithValue("ThanhTien", txtThanhTien.Text);
            cmd.ExecuteNonQuery();
            dgvHangHoa.DataSource = Class.LoadTbl("select *from CHITIETPHIEU");
            LoadPhieu();
            // Cập nhật lại số lượng của mặt hàng vào bảng HANGHOA
            slkho = Convert.ToInt32(Class.GetFieldValues("SELECT SoLuongKho FROM QLHANGHOA WHERE MaHang = N'" + cbMaHang.SelectedValue + "'"));
            slke = Convert.ToInt32(Class.GetFieldValues("SELECT SoLuong FROM QLHANGHOA WHERE MaHang = N'" + cbMaHang.SelectedValue + "'"));
            if (lbLoaiPhieu.Text == "PHIẾU NHẬP")
            {
                //Nhap thi them hang vao
                slmoi = slkho + Convert.ToInt32(txtSoLuong.Text);
                sql = "UPDATE QLHANGHOA SET SoLuongKho =" + slmoi + " WHERE MaHang= N'" + cbMaHang.Text + "'";
                Class.LoadTbl(sql);
            }
            else if(lbLoaiPhieu.Text == "PHIẾU XUẤT")
            {
                //Xuat thi tru hang o kho
                if (Convert.ToDouble(txtSoLuong.Text) > slkho)
                {
                    MessageBox.Show("Số lượng mặt hàng này chỉ còn " + slkho, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                else
                {
                    slmoi = slkho - Convert.ToInt32(txtSoLuong.Text);
                    sql = "UPDATE QLHANGHOA SET SoLuongKho =" + slmoi + " WHERE MaHang= N'" + cbMaHang.Text + "'";
                    Class.LoadTbl(sql);
                    sl = slke + Convert.ToInt32(txtSoLuong.Text);
                    sql = "UPDATE QLHANGHOA SET SoLuong =" + sl + " WHERE MaHang= N'" + cbMaHang.Text + "'";
                    Class.LoadTbl(sql);
                }
            }
            //
            //Cập nhật lại tổng tiền cho hóa đơn bán
            double tong, tongtien;
            tong = double.Parse(Class.GetFieldValues("SELECT TongTien FROM QLNHAPXUAT WHERE MaPhieu = N'" + txtMaPhieu.Text + "'"));
            tongtien = tong + Convert.ToDouble(txtThanhTien.Text);
            txtTongTien.Text = tongtien.ToString();
            sql = "UPDATE QLNHAPXUAT SET TongTien =" + tongtien + " WHERE MaPhieu = N'" + txtMaPhieu.Text + "'";
            Class.LoadTbl(sql);
            //  
        }
        private void cbMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaNCC.Text == "")
                txtTenNCC.Text = "";
            // Khi chọn Mã nCC thì tên nCC tự động hiện ra
            str = "Select TenNCC from QLNHACUNGCAP where MaNCC =N'" + cbMaNCC.Text + "'";
            txtTenNCC.Text = Class.GetFieldValues(str);
        }
        private void cbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNhanVien from QLNHANVIEN where MaNhanVien =N'" + cbMaNV.Text + "'";
            txtTenNV.Text = Class.GetFieldValues(str);
        }
        private void frmQuanLyNhapXuat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.DisConnection();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("v","Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                btnLuu.Hide();
                btnHuy.Hide();
                ResetValuesHang();
                ResetValuesPhieu();
                LoadPhieu();
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (lbLoaiPhieu.Text == "PHIẾU NHẬP")
            {
                // Khởi động chương trình Excel
                COMExcel.Application exApp = new COMExcel.Application();
                COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                COMExcel.Range exRange;
                string sql;
                int hang = 0, cot = 0;
                DataTable tblThongtinPhieu, tblThongtinHang;
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
                exRange.Range["A2:C2"].Value = "Điện thoại: (03)89852511";
                exRange.Range["A3:C3"].MergeCells = true;
                exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A3:C3"].Value = "Đại học CNTT và TT Thái Nguyên";
                exRange.Range["D2:F2"].Font.Size = 20;
                exRange.Range["D2:F2"].Font.Bold = true;
                exRange.Range["D2:F2"].Font.ColorIndex = 3; //Màu đỏ
                exRange.Range["D2:F2"].MergeCells = true;
                exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["D2:F2"].Value = "PHIẾU NHẬP";
                // Biểu diễn thông tin chung của hóa đơn bán
                sql = "SELECT a.MaPhieu, a.NgayNhap , b.TenNCC, c.TenNhanVien, a.TongTien FROM QLNHAPXUAT AS a, QLNHACUNGCAP AS b, QLNHANVIEN AS c WHERE a.MaPhieu = N'" + txtMaPhieu.Text + "' AND a.MaNCC = b.MaNCC AND a.MaNhanVien = c.MaNhanVien";
                tblThongtinPhieu = Class.LoadTbl(sql);
                exRange.Range["B6:C9"].Font.Size = 13;
                exRange.Range["B6:B6"].Value = "Mã phiếu :";
                exRange.Range["C6:E6"].MergeCells = true;
                exRange.Range["C6:E6"].Value = tblThongtinPhieu.Rows[0][0].ToString();
                exRange.Range["B7:B7"].Value = "Ngày nhập : ";
                exRange.Range["C7:E7"].MergeCells = true;
                exRange.Range["C7:E7"].Value = tblThongtinPhieu.Rows[0][1].ToString();
                exRange.Range["B8:B8"].Value = "Nhà cung cấp : ";
                exRange.Range["C8:E8"].MergeCells = true;
                exRange.Range["C8:E8"].Value = tblThongtinPhieu.Rows[0][2].ToString();
                exRange.Range["B9:B9"].Value = "Nhân viên :";
                exRange.Range["C9:E9"].MergeCells = true;
                exRange.Range["C9:E9"].Value = tblThongtinPhieu.Rows[0][3].ToString();
                //Lấy thông tin các mặt hàng
                sql = "SELECT b.TenHang, a.SoLuong, b.DonGiaMua, a.ThanhTien " +
                      "FROM CHITIETPHIEU AS a , QLHANGHOA AS b WHERE a.MaPhieu = N'" +
                      txtMaPhieu.Text + "' AND a.MaHang = b.MaHang";
                tblThongtinHang = Class.LoadTbl(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A11:F11"].Font.Bold = true;
                exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Tên hàng";
                exRange.Range["C11:C11"].Value = "Số lượng";
                exRange.Range["D11:D11"].Value = "Đơn giá";
                exRange.Range["E11:E11"].Value = "Thành tiền";
                for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() ;
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = "Tổng tiền:";
                exRange = exSheet.Cells[cot + 1][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = tblThongtinPhieu.Rows[0][4].ToString();
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
                DateTime d = Convert.ToDateTime(tblThongtinPhieu.Rows[0][1]);
                exRange.Range["A1:C1"].Value = "Thái Nguyên, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year; ;
                exSheet.Name = "Phiếu nhập hàng";
                exApp.Visible = true;
            }
            else if (lbLoaiPhieu.Text == "PHIẾU XUẤT")
            {
                // Khởi động chương trình Excel
                COMExcel.Application exApp = new COMExcel.Application();
                COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                COMExcel.Range exRange;
                string sql;
                int hang = 0, cot = 0;
                DataTable tblThongtinPhieu, tblThongtinHang;
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
                exRange.Range["A2:C2"].Value = "Điện thoại: (03)89852511";
                exRange.Range["A3:C3"].MergeCells = true;
                exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["A3:C3"].Value = "Đại học CNTT và TT Thái Nguyên";
                exRange.Range["D2:F2"].Font.Size = 20;
                exRange.Range["D2:F2"].Font.Bold = true;
                exRange.Range["D2:F2"].Font.ColorIndex = 3; //Màu đỏ
                exRange.Range["D2:F2"].MergeCells = true;
                exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["D2:F2"].Value = "PHIẾU XUẤT";
                // Biểu diễn thông tin chung của hóa đơn bán
                sql = "SELECT a.MaPhieu, a.NgayXuat, c.TenNhanVien, a.TongTien FROM QLNHAPXUAT AS a, QLNHACUNGCAP AS b, QLNHANVIEN AS c WHERE a.MaPhieu = N'" + txtMaPhieu.Text + "' AND  a.MaNhanVien = c.MaNhanVien";
                tblThongtinPhieu = Class.LoadTbl(sql);
                exRange.Range["B6:C9"].Font.Size = 13;
                exRange.Range["B6:B6"].Value = "Mã phiếu :";
                exRange.Range["C6:E6"].MergeCells = true;
                exRange.Range["C6:E6"].Value = tblThongtinPhieu.Rows[0][0].ToString();
                exRange.Range["B7:B7"].Value = "Ngày xuất : ";
                exRange.Range["C7:E7"].MergeCells = true;
                exRange.Range["C7:E7"].Value = tblThongtinPhieu.Rows[0][1].ToString();
                exRange.Range["B8:B8"].Value = "Nhân viên :";
                exRange.Range["C8:E8"].MergeCells = true;
                exRange.Range["C8:E8"].Value = tblThongtinPhieu.Rows[0][3].ToString();
                //Lấy thông tin các mặt hàng
                sql = "SELECT b.TenHang, a.SoLuong, b.DonGiaMua, a.ThanhTien " +
                      "FROM CHITIETPHIEU AS a , QLHANGHOA AS b WHERE a.MaPhieu = N'" +
                      txtMaPhieu.Text + "' AND a.MaHang = b.MaHang";
                tblThongtinHang = Class.LoadTbl(sql);
                //Tạo dòng tiêu đề bảng
                exRange.Range["A11:F11"].Font.Bold = true;
                exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                exRange.Range["C11:F11"].ColumnWidth = 12;
                exRange.Range["A11:A11"].Value = "STT";
                exRange.Range["B11:B11"].Value = "Tên hàng";
                exRange.Range["C11:C11"].Value = "Số lượng";
                exRange.Range["D11:D11"].Value = "Đơn giá";
                exRange.Range["E11:E11"].Value = "Thành tiền";
                for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
                {
                    //Điền số thứ tự vào cột 1 từ dòng 12
                    exSheet.Cells[1][hang + 12] = hang + 1;
                    for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    {
                        exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                        if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    }
                }
                exRange = exSheet.Cells[cot][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = "Tổng tiền:";
                exRange = exSheet.Cells[cot + 1][hang + 14];
                exRange.Font.Bold = true;
                exRange.Value2 = tblThongtinPhieu.Rows[0][4].ToString();
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
                DateTime d = Convert.ToDateTime(tblThongtinPhieu.Rows[0][1]);
                exRange.Range["A1:C1"].Value = "Thái Nguyên, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year; ;
                exSheet.Name = "Phiếu xuat hàng";
                exApp.Visible = true;
            }
        }

        private void btnXemDS_Click(object sender, EventArgs e)
        {
            frmDanhSachPhieu f = new frmDanhSachPhieu();
            f.ShowDialog();
        }
    }
}
