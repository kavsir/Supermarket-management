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
    MaQuanLy INT PRIMARY KEY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SDT NVARCHAR(20),
    FOREIGN KEY (MaQuanLy) REFERENCES TaiKhoan(MaTaiKhoan)
);

CREATE TABLE DanhMuc (
    MaDanhMuc INT PRIMARY KEY,
    TenDanhMuc NVARCHAR(100)
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
	MaDanhMuc INT,
    FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy),
	FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
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

INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, VaiTro) VALUES
 (1, 'admin', 'admin123', 'Admin'),
 (2, 'user', 'user123', 'User'),
 (3, 'user2', 'user456', 'User');

 INSERT INTO QuanLy (MaQuanLy, HoTen, Email, SDT) VALUES
(1, N'Nguyễn Văn A', 'admin@example.com', '0123456789');

INSERT INTO KhachHang (MaKhachHang, HoTen, Email, SDT, DiaChi, NgayDangKy) VALUES
(2, N'Trần Thị B', 'user1@example.com', '0987654321', N'Hà Nội', GETDATE()),
(3, N'Lê Văn C', 'user2@example.com', '0909123456', N'Hồ Chí Minh', GETDATE());

INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc) VALUES
(1, 'Đồ uống'),
(2, 'Thực phẩm'),
(3, 'Gia dụng');


INSERT INTO SanPham (MaSP, TenSP, MoTa, GiaBan, SoLuong, NgayNhap, TrangThai,MaDanhMuc, HinhAnh, MaQuanLy) VALUES
(1, N'Sữa tươi', N'Sữa tươi nguyên chất 1L', 25000, 100, GETDATE(), N'Còn hàng',1, 'sua.jpg', 1),
(2, N'Bánh mì', N'Bánh mì Pháp giòn', 15000, 50, GETDATE(), N'Còn hàng',2, 'banhmi.jpg', 1);

INSERT INTO GioHang (MaGioHang, MaKhachHang, NgayTao, TongTien) VALUES
(1, 2, GETDATE(), 40000),
(2, 3, GETDATE(), 15000);

INSERT INTO ChiTietGioHang (MaChiTiet, MaGioHang, MaSanPham, SoLuong, Gia) VALUES
(1, 1, 1, 1, 25000),  -- user1 mua 1 sữa tươi
(2, 1, 2, 1, 15000),  -- user1 mua 1 bánh mì
(3, 2, 2, 1, 15000);  -- user2 mua 1 bánh mì

INSERT INTO DonHang (MaDonHang, MaKhachHang, NgayDat, TrangThai, GhiChu) VALUES
(1, 2, GETDATE(), N'Chờ xử lý', N'Giao giờ hành chính');

INSERT INTO ChiTietDonHang (MaChiTiet, MaDonHang, MaSanPham, SoLuong, DonGia) VALUES
(1, 1, 1, 1, 25000),
(2, 1, 2, 1, 15000);

INSERT INTO HoaDon (MaHoaDon, MaDonHang, NgayLap, TongTien, TrangThaiTT) VALUES
(1, 1, GETDATE(), 40000, N'Chưa thanh toán');

INSERT INTO ThanhToan (MaThanhToan, MaKhachHang, MaHoaDon, NgayThanhToan, PhuongThuc, SoTien) VALUES
(1, 2, 1, GETDATE(), N'Tiền mặt', 40000);
