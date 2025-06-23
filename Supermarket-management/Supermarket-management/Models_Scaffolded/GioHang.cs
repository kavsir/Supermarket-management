using System;
using System.Collections.Generic;

namespace Supermarket_management.Models_Scaffolded;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaKhachHang { get; set; }

    public DateOnly? NgayTao { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
