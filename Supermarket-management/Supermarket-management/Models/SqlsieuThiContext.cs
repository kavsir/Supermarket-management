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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BCNU3UF\\SQLEXPRESS; Database=SQLSieuThi;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietD__CDF0A11477FA0E36");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaChiTiet).ValueGeneratedNever();
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__ChiTietDo__MaDon__6383C8BA");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietDo__MaSan__6477ECF3");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__ChiTietG__CDF0A114811804D2");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.MaChiTiet).ValueGeneratedNever();
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__ChiTietGi__MaGio__5CD6CB2B");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietGi__MaSan__5DCAEF64");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD03958564");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDonHang).ValueGeneratedNever();
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__DonHang__MaKhach__60A75C0F");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3B1286180");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang).ValueGeneratedNever();
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__GioHang__MaKhach__59FA5E80");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B246047AD");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThaiTt)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiTT");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__HoaDon__MaDonHan__6754599E");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E5C9B6A523");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaKhachHangNavigation).WithOne(p => p.KhachHang)
                .HasForeignKey<KhachHang>(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachHang__MaKha__5165187F");
        });

        modelBuilder.Entity<QuanLy>(entity =>
        {
            entity.HasKey(e => e.MaQuanLy).HasName("PK__QuanLy__2AB9EAF80A03E4AD");

            entity.ToTable("QuanLy");

            entity.Property(e => e.MaQuanLy).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaQuanLyNavigation).WithOne(p => p.QuanLy)
                .HasForeignKey<QuanLy>(d => d.MaQuanLy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuanLy__MaQuanLy__5441852A");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C385C90BB");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp)
                .ValueGeneratedNever()
                .HasColumnName("MaSP");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaQuanLyNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaQuanLy)
                .HasConstraintName("FK__SanPham__MaQuanL__571DF1D5");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C652991BEDD6B");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaTaiKhoan).ValueGeneratedNever();
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B25844DDD6C660");

            entity.ToTable("ThanhToan");

            entity.HasIndex(e => e.MaHoaDon, "UQ__ThanhToa__835ED13ADE17212E").IsUnique();

            entity.Property(e => e.MaThanhToan).ValueGeneratedNever();
            entity.Property(e => e.PhuongThuc).HasMaxLength(50);
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaHoaDonNavigation).WithOne(p => p.ThanhToan)
                .HasForeignKey<ThanhToan>(d => d.MaHoaDon)
                .HasConstraintName("FK__ThanhToan__MaHoa__6C190EBB");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__ThanhToan__MaKha__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
