using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public DateOnly? NgayDangKy { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual TaiKhoan MaKhachHangNavigation { get; set; } = null!;

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
