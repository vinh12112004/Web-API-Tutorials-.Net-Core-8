using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Student>
            {
                new Student
                {
                    Id = 1,
                    StudentName = "John Doe",
                    Email = "JD@gmail.com",
                    Address = "123 Main St, Springfield",
                    DOB = new DateTime(2000, 1, 1)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Jane Smith",
                    Email = "JS@gmail.com",
                    Address = "456 Elm St, Springfield",
                    DOB = new DateTime(2001, 2, 2)
                }
            });

            builder.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentID)
                .HasConstraintName("FK_Students_Departments");
        }
    }
}
