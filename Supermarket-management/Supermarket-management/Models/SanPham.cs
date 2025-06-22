using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public string? MoTa { get; set; }

    public decimal? GiaBan { get; set; }

    public int? SoLuong { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public string? TrangThai { get; set; }

    public string? HinhAnh { get; set; }

    public int? MaQuanLy { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual QuanLy? MaQuanLyNavigation { get; set; }
}
