using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sistemecitasAPI.Models;

public partial class SistemaCitasContext : DbContext
{
    public SistemaCitasContext()
    {
    }

    public SistemaCitasContext(DbContextOptions<SistemaCitasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<HistorialCambiosCita> HistorialCambiosCitas { get; set; }

    public virtual DbSet<HorariosEmpleado> HorariosEmpleados { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<UsuariosAdmin> UsuariosAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=SistemaCitas; User Id=SisCit; Password=123456; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__394B0202D22BDF0C");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Agendada')");
            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdCliente__4CA06362");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdEmplead__4D94879B");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdServici__4E88ABD4");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D594664290BC12ED");

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EB4E4FF84");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<HistorialCambiosCita>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__9CC7DBB4D25E71E6");

            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.HistorialCambiosCita)
                .HasForeignKey(d => d.IdCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__IdCit__52593CB8");
        });

        modelBuilder.Entity<HorariosEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horarios__1539229BD527CA76");

            entity.ToTable("HorariosEmpleado");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.HorariosEmpleados)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HorariosE__IdEmp__48CFD27E");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__2DCCF9A2817BC335");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<UsuariosAdmin>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF972737C94B");

            entity.ToTable("UsuariosAdmin");

            entity.HasIndex(e => e.Usuario, "UQ__Usuarios__E3237CF78767C2FB").IsUnique();

            entity.Property(e => e.ContrasenaHash).HasMaxLength(255);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Admin')");
            entity.Property(e => e.Usuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
