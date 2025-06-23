// Thêm namespace cho class TaiKhoan
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Supermarket_management.Models
{
    public partial class TaiKhoan
    {
        [Key]
        [BindNever]
        public int MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public virtual QuanLy? QuanLy { get; set; }
    }
}
