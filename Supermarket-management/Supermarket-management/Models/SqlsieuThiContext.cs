using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Supermarket_management.Models;

public partial class SqlsieuThiContext : DbContext
{
    public SqlsieuThiContext()
    {
    }

    public SqlsieuThiContext(DbContextOptions<SqlsieuThiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<QuanLy> QuanLies { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SEELE;Database=SQLSieuThi;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietD__CDF0A11490620E7D");
=======
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietD__CDF0A11436A78CE5");
>>>>>>> origin/main

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
<<<<<<< HEAD
                .HasConstraintName("FK__ChiTietDo__MaDon__4BAC3F29");
=======
                .HasConstraintName("FK__ChiTietDo__MaDon__60A75C0F");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietDo__MaSan__619B8048");
>>>>>>> origin/main
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietG__CDF0A11470F8368B");
=======
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietG__CDF0A114B37C8493");
>>>>>>> origin/main

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
<<<<<<< HEAD
                .HasConstraintName("FK__ChiTietGi__MaGio__44FF419A");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887387A63E4");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MaDanhMuc).ValueGeneratedNever();
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
=======
                .HasConstraintName("FK__ChiTietGi__MaGio__59FA5E80");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietGi__MaSan__5AEE82B9");
>>>>>>> origin/main
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887C2E5556A");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD002359A6");
=======
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD0BED52D7");
>>>>>>> origin/main

            entity.ToTable("DonHang");

            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKhachHang)
<<<<<<< HEAD
                .HasConstraintName("FK__DonHang__MaKhach__48CFD27E");
=======
                .HasConstraintName("FK__DonHang__MaKhach__5DCAEF64");
>>>>>>> origin/main
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA314EEB7A2");
=======
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3C541E766");
>>>>>>> origin/main

            entity.ToTable("GioHang");

            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaKhachHang)
<<<<<<< HEAD
                .HasConstraintName("FK__GioHang__MaKhach__4222D4EF");
=======
                .HasConstraintName("FK__GioHang__MaKhach__571DF1D5");
>>>>>>> origin/main
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13BBC8B8D24");
=======
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13BD2E7075A");
>>>>>>> origin/main

            entity.ToTable("HoaDon");

            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThaiTt)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiTT");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDonHang)
<<<<<<< HEAD
                .HasConstraintName("FK__HoaDon__MaDonHan__4F7CD00D");
=======
                .HasConstraintName("FK__HoaDon__MaDonHan__6477ECF3");
>>>>>>> origin/main
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E5E5F9F034");
=======
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E56CD305CA");
>>>>>>> origin/main

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("FK__KhachHang__MaTai__398D8EEE");
        });

        modelBuilder.Entity<QuanLy>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaQuanLy).HasName("PK__QuanLy__2AB9EAF8A9294F6D");
=======
            entity.HasKey(e => e.MaQuanLy).HasName("PK__QuanLy__2AB9EAF89DFA9E2E");
>>>>>>> origin/main

            entity.ToTable("QuanLy");

            entity.Property(e => e.MaQuanLy).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");

            entity.HasOne(d => d.TaiKhoan).WithMany(p => p.QuanLies)
                .HasForeignKey(d => d.TaiKhoanId)
                .HasConstraintName("FK__QuanLy__TaiKhoan__3C69FB99");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C9C65F0A2");
=======
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C6AC05ABE");
>>>>>>> origin/main

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
<<<<<<< HEAD
                .HasConstraintName("FK__SanPham__MaDanhM__7E37BEF6");

            entity.HasOne(d => d.MaQuanLyNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaQuanLy)
                .HasConstraintName("FK__SanPham__MaQuanL__7D439ABD");
=======
                .HasConstraintName("FK__SanPham__MaDanhM__5441852A");

            entity.HasOne(d => d.MaQuanLyNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaQuanLy)
                .HasConstraintName("FK__SanPham__MaQuanL__534D60F1");
>>>>>>> origin/main
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529091CC1D5");
=======
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529E79C3DBC");
>>>>>>> origin/main

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B25844B8744B2F");

            entity.ToTable("ThanhToan");

            entity.HasIndex(e => e.MaHoaDon, "UQ__ThanhToa__835ED13A5E35194C").IsUnique();
=======
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B25844DD61BE3B");

            entity.ToTable("ThanhToan");

            entity.HasIndex(e => e.MaHoaDon, "UQ__ThanhToa__835ED13AE6C5F486").IsUnique();
>>>>>>> origin/main

            entity.Property(e => e.PhuongThuc).HasMaxLength(50);
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaHoaDonNavigation).WithOne(p => p.ThanhToan)
                .HasForeignKey<ThanhToan>(d => d.MaHoaDon)
<<<<<<< HEAD
                .HasConstraintName("FK__ThanhToan__MaHoa__5441852A");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__ThanhToan__MaKha__534D60F1");
=======
                .HasConstraintName("FK__ThanhToan__MaHoa__693CA210");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__ThanhToan__MaKha__68487DD7");
>>>>>>> origin/main
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
