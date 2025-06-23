using System;
using System.Collections.Generic;

namespace Supermarket_management.Models_Scaffolded;

public partial class ChiTietGioHang
{
    public int MaChiTiet { get; set; }

    public int? MaGioHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
