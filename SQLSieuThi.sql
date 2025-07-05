CREATE DATABASE SQLSieuThi
GO 
USE SQLSieuThi
GO

CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(255),
    VaiTro NVARCHAR(20) 
);

CREATE TABLE KhachHang (
    MaKhachHang INT PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SDT NVARCHAR(20),
    DiaChi NVARCHAR(255),
    NgayDangKy DATE,
    FOREIGN KEY (MaKhachHang) REFERENCES TaiKhoan(MaTaiKhoan)
);


CREATE TABLE QuanLy (
    MaQuanLy INT  PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SDT NVARCHAR(20),
    FOREIGN KEY (MaQuanLy) REFERENCES TaiKhoan(MaTaiKhoan)
);

CREATE TABLE DanhMuc (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100)
);

CREATE TABLE SanPham (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    GiaBan DECIMAL(18, 2),
    SoLuong INT,
    NgayNhap DATE,
    TrangThai NVARCHAR(50),
    HinhAnh NVARCHAR(255),
    MaQuanLy INT,
	MaDanhMuc INT,
    FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy),
	FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
);


CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT,
    NgayTao DATE,
    TongTien DECIMAL(18, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

CREATE TABLE ChiTietGioHang (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaGioHang INT,
    MaSanPham INT,
    SoLuong INT,
    Gia DECIMAL(18, 2),
    FOREIGN KEY (MaGioHang) REFERENCES GioHang(MaGioHang),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSP)
);

CREATE TABLE DonHang (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT,
    NgayDat DATE,
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

CREATE TABLE ChiTietDonHang (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHang INT,
    MaSanPham INT,
    SoLuong INT,
    DonGia DECIMAL(18, 2),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSP)
);

CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHang INT,
    NgayLap DATE,
    TongTien DECIMAL(18, 2),
    TrangThaiTT NVARCHAR(50),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);

CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT,
    MaHoaDon INT UNIQUE,
    NgayThanhToan DATE,
    PhuongThuc NVARCHAR(50),
    SoTien DECIMAL(18, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon)
);

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro) VALUES
('admin', 'admin123', 'Admin'),
('user', 'user123', 'User'),
('user2', 'user456', 'User');
select *from TaiKhoan
INSERT INTO QuanLy (MaQuanLy,HoTen, Email, SDT) VALUES
(1,N'Nguyễn Văn A', 'admin@example.com', '0123456789');

INSERT INTO KhachHang (MaKhachHang,HoTen, Email, SDT, DiaChi, NgayDangKy) VALUES
(2,N'Trần Thị B', 'user1@example.com', '0987654321', N'Hà Nội', GETDATE()),
(3,N'Lê Văn C', 'user2@example.com', '0909123456', N'Hồ Chí Minh', GETDATE());			

INSERT INTO DanhMuc (TenDanhMuc) VALUES
(N'Đồ uống'),
(N'Thực phẩm'),
(N'Gia dụng');

select * from HoaDon
select * from DonHang
select *from SanPham
SELECT * FROM DanhMuc;
