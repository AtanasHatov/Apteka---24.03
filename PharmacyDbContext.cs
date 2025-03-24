using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project1.Data.Models;

public partial class PharmacyDbContext : DbContext
{
    public PharmacyDbContext()
    {
    }

    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=STUDENT7;Initial Catalog=PharmacyDB;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA8FA093378");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.IdMedicine).HasName("PK__Medicine__1F746A2EAA43FEE0");

            entity.HasIndex(e => e.Name, "UQ__Medicine__72E12F1B5FB4357C").IsUnique();

            entity.Property(e => e.IdMedicine).HasColumnName("id_medicine");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__DD5B8F3F8D7973C5");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IdMedicine).HasColumnName("id_medicine");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.QuantityOrdered).HasColumnName("quantity_ordered");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("supplier_name");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Orders__employee__412EB0B6");

            entity.HasOne(d => d.IdMedicineNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdMedicine)
                .HasConstraintName("FK__Orders__id_medic__403A8C7D");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.IdPrescriptions).HasName("PK__Prescrip__4247CB6677A53A20");

            entity.Property(e => e.IdPrescriptions).HasColumnName("id_prescriptions");
            entity.Property(e => e.DateIssued).HasColumnName("date_issued");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("doctor_name");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IdMedicine).HasColumnName("id_medicine");
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_name");

            entity.HasOne(d => d.Employee).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Prescript__emplo__3D5E1FD2");

            entity.HasOne(d => d.IdMedicineNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdMedicine)
                .HasConstraintName("FK__Prescript__id_me__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
