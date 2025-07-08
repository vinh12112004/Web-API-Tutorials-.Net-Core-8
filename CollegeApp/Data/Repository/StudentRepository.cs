

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDbContext _dbContext;
        public StudentRepository(CollegeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Student>> GetStudentByFeeStatusAsync(bool isFeeStatus)
        {
            //write code
            return null;
        }
    }
}
