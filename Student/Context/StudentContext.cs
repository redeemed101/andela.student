using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Context
{
    public class StudentContext : DbContext
    {

        public StudentContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Entities.Student>(entity => {
                entity.HasMany<Entities.Course>();
                entity.HasKey(s => s.ID);
                entity.HasData(
                       new Entities.Student
                       {
                           ID = 1,
                           Name = "Lewis Msasa",
                       },
                       new Entities.Student
                       {
                           ID = 2,
                           Name = "Mwayi Msasa"
                       }
                    );
            }) ;
            modelBuilder.Entity<Entities.Course>(entity => {
               
                entity.HasKey(s => s.ID);
                entity.HasData(
                      new Entities.Course
                      {
                          ID = 1,
                          Name = "Physics"
                      },
                      new Entities.Course
                      {
                          ID = 2,
                          Name = "Mathematics"
                      }
                    );
            });
            modelBuilder.Entity<Entities.StudentCourse>()
             .HasKey(s => new { s.CourseId, s.StudentId });
            modelBuilder.Entity<Entities.StudentCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(bc => bc.CourseId);
            modelBuilder.Entity<Entities.StudentCourse>()
                .HasOne(bc => bc.Student)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<Entities.StudentCourse>().HasData(
                new Entities.StudentCourse
                {
                     CourseId = 1,
                     StudentId = 1
                },
                 new Entities.StudentCourse
                 {
                     CourseId = 2,
                     StudentId = 2
                 }
            );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Entities.Student> Students { get; set; }
        public DbSet<Entities.Course> Courses { get; set; }

        public DbSet<Entities.StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
