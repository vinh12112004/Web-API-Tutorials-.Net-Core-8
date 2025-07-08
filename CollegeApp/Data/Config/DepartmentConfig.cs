using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).UseIdentityColumn();

            builder.Property(d => d.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Description).HasMaxLength(500).IsRequired(false);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmentName = "ECE",
                    Description = "Electronics and Communication Engineering"
                },
                new Department
                {
                    Id = 2,
                    DepartmentName = "CSE",
                    Description = "Computer Science and Engineering"
                },
            });
        }
    }
}
