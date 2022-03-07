using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.View;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyBanHang
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Connection();
            string id = lbXinChao.Text;
            string lay_id = id.Substring(0, 2);
            string str = Class.GetFieldValues("Select MaNhanVien from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'");
            if (lay_id == "NV")
            {
                mnuQuanLyNhanVien.Enabled = false;
                mnuQuanLyNCC.Enabled = false;
                if(str == Class.GetFieldValues("Select MaNhanVien from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "' and NhiemVu = 'Nhân viên kho'"))
                {
                    MessageBox.Show("Bạn đã đăng nhập với quyền nhân viên kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mnuHoaDon.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn đã đăng nhập với quyền nhân viên bán hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mnuQuanLyKho.Enabled = false;
                }
            }
            else if (lay_id == "MN")
            {
                MessageBox.Show("Bạn đã đăng nhập với quyền người quản lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mnuHoaDon.Enabled = false;
                mnuQuanLyKho.Enabled = false;
            }
        }
        /*
        private bool CheckForm(string nameFrm)
        {
            bool check = false;
            foreach(Form f in this.MdiChildren)
            {
                if(nameFrm == f.Name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        private void ActiveFormChidlen(string nameFrm)
        {
            foreach(Form f in this.MdiChildren)
            {
                if(nameFrm == f.Name)
                {
                    f.Activate();
                    break;
                }
            }
        }
        */
        private void mnuQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            frmQuanLyNhanVien f = new frmQuanLyNhanVien();
            Dispay(f);
        }
        private void mnuQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            frmQuanLyKhachHang f = new frmQuanLyKhachHang();
            Dispay(f);
        }
        
        private void mnuQuanLyNCC_Click(object sender, EventArgs e)
        {
            frmQuanLyNCC f = new frmQuanLyNCC();
            Dispay(f);
        }

        private void mnuDanhSachHangHoa_Click(object sender, EventArgs e)
        {
            frmDanhSachHangHoa f = new frmDanhSachHangHoa();
            Dispay(f);
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Class.DisConnection();
                Application.Exit();// đóng form
            }
        }
        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            frmDangNhap f = new frmDangNhap();
            f.Show();
            this.Hide();
        }

 
        /*
         Số phiếu nhập, Mã nhà cung cấp, tên nhà cung cấp, địa chỉ NCC, số điện thoại, ngày nhập, mã hàng,
         tên hàng, đơn vị tính, đơn giá, số lượng, thành tiền, tên thủ kho nhận. Khi hàng được xuất khỏi kho thì Thủ kho sẽ tạo Phiếu xuất kho.
         Phiếu xuất kho có các thông tin sau: Số phiếu xuất, Mã nhân viên, tên nhân viên, ngày xuất,
         mã hàng, tên hàng, đơn vị tính, đơn giá, số lượng, nhân viên nhận hàng từ kho. 
         */

        //Hiển thị form trong form main
        private void Dispay(Form f)
        {
            this.pnMain.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            this.pnMain.Controls.Add(f);
            f.Show();
        }
        private void mnuBaoCao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này đang được nâng cấp");
        }

        private void mnuHangTon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này đang được nâng cấp");
        }
        private void mnuPhieuNhap_Click(object sender, EventArgs e)
        {
            QuanLyKho f = new QuanLyKho();
            f.lbLoaiPhieu.Text = "PHIẾU NHẬP";
            Dispay(f);

        }
        private void munPhieuXuat_Click(object sender, EventArgs e)
        {
            QuanLyKho f = new QuanLyKho();
            f.lbLoaiPhieu.Text = "PHIẾU XUẤT";
            Dispay(f);
        }

        private void mnuLapHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon f = new frmHoaDon();
            Dispay(f);
        }

        private void mnuDanhSachHoaDon_Click(object sender, EventArgs e)
        {
            frmDanhSachHoaDon f = new frmDanhSachHoaDon();
            Dispay(f);
        }

        private void mnuDanhSachPhieu_Click(object sender, EventArgs e)
        {
            frmDanhSachPhieu f = new frmDanhSachPhieu();
            f.cbLoai.Text = "TẤT CẢ";
            Dispay(f);
        }

        private void mnuThongTin_Click(object sender, EventArgs e)
        {
            frmThongTinTaiKhoan f = new frmThongTinTaiKhoan();
            f.lbMaNV.Text =  lbXinChao.Text;
            f.lbTenNV.Text = "Họ và tên : "+Class.GetFieldValues("select TenNhanVien from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'"); ;
            f.lbGioiTinh.Text = "Giới tính : "+Class.GetFieldValues("select GioiTinh from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'"); ;
            f.lbNgaySinh.Text = "Ngày sinh: "+Convert.ToDateTime(Class.GetFieldValues("select NgaySinh from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'")).ToString("dd-MM-yyyy"); 
            f.lbDiaChi.Text = "Địa chỉ : "+ Class.GetFile("select DiaChi from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'"); ;
            f.lbSDT.Text = "Số điện thoại : "+ Class.GetFieldValues("select SDT from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'"); ;
            f.lbChucVu.Text = "Chức vụ : " +  Class.GetFieldValues("select NhiemVu from QLNHANVIEN where MaNhanVien = '" + lbXinChao.Text + "'");
            Dispay(f);
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau f = new frmDoiMatKhau();
            Dispay(f);
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            frmGioiThieu f = new frmGioiThieu();
            Dispay(f);
        }
    }
}
