using Microsoft.EntityFrameworkCore;
using piy.attendance.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Lecture> Lectures => Set<Lecture>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasIndex(x => x.EnrollmentNumber).IsUnique();
                e.Property(x => x.FullName).HasMaxLength(150).IsRequired();
                e.Property(x => x.Email).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<Class>(e =>
            {
                e.Property(x => x.ClassName).HasMaxLength(50).IsRequired();
                e.Property(x => x.Subject).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Lecture>(e =>
            {
                e.HasOne(x => x.Class).WithMany(c => c.Lectures).HasForeignKey(x => x.ClassId);
                e.Property(x => x.Topic).HasMaxLength(200).IsRequired();
                e.Property(x => x.Latitude).HasColumnType("decimal(9,6)");
                e.Property(x => x.Longitude).HasColumnType("decimal(9,6)");
                e.Property(x => x.QrToken).HasMaxLength(500);
            });

            modelBuilder.Entity<Attendance>(e =>
            {
                e.HasOne(x => x.Lecture).WithMany(l => l.Attendances).HasForeignKey(x => x.LectureId);
                e.HasOne(x => x.Student).WithMany(s => s.Attendances).HasForeignKey(x => x.StudentId);
                e.HasIndex(x => new { x.LectureId, x.StudentId }).IsUnique();
                e.Property(x => x.Latitude).HasColumnType("decimal(9,6)");
                e.Property(x => x.Longitude).HasColumnType("decimal(9,6)");
            });
        }
    }
}
