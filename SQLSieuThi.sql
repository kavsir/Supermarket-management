CREATE DATABASE SQLSieuThi
GO 
USE SQLSieuThi
GO

CREATE TABLE TaiKhoan (
    MaTaiKhoan INT PRIMARY KEY,
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(255),
    VaiTro NVARCHAR(20) 
);
INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, VaiTro) VALUES
 (1, 'admin', 'admin123', 'Admin'),
 (2, 'user', 'user123', 'User');





CREATE TABLE KhachHang (
    MaKhachHang INT PRIMARY KEY,
    MaTaiKhoan INT,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SDT NVARCHAR(20),
    DiaChi NVARCHAR(255),
    NgayDangKy DATE,
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);


CREATE TABLE QuanLy (
    MaQuanLy INT PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SDT NVARCHAR(20),
    TaiKhoanID INT,
    FOREIGN KEY (TaiKhoanID) REFERENCES TaiKhoan(MaTaiKhoan)
);


CREATE TABLE SanPham (
    MaSP INT PRIMARY KEY,
    TenSP NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    GiaBan DECIMAL(18, 2),
    SoLuong INT,
    NgayNhap DATE,
    TrangThai NVARCHAR(50),
    HinhAnh NVARCHAR(255),
    MaQuanLy INT,
    FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy)
);


CREATE TABLE GioHang (
    MaGioHang INT PRIMARY KEY,
    MaKhachHang INT,
    NgayTao DATE,
    TongTien DECIMAL(18, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

CREATE TABLE ChiTietGioHang (
    MaChiTiet INT PRIMARY KEY,
    MaGioHang INT,
    MaSanPham INT,
    SoLuong INT,
    Gia DECIMAL(18, 2),
    FOREIGN KEY (MaGioHang) REFERENCES GioHang(MaGioHang),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSP)
);

CREATE TABLE DonHang (
    MaDonHang INT PRIMARY KEY,
    MaKhachHang INT,
    NgayDat DATE,
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

CREATE TABLE ChiTietDonHang (
    MaChiTiet INT PRIMARY KEY,
    MaDonHang INT,
    MaSanPham INT,
    SoLuong INT,
    DonGia DECIMAL(18, 2),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSP)
);

CREATE TABLE HoaDon (
    MaHoaDon INT PRIMARY KEY,
    MaDonHang INT,
    NgayLap DATE,
    TongTien DECIMAL(18, 2),
    TrangThaiTT NVARCHAR(50),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);

CREATE TABLE ThanhToan (
    MaThanhToan INT PRIMARY KEY,
    MaKhachHang INT,
    MaHoaDon INT UNIQUE,
    NgayThanhToan DATE,
    PhuongThuc NVARCHAR(50),
    SoTien DECIMAL(18, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon)
);


ALTER TABLE TaiKhoan
DROP COLUMN MaKhachHang;
