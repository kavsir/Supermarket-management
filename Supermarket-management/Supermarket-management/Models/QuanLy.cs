using System;
using System.Collections.Generic;

namespace Supermarket_management.Models;

public partial class QuanLy
{
    public int MaQuanLy { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public virtual TaiKhoan MaQuanLyNavigation { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
