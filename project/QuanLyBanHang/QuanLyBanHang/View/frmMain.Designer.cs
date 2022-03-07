
namespace QuanLyBanHang
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyNCC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHangTon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLapHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuQuanLyKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhieuNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.munPhieuXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachPhieu = new System.Windows.Forms.ToolStripMenuItem();
            this.pnMain = new System.Windows.Forms.Panel();
            this.lbXinChao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.AutoSize = false;
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuanLyNhanVien,
            this.mnuQuanLyKhachHang,
            this.mnuQuanLyNCC});
            this.mnuHeThong.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(100, 30);
            this.mnuHeThong.Text = "Quản lý";
            // 
            // mnuQuanLyNhanVien
            // 
            this.mnuQuanLyNhanVien.Name = "mnuQuanLyNhanVien";
            this.mnuQuanLyNhanVien.Size = new System.Drawing.Size(235, 24);
            this.mnuQuanLyNhanVien.Text = "Quản lý nhân viên";
            this.mnuQuanLyNhanVien.Click += new System.EventHandler(this.mnuQuanLyNhanVien_Click);
            // 
            // mnuQuanLyKhachHang
            // 
            this.mnuQuanLyKhachHang.Name = "mnuQuanLyKhachHang";
            this.mnuQuanLyKhachHang.Size = new System.Drawing.Size(235, 24);
            this.mnuQuanLyKhachHang.Text = "Quản lý khách hàng";
            this.mnuQuanLyKhachHang.Click += new System.EventHandler(this.mnuQuanLyKhachHang_Click);
            // 
            // mnuQuanLyNCC
            // 
            this.mnuQuanLyNCC.Name = "mnuQuanLyNCC";
            this.mnuQuanLyNCC.Size = new System.Drawing.Size(235, 24);
            this.mnuQuanLyNCC.Text = "Quản lý NCC";
            this.mnuQuanLyNCC.Click += new System.EventHandler(this.mnuQuanLyNCC_Click);
            // 
            // mnuHangHoa
            // 
            this.mnuHangHoa.AutoSize = false;
            this.mnuHangHoa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhSachHangHoa,
            this.mnuHangTon});
            this.mnuHangHoa.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuHangHoa.Name = "mnuHangHoa";
            this.mnuHangHoa.Size = new System.Drawing.Size(100, 30);
            this.mnuHangHoa.Text = "Hàng hóa";
            // 
            // mnuDanhSachHangHoa
            // 
            this.mnuDanhSachHangHoa.Name = "mnuDanhSachHangHoa";
            this.mnuDanhSachHangHoa.Size = new System.Drawing.Size(235, 24);
            this.mnuDanhSachHangHoa.Text = "Danh sách hàng hóa";
            this.mnuDanhSachHangHoa.Click += new System.EventHandler(this.mnuDanhSachHangHoa_Click);
            // 
            // mnuHangTon
            // 
            this.mnuHangTon.Name = "mnuHangTon";
            this.mnuHangTon.Size = new System.Drawing.Size(235, 24);
            this.mnuHangTon.Text = "Hàng tồn kho";
            this.mnuHangTon.Click += new System.EventHandler(this.mnuHangTon_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.AutoSize = false;
            this.mnuBaoCao.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(100, 30);
            this.mnuBaoCao.Text = "Báo cáo";
            this.mnuBaoCao.Click += new System.EventHandler(this.mnuBaoCao_Click);
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.AutoSize = false;
            this.mnuHoaDon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLapHoaDon,
            this.mnuDanhSachHoaDon});
            this.mnuHoaDon.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(122, 30);
            this.mnuHoaDon.Text = "Hóa đơn";
            // 
            // mnuLapHoaDon
            // 
            this.mnuLapHoaDon.Name = "mnuLapHoaDon";
            this.mnuLapHoaDon.Size = new System.Drawing.Size(227, 24);
            this.mnuLapHoaDon.Text = "Lập hóa đơn";
            this.mnuLapHoaDon.Click += new System.EventHandler(this.mnuLapHoaDon_Click);
            // 
            // mnuDanhSachHoaDon
            // 
            this.mnuDanhSachHoaDon.Name = "mnuDanhSachHoaDon";
            this.mnuDanhSachHoaDon.Size = new System.Drawing.Size(227, 24);
            this.mnuDanhSachHoaDon.Text = "Danh sách hóa đơn";
            this.mnuDanhSachHoaDon.Click += new System.EventHandler(this.mnuDanhSachHoaDon_Click);
            // 
            // mnuTaiKhoan
            // 
            this.mnuTaiKhoan.AutoSize = false;
            this.mnuTaiKhoan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuThongTin,
            this.mnuDangXuat});
            this.mnuTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(122, 30);
            this.mnuTaiKhoan.Text = "Tài khoản";
            // 
            // mnuThongTin
            // 
            this.mnuThongTin.Name = "mnuThongTin";
            this.mnuThongTin.Size = new System.Drawing.Size(158, 24);
            this.mnuThongTin.Text = "Thông tin";
            this.mnuThongTin.Click += new System.EventHandler(this.mnuThongTin_Click);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(158, 24);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.AutoSize = false;
            this.mnuTroGiup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trợGiúpToolStripMenuItem2,
            this.mnuGioiThieu});
            this.mnuTroGiup.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(100, 30);
            this.mnuTroGiup.Text = "Trợ giúp";
            // 
            // trợGiúpToolStripMenuItem2
            // 
            this.trợGiúpToolStripMenuItem2.Name = "trợGiúpToolStripMenuItem2";
            this.trợGiúpToolStripMenuItem2.Size = new System.Drawing.Size(156, 24);
            this.trợGiúpToolStripMenuItem2.Text = "Trợ giúp";
            // 
            // mnuGioiThieu
            // 
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(156, 24);
            this.mnuGioiThieu.Text = "Giới thiệu";
            this.mnuGioiThieu.Click += new System.EventHandler(this.mnuGioiThieu_Click);
            // 
            // mnuThoat
            // 
            this.mnuThoat.AutoSize = false;
            this.mnuThoat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mnuThoat.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.mnuThoat.ForeColor = System.Drawing.Color.Red;
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(100, 30);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTaiKhoan,
            this.mnuHeThong,
            this.mnuHangHoa,
            this.mnuBaoCao,
            this.mnuQuanLyKho,
            this.mnuHoaDon,
            this.mnuTroGiup,
            this.mnuThoat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 3, 0, 3);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1204, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuQuanLyKho
            // 
            this.mnuQuanLyKho.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPhieuNhap,
            this.munPhieuXuat,
            this.mnuDanhSachPhieu});
            this.mnuQuanLyKho.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.mnuQuanLyKho.Name = "mnuQuanLyKho";
            this.mnuQuanLyKho.Size = new System.Drawing.Size(118, 36);
            this.mnuQuanLyKho.Text = "Quản lý kho";
            // 
            // mnuPhieuNhap
            // 
            this.mnuPhieuNhap.Name = "mnuPhieuNhap";
            this.mnuPhieuNhap.Size = new System.Drawing.Size(207, 24);
            this.mnuPhieuNhap.Text = "Phiếu nhập";
            this.mnuPhieuNhap.Click += new System.EventHandler(this.mnuPhieuNhap_Click);
            // 
            // munPhieuXuat
            // 
            this.munPhieuXuat.Name = "munPhieuXuat";
            this.munPhieuXuat.Size = new System.Drawing.Size(207, 24);
            this.munPhieuXuat.Text = "Phiếu xuất";
            this.munPhieuXuat.Click += new System.EventHandler(this.munPhieuXuat_Click);
            // 
            // mnuDanhSachPhieu
            // 
            this.mnuDanhSachPhieu.Name = "mnuDanhSachPhieu";
            this.mnuDanhSachPhieu.Size = new System.Drawing.Size(207, 24);
            this.mnuDanhSachPhieu.Text = "Danh sách phiếu";
            this.mnuDanhSachPhieu.Click += new System.EventHandler(this.mnuDanhSachPhieu_Click);
            // 
            // pnMain
            // 
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 42);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1204, 590);
            this.pnMain.TabIndex = 2;
            // 
            // lbXinChao
            // 
            this.lbXinChao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXinChao.AutoSize = true;
            this.lbXinChao.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbXinChao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbXinChao.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbXinChao.Location = new System.Drawing.Point(1036, 12);
            this.lbXinChao.Name = "lbXinChao";
            this.lbXinChao.Size = new System.Drawing.Size(55, 19);
            this.lbXinChao.TabIndex = 4;
            this.lbXinChao.Text = "label1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(961, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Xin chào ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1204, 632);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbXinChao);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Quản Lý Bán Hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyNCC;
        private System.Windows.Forms.ToolStripMenuItem mnuHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTin;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyKho;
        private System.Windows.Forms.ToolStripMenuItem mnuHangTon;
        private System.Windows.Forms.ToolStripMenuItem mnuPhieuNhap;
        private System.Windows.Forms.ToolStripMenuItem munPhieuXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachPhieu;
        private System.Windows.Forms.ToolStripMenuItem mnuLapHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHoaDon;
        public System.Windows.Forms.Label lbXinChao;
        public System.Windows.Forms.Label label1;
    }
}

