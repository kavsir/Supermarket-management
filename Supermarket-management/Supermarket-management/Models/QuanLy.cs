using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class QuanLy
{
    public int MaQuanLy { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public int? TaiKhoanId { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();

    public virtual TaiKhoan? TaiKhoan { get; set; }
}
