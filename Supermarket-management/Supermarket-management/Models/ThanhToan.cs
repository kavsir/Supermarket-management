using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class ThanhToan
{
    public int MaThanhToan { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaHoaDon { get; set; }

    public DateOnly? NgayThanhToan { get; set; }

    public string? PhuongThuc { get; set; }

    public decimal? SoTien { get; set; }

    public virtual HoaDonController? MaHoaDonNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
