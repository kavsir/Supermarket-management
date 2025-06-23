using System;
using System.Collections.Generic;

namespace Supermarket_management.Models_Scaffolded;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public int? MaDonHang { get; set; }

    public DateOnly? NgayLap { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThaiTt { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual ThanhToan? ThanhToan { get; set; }
}
