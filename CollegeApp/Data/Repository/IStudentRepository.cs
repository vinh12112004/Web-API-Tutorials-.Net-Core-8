namespace CollegeApp.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentByFeeStatusAsync(bool isFeeStatus);
    }
}
