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
    public partial class frmDanhSachHoaDon : Form
    {
        public frmDanhSachHoaDon()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        private void frmDSHoaDon_Load(object sender, EventArgs e)
        {
            dgvDSHD.DataSource = Class.LoadTbl("select *from HOADON");
            RenameColumnDSHD();
            this.dgvDSHD.DefaultCellStyle.Font = new Font("Times new roman", 13);
        }
        private void RenameColumnDSHD()
        {
            dgvDSHD.Columns[0].HeaderText = "Số hóa đơn";
            dgvDSHD.Columns[1].HeaderText = "Mã nhân viên";
            dgvDSHD.Columns[2].HeaderText = "Mã khách hàng";
            dgvDSHD.Columns[3].HeaderText = "Ngày tạo";
            dgvDSHD.Columns[4].HeaderText = "Tổng tiền";
            dgvDSHD.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            txtSoHD.Text = dgvDSHD.CurrentRow.Cells[0].Value.ToString();
            txtMaKhach.Text = dgvDSHD.CurrentRow.Cells[1].Value.ToString();
            txtTenKhach.Text = Class.GetFieldValues("select TenKhach from QLKHACH where MaKhach = '" + txtMaKhach.Text + "'");
            txtMaNV.Text = dgvDSHD.CurrentRow.Cells[2].Value.ToString();
            txtTenNV.Text = Class.GetFieldValues("select TenNhanVien from QLNHANVIEN where MaNhanVien = '"+txtMaNV.Text+"'");
            txtNgayTao.Text = dgvDSHD.CurrentRow.Cells[3].Value.ToString();
            txtTongTien.Text = dgvDSHD.CurrentRow.Cells[4].Value.ToString();
            dgvXemCTHD.DataSource = Class.LoadTbl("SELECT a.MaHang, b.TenHang, a.SoLuong, b.DonGiaBan, a.KhuyenMai,a.ThanhTien FROM CHITIETHOADON AS a, QLHANGHOA AS b WHERE a.SoHD = N'" + txtSoHD.Text + "' AND a.MaHang=b.MaHang");
            Class.RenameColumnCTHD(dgvXemCTHD);
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Length == 0) MessageBox.Show("Nhập thông tin bạn muốn tìm kiếm !");
            else
            {
                cmd = new SqlCommand("select *from HOADON where SoHD = @SoHD or MaNhanVien = @MaNhanVien or MaKhach = @MaKhach ", Class.Con);
                cmd.Parameters.AddWithValue("SoHD", txtTimKiem.Text);
                cmd.Parameters.AddWithValue("MaNhanVien", txtTimKiem.Text);
                cmd.Parameters.AddWithValue("MaKhach", txtTimKiem.Text);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvDSHD.DataSource = dt;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {          
            if (MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmd = new SqlCommand("Delete from CHITIETHOADON where SoHD='" + txtSoHD.Text + "'", Class.Con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete from HOADON where SoHD = '" + txtSoHD.Text + "'", Class.Con);
                cmd.ExecuteNonQuery();
                frmDSHoaDon_Load(sender, e);
            }
            //  Class.LoadTbl("select *from CHITIETHOADON");
            ///
            //  cmd = new SqlCommand("delete from HOADON where SoHD = '" + txtSoHD.Text + "'", Class.Con);
            //cmd.ExecuteNonQuery();
            // Class.LoadTbl("select *from HOADON");

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtSoHD.Text.Length == 0)
            {
                MessageBox.Show("Chọn hóa đơn để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
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
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
