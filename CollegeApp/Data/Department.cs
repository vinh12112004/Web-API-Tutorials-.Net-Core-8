namespace CollegeApp.Data
{
    public class Department
    {
        public int Id { get; set; }
        public String DepartmentName { get; set; }
        public String Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
