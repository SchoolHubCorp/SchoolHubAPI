using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.Classroom;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Repositories.Interface;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Controllers.Attributes;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.Pupil;
using SchoolHubApi.Models.Parent;

namespace SchoolHubApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;

        public CourseController(ITeacherRepository teacherRepository, ICourseRepository courseRepository, IClassRepository classRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
            _classRepository = classRepository;
        }

        [HttpPost, Auth(Role.Admin)]
        public async Task<ActionResult<CourseModel>> CreateCourse([FromBody] CourseDto request)
        {
            var classroom = _classRepository.Find(x => x.Id == request.ClassroomId).FirstOrDefault();
            if (classroom == null)
                return BadRequest("Classroom not found, cant add subject");

            var teacher = _teacherRepository.Find(x => x.Id == request.TeacherId).FirstOrDefault();
            if (teacher == null)
                return BadRequest("Teacher not found, cant add subject");

            if (_courseRepository.Find(x => x.CourseName == request.CourseName).Any())
                return BadRequest("Course name already exists");

            var course = new Course(request.CourseName)
            {
                ClassroomId = request.ClassroomId,
                TeacherId = request.TeacherId
            };

            _courseRepository.Add(course);
            await _courseRepository.SaveChangesAsync();
            return new CourseModel(course.Id, course.CourseName);
        }

        [HttpGet, Auth(Role.Admin)]
        public async Task<ActionResult<List<CourseClassModel>>> GetAllCourses()
        {
            return await _courseRepository
                .GetAll()
                .OrderBy(x => x.CourseName)
                .Select(x => new CourseClassModel(x.Id, x.CourseName, x.Classroom.ClassName))
                .ToListAsync();
        }

        [HttpDelete("{courseId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<CourseModel>> DeleteCourse(int courseId)
        {
            var course = await _courseRepository
                .FindWithTracking(x => x.Id == courseId)
                .FirstOrDefaultAsync();

            if (course == null)
                return NotFound("Pupil not found");

            _courseRepository.Remove(course);

            await _courseRepository.SaveChangesAsync();

            return Ok("Course was deleted sucsessfully");
        }
    }
}