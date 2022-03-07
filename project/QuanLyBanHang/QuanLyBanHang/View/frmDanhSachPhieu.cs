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
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanHang.View
{
    public partial class frmDanhSachPhieu : Form
    {
        public frmDanhSachPhieu()
        {
            InitializeComponent();
        }

        private void frmDSPhieu_Load(object sender, EventArgs e)
        {
            dgvDanhSachPhieu.DataSource = Class.LoadTbl("select *from QLNHAPXUAT");
            dgvDanhSachPhieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //select a.MaPhieu, a.LoaiPhieu, a.MaNCC, b.TenNCC, a.MaNhanVien, c.TenNhanVien, a.NgayNhap, a.TongTien from QLNHAPXUAT as a, QLNHACUNGCAP as b, QLNHANVIEN as c where a.MaNCC = b.MaNCC and b.MaNhanVien = c.MaNhanVien")
            //
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoai.Text == "TẤT CẢ")
            {
                dgvDanhSachPhieu.DataSource = Class.LoadTbl("select *from QLNHAPXUAT");
                dgvDanhSachPhieu.Columns[0].HeaderText = "Mã phiếu";
                dgvDanhSachPhieu.Columns[1].HeaderText = "Loại phiếu";
                dgvDanhSachPhieu.Columns[2].HeaderText = "Mã NCC";
                dgvDanhSachPhieu.Columns[3].HeaderText = "Mã nhân viên";
                dgvDanhSachPhieu.Columns[4].HeaderText = "Ngày nhập";
                dgvDanhSachPhieu.Columns[5].HeaderText = "Ngày xuất";
                dgvDanhSachPhieu.Columns[6].HeaderText = "Tổng tiền";
                dgvHangHoa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if (cbLoai.Text == "PHIẾU NHẬP")
            {
                string a = "PHIẾU NHẬP";
                dgvDanhSachPhieu.DataSource = Class.LoadTbl("select MaPhieu, LoaiPhieu, MaNCC,  MaNhanVien,  NgayNhap, TongTien from QLNHAPXUAT where LoaiPhieu = N'"+a+"'");
                dgvDanhSachPhieu.Columns[0].HeaderText = "Mã phiếu";
                dgvDanhSachPhieu.Columns[1].HeaderText = "Loại phiếu";
                dgvDanhSachPhieu.Columns[2].HeaderText = "Mã NCC";
                dgvDanhSachPhieu.Columns[3].HeaderText = "Mã nhân viên";
                dgvDanhSachPhieu.Columns[4].HeaderText = "Ngày Nhập";
                dgvDanhSachPhieu.Columns[5].HeaderText = "Tổng tiền";
                dgvHangHoa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if (cbLoai.Text == "PHIẾU XUẤT")
            {
                string b = "PHIẾU XUẤT";
                dgvDanhSachPhieu.DataSource = Class.LoadTbl("select MaPhieu, LoaiPhieu,  MaNhanVien,  NgayXuat, TongTien from QLNHAPXUAT where LoaiPhieu = N'" + b + "'");
                dgvDanhSachPhieu.Columns[0].HeaderText = "Mã phiếu";
                dgvDanhSachPhieu.Columns[1].HeaderText = "Loại phiếu";
                dgvDanhSachPhieu.Columns[2].HeaderText = "Mã nhân viên";
                dgvDanhSachPhieu.Columns[3].HeaderText = "Ngày xuất";
                dgvDanhSachPhieu.Columns[4].HeaderText = "Tổng tiền";
                dgvHangHoa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void RenameColumnHangHoa()
        {
            dgvHangHoa.Columns[0].HeaderText = "Mã hàng";
            dgvHangHoa.Columns[1].HeaderText = "Tên hàng";
            dgvHangHoa.Columns[2].HeaderText = "Đơn giá mua";
            dgvHangHoa.Columns[3].HeaderText = "Số lượng";
            dgvHangHoa.Columns[4].HeaderText = "Thành tiền";
            dgvHangHoa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Text = dgvDanhSachPhieu.CurrentRow.Cells[0].Value.ToString();
            txtLoaiPhieu.Text = dgvDanhSachPhieu.CurrentRow.Cells[1].Value.ToString();
            txtMaNCC.Text = dgvDanhSachPhieu.CurrentRow.Cells[2].Value.ToString();
            txtTenNCC.Text = Class.GetFieldValues("Select TenNCC from QLNHACUNGCAP where MaNCC = '" + txtMaNCC.Text + "'");
            txtMaNV.Text = dgvDanhSachPhieu.CurrentRow.Cells[3].Value.ToString();
            txtTenNV.Text = Class.GetFieldValues("Select TenNhanVien from QLNHANVIEN where MaNhanVien = '" + txtMaNV.Text + "'");
            if(txtLoaiPhieu.Text == "PHIẾU XUẤT")
            {
                txtNgayNhap.Text = "";
                txtNgayXuat.Text = dgvDanhSachPhieu.CurrentRow.Cells[4].Value.ToString();
            }
            else
            {
                txtNgayNhap.Text = dgvDanhSachPhieu.CurrentRow.Cells[4].Value.ToString();
                txtNgayXuat.Text = "";
            }
            dgvHangHoa.DataSource = Class.LoadTbl("SELECT a.MaHang, b.TenHang, b.DonGiaMua, a.SoLuong, a.ThanhTien FROM CHITIETPHIEU AS a, QLHANGHOA AS b WHERE a.MaPhieu = N'" + txtMaPhieu.Text + "' AND a.MaHang = b.MaHang");
            RenameColumnHangHoa();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("Delete from CHITIETPHIEU where MaPhieu ='" + txtMaPhieu.Text + "'", Class.Con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete from QLNHAPXUAT where MaPhieu = '" + txtMaPhieu.Text + "'", Class.Con);
                cmd.ExecuteNonQuery();
                frmDSPhieu_Load(sender, e);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtLoaiPhieu.Text == "PHIẾU NHẬP")
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
                exSheet.Name = "Phiếu nhập hàng";
                exApp.Visible = true;
            }
            else if (txtLoaiPhieu.Text == "PHIẾU XUẤT")
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
                exRange.Range["C8:E8"].Value = tblThongtinPhieu.Rows[0][2].ToString();
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
                exRange.Value2 = tblThongtinPhieu.Rows[0][3].ToString();
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
