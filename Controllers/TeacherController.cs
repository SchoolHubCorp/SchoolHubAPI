using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.Classroom;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Models.Pupil;
using SchoolHubApi.Models.Teacher;
using SchoolHubApi.Models.UserDto;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Repositories.Interface;
using System.Security.Claims;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public TeacherController(ITeacherRepository teacherRepository, IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }
        [HttpPost("register/teacher"), Auth(Role.Admin)]
        public async Task<ActionResult<TeacherModel>> RegisterTeacher([FromBody] TeacherDto request)
        {
            if (_userRepository
                .Find(x => x.Email == request.Email)
                .Any())
                return Conflict("User with this email already exists.");

            HashPasswordHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var teacher = new Teacher()
            {
                UserData = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Pesel = request.Pesel,
                    Role = Role.Teacher,
                }
            };

            _teacherRepository.Add(teacher);
            await _teacherRepository.SaveChangesAsync();
            return new TeacherModel(teacher.Id, teacher.UserData.Email, teacher.UserData.FirstName, teacher.UserData.LastName);
        }
        [HttpGet, Auth(Role.Admin)]
        public async Task<ActionResult<List<TeacherShortModel>>> GetAllTeachers()
        {
            return await _teacherRepository
                .GetAll()
                .OrderBy(x => x.UserData.FirstName)
                .Select(x => new TeacherShortModel(
                    x.Id,
                    x.UserData.FirstName,
                    x.UserData.LastName,
                    x.Courses.Count
                    )).ToListAsync();
        }
        [HttpGet("{teacherid:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<TeacherDetailModel>> GetTeacherDetailInfo(int teacherid)
        {
            var teacher = await _teacherRepository
                .Find(x => x.Id == teacherid)
                .Include(x => x.Courses)
                .Select(x => new TeacherDetailModel(
                    x.Id,
                    x.UserData.FirstName,
                    x.UserData.LastName,
                    x.UserData.Email,
                    x.UserData.PhoneNumber,
                    x.UserData.Pesel,
                    x.Courses.Select(course => new CourseClassModel(
                        course.Id,
                        course.CourseName,
                        course.Classroom.ClassName)).ToList()))
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("teacher not found");

            return Ok(teacher);
        }
        [HttpGet("teacherCourses"), Auth(Role.Teacher)]
        public async Task<ActionResult<List<CourseModel>>> GetTeacherCourses()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);

            var teacher = await _teacherRepository
                .Find(x =>x.UserDataEmail == email)
                .Include(t => t.Courses)  
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("Teacher not found");

            if (teacher.Courses == null || teacher.Courses.Count == 0)
                return NotFound("Teacher doesn't have courses");

            return  teacher.Courses.Select(x => new CourseModel(x.Id,x.CourseName)).ToList();
        }
        
        [HttpGet("{teacherId:int}/plan"), Auth(Role.Admin)]
        public async Task<ActionResult> GetTeacherImage(int teacherId)
        {
            var teacher = await _teacherRepository
                .Find(x => x.Id == teacherId)
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("Teacher not found");

            if (teacher.TeacherPlan == null)
                return NotFound("Teacher doesnt have Plan");

            return File(teacher.TeacherPlan, teacher.TeacherPlanContentType);
        }

        [HttpGet("plan"), Auth(Role.Teacher)]
        public async Task<ActionResult> GetTeacherPlan()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var teacher = await _teacherRepository
                .Find(x => x.UserData.Email == email)
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("Teacher not found");

            if (teacher.TeacherPlan == null)
                return NotFound("Teacher doesnt have Plan");

            return File(teacher.TeacherPlan, teacher.TeacherPlanContentType);
        }
        [HttpPost("{teacherId:int}/plan"), Auth(Role.Admin)]
        public async Task<ActionResult> UploadPlan(int teacherId, IFormFile file)
        {
            var teacher = _teacherRepository
                .FindWithTracking(x => x.Id == teacherId)
                .FirstOrDefault();

            if (teacher == null)
                return NotFound("Teacher not found");

            if ((file.ContentType != "image/jpeg") &&
                (file.ContentType != "image/png") &&
                (file.ContentType != "image/jpg"))
                return BadRequest("Not propriate file type");

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    teacher.TeacherPlanContentType = file.ContentType;
                    teacher.TeacherPlan = stream.ToArray();
                    await _teacherRepository.SaveChangesAsync();
                }
            }
            return Ok("Plan uploaded successfully.");
        }
        [HttpPut("{teacherId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<TeacherShortModel>> UpdateTeacherInfo(int teacherId, [FromBody] TeacherDetailUpdateModel request)
        {
            var teacher = await _teacherRepository
                .FindWithTracking(x => x.Id == teacherId)
                .Include(x => x.UserData)
                .Include(x => x.Courses)
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("Teacher not found");

            if (_userRepository.Find(x => x.Pesel == request.Pesel && x.Email != teacher.UserDataEmail).Any())
                return BadRequest("There is Somebody with this Pesel");
            if (_userRepository.Find(x => x.PhoneNumber == request.PhoneNumber && x.Email != teacher.UserDataEmail).Any())
                return BadRequest("There is also pupil with this PhoneNumber");

            teacher.UserData.FirstName = request.FirstName;
            teacher.UserData.LastName = request.LastName;
            teacher.UserData.PhoneNumber = request.PhoneNumber;
            teacher.UserData.Pesel = request.Pesel;

            await _teacherRepository.SaveChangesAsync();

            return new TeacherShortModel(teacher.Id, teacher.UserData.FirstName, teacher.UserData.LastName, teacher.Courses.Count());
        }
        [HttpDelete("{teacherId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult> DeleteTeacher(int teacherId)
        {
            var teacher = await _teacherRepository
                .FindWithTracking(x => x.Id == teacherId)
                .Include(x => x.UserData)
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound("Teacher not found");
            
            _teacherRepository.Remove(teacher);
            _userRepository.Remove(teacher.UserData);
            await _teacherRepository.SaveChangesAsync();
            await _userRepository.SaveChangesAsync();  

            return Ok($"Teacher {teacher.UserData.Email}, {teacher.UserData.FirstName} {teacher.UserData.LastName} was delated");
        }
    }
}
