using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FileJson.Models;

public partial class FileContext : DbContext
{
    public FileContext()
    {
    }

    public FileContext(DbContextOptions<FileContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradores { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=File;Trusted_Connection=True;MultipleActiveResultSets=True;Integrated Security=true;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.ToTable("Colaborador");

            entity.Property(e => e.Cui)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CUI");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colaborador_Sucursal");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.ToTable("Empresa");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.ToTable("Sucursal");

            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursal_Empresa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
