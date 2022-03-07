CREATE DATABASE QuanLyBanHang
go
use QuanLyBanHang
go
CREATE TABLE QLNHANVIEN(
MaNhanVien char(30) primary key,
TenNhanVien nvarchar(30) ,
GioiTinh nvarchar(5) ,
NgaySinh date ,
DiaChi nvarchar(50) ,
SDT nchar(10) ,
NhiemVu nvarchar(30) ,
GhiChu nvarchar(100) ,
)
CREATE TABLE QLKHACH(
MaKhach char(30) primary key,
TenKhach nvarchar(30) ,
GioiTinh nvarchar(5) ,
DiaChi nvarchar(50) ,
NhomKhach nvarchar(20) ,
SDT nvarchar(10) ,
SoDiem int ,
)
CREATE TABLE QLHANGHOA(
MaHang char(30) primary key,
TenHang nvarchar(50),
SoLuong int,
DVT nvarchar(15) ,
NhomHang nvarchar(50) ,
HinhAnh nvarchar(20) ,
DonGiaBan float ,
DonGiaMua float ,
NgayTao date ,
SoLuongKho int ,
)
CREATE TABLE QLNHACUNGCAP(
MaNCC char(30) primary key,
TenNCC nvarchar(30) ,
DiaChi nvarchar(50) ,
SDT nvarchar(10) ,
Email nvarchar(20) ,
GhiChu nvarchar(100) ,
)

CREATE TABLE HOADON(
SoHD char(30) ,
MaKhach char(30) ,
MaNhanVien char(30) ,
NgayTao date ,
TongTien float,
CONSTRAINT PRKEY3 PRIMARY KEY(SoHD, MaKhach, MaNhanVien),
CONSTRAINT MaKhach_FK FOREIGN KEY(MaKhach) REFERENCES QLKHACH(MaKhach),
CONSTRAINT MaNhanVien_FK FOREIGN KEY(MaNhanVien) REFERENCES QLNhanVien(MaNhanVien)
)
CREATE TABLE CHITIETHOADON(
SoHD nvarchar(30) ,
MaHang char(30) ,
DonGia float ,
SoLuong int ,
KhuyenMai nvarchar(20) ,
ThanhTien float ,
CONSTRAINT PRKEY4  PRIMARY KEY(SoHD, MaHang) ,
CONSTRAINT MaHang_FK4 FOREIGN KEY(MaHang) REFERENCES QLHANGHOA(MaHang) 
)

CREATE TABLE TAIKHOAN(
MaNhanVien char(30),
Pass varchar(15) ,
CONSTRAINT PRKEY7 PRIMARY KEY(MaNhanVien) ,
CONSTRAINT MaNhanVien_FK1 FOREIGN KEY(MaNhanVien) REFERENCES QLNHANVIEN(MaNhanVien) 
)
--Số phiếu nhập, Mã nhà cung cấp, tên nhà cung cấp, địa chỉ NCC, số điện thoại, ngày nhập, mã hàng, tên hàng, đơn vị tính, đơn giá, số lượng, thành tiền, tên thủ kho nhận
--Phiếu xuất kho có các thông tin sau: Số phiếu xuất, Mã nhân viên, tên nhân viên, ngày xuất, mã hàng, tên hàng, đơn vị tính, đơn giá, số lượng, nhân viên nhận hàng từ kho. 
CREATE TABLE QLNHAPXUAT(
MaPhieu char(30),
LoaiPhieu nvarchar(20),
MaNCC char(30) , 
MaNhanVien char(30) ,
NgayNhap date,
NgayXuat date ,
TongTien float ,
CONSTRAINT PRKEY5 PRIMARY KEY(MaPhieu, MaNhanVien),
CONSTRAINT MaNV_FK FOREIGN KEY(MaNhanVien) REFERENCES QLNhanVien(MaNhanVien)
)
DELETE TAIKHOAN
WHERE Pass = 'as'
CREATE TABLE CHITIETPHIEU(
MaPhieu char(30) ,
MaHang char(30) ,
DonGia float,
SoLuong int ,
ThanhTien int ,
CONSTRAINT PRKEY6 PRIMARY KEY(MaPhieu,MaHang),
CONSTRAINT MaHang_FK FOREIGN KEY(MaHang) REFERENCES QLHANGHOA(MaHang),
)
