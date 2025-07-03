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

INSERT INTO QuanLy (MaQuanLy,HoTen, Email, SDT) VALUES
(1,N'Nguyễn Văn A', 'admin@example.com', '0123456789');

INSERT INTO KhachHang (MaKhachHang,HoTen, Email, SDT, DiaChi, NgayDangKy) VALUES
(2,N'Trần Thị B', 'user1@example.com', '0987654321', N'Hà Nội', GETDATE()),
(3,N'Lê Văn C', 'user2@example.com', '0909123456', N'Hồ Chí Minh', GETDATE());


INSERT INTO DanhMuc (TenDanhMuc) VALUES
(N'Đồ uống'),
(N'Thực phẩm'),
(N'Gia dụng');

UPDATE DanhMuc SET TenDanhMuc = N'Đồ uống' WHERE MaDanhMuc = 1;
UPDATE DanhMuc SET TenDanhMuc = N'Thực phẩm' WHERE MaDanhMuc = 2;
UPDATE DanhMuc SET TenDanhMuc = N'Gia dụng' WHERE MaDanhMuc = 3;


INSERT INTO SanPham (TenSP, MoTa, GiaBan, SoLuong, NgayNhap, TrangThai, MaDanhMuc, HinhAnh, MaQuanLy) VALUES
(N'Nồi cơm điện', N'Nồi cơm điện siêu cấp vip pro', 300000, 50, GETDATE(), N'Còn hàng', 3, 'noicomdien.jpg', 1),
(N'Sữa tươi', N'Sữa tươi nguyên chất 1L', 25000, 100, GETDATE(), N'Còn hàng', 1, 'sua.jpg', 1),
(N'Bánh mì', N'Bánh mì Pháp giòn', 15000, 50, GETDATE(), N'Còn hàng', 2, 'banhmi.jpg', 1);

INSERT INTO GioHang (MaKhachHang, NgayTao, TongTien) VALUES
(2, GETDATE(), 40000),
(3, GETDATE(), 15000);

INSERT INTO ChiTietGioHang (MaGioHang, MaSanPham, SoLuong, Gia) VALUES
(1, 1, 1, 25000),  -- user1 mua 1 sữa tươi
(1, 2, 1, 15000),  -- user1 mua 1 bánh mì
(2, 2, 1, 15000);  -- user2 mua 1 bánh mì

INSERT INTO DonHang (MaKhachHang, NgayDat, TrangThai, GhiChu) VALUES
(2, GETDATE(), N'Chờ xử lý', N'Đang xử lý');

INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia) VALUES
(1, 1, 1, 25000),
(1, 2, 1, 15000);

INSERT INTO HoaDon (MaDonHang, NgayLap, TongTien, TrangThaiTT) VALUES
(1, GETDATE(), 40000, N'Chưa thanh toán');

INSERT INTO ThanhToan (MaKhachHang, MaHoaDon, NgayThanhToan, PhuongThuc, SoTien) VALUES
(2, 1, GETDATE(), N'Tiền mặt', 40000);

select * from HoaDon
select * from DonHang

     