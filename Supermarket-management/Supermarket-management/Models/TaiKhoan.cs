using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? VaiTro { get; set; }

    public virtual KhachHang? KhachHang { get; set; }

    public virtual QuanLy? QuanLy { get; set; }
}
