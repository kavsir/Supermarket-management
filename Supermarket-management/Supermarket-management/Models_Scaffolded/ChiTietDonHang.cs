using System;
using System.Collections.Generic;

namespace Supermarket_management.Models_Scaffolded;

public partial class ChiTietDonHang
{
    public int MaChiTiet { get; set; }

    public int? MaDonHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
