using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CollegeDbContext _dbContext;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, CollegeDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All",Name = "GetAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDTO>> GetStudent()
        {
            _logger.LogInformation("GetStudent called");
            var students = await _dbContext.Students.ToListAsync();

            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

            return Ok(studentDTOData);
        }

        [HttpGet]
        [Route("{id}",Name ="GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            // BadRequest - 400 - client error
            if (id <= 0)
            {
                _logger.LogWarning("Bad request for student id={Id}", id);
                return BadRequest();
            }

            var student = await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            // NotFound - 404 - client error
            if (student == null)
            {
                _logger.LogError("Student with id={Id} not found", id);
                return NotFound($"cant find student have id={id}");
            }
            
            var studentDTO = _mapper.Map<StudentDTO>(student);

            // Ok - 200 - success
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody]StudentDTO dto)
        {
            if (dto == null)
                return BadRequest();
            Student student = _mapper.Map<Student>(dto);

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            dto.Id = student.Id;
            
            return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("Update")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> UpdateStudent([FromBody]StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest();
            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();
            if (existingStudent == null)
                return NotFound($"cant find student have id={dto.Id}");
            var newRecord = _mapper.Map<Student>(dto) ;
            _dbContext.Students.Update(newRecord);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> UpdateStudentPartial(int id,[FromBody] JsonPatchDocument<StudentDTO> pathDocument)
        {
            if (pathDocument == null || id <= 0)
                return BadRequest();

            var existingStudent =await _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();
            if (existingStudent == null)
                return NotFound();

            var studentDTO = _mapper.Map<StudentDTO>(existingStudent);
            pathDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent = _mapper.Map<Student>(studentDTO);
            _dbContext.Students.Update(existingStudent);

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var student = await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound($"cant find student have id={id}");
            }
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Index action trace log");
            _logger.LogDebug("Index action debug log");
            _logger.LogInformation("Index action information log");
            _logger.LogWarning("Index action warning log");
            _logger.LogError("Index action error log");
            _logger.LogCritical("Index action critical log");
            return Ok();
        }
    }
}
