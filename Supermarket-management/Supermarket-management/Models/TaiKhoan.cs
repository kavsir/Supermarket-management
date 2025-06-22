using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? VaiTro { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<QuanLy> QuanLies { get; set; } = new List<QuanLy>();
}
