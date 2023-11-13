using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.Classroom;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Models.Pupil;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassRepository _classRepository;

        public ClassroomController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet, Auth(Role.Admin)]
        public async Task<ActionResult<List<ClassroomModel>>> GetAllClassrooms()
        {
            return await _classRepository
                .GetAll()
                .OrderBy(x => x.ClassName)
                .Select(x => new ClassroomModel(x.Id, x.ClassName, x.ClassAccessCode))
                .ToListAsync();
        }

        [HttpGet("{classroomId:int}/plan"), Authorize]
        public async Task<ActionResult> GetClassImage(int classroomId)
        {
            var classroom = await _classRepository
                .Find(x => x.Id == classroomId)
                .FirstOrDefaultAsync();

            if (classroom == null)
                return NotFound("Classroom not found");

            return File(classroom.Plan, classroom.PlanContentType);
        }

        [HttpPost("{classroomId:int}/plan"), Auth(Role.Admin)]
        public async Task<ActionResult> UploadPlan(int classroomId, IFormFile file)
        {
            var classroom = _classRepository
                .FindWithTracking(x => x.Id == classroomId)
                .FirstOrDefault();

            if (classroom == null)
                return NotFound("Classroom not found");

            if ((file.ContentType != "image/jpeg") &&
                (file.ContentType != "image/png") &&
                (file.ContentType != "image/jpg"))
                return BadRequest("Not propriate file type");

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    classroom.PlanContentType = file.ContentType;
                    classroom.Plan = stream.ToArray();
                    await _classRepository.SaveChangesAsync();
                }
            }
            return Ok("Plan uploaded successfully.");
        }

        [HttpPost, Authorize(Roles = nameof(Role.Admin))]
        public async Task<ActionResult<ClassroomModel>> CreateClassroom([FromBody] ClassroomDto request)
        {
            if (_classRepository.Find(x => x.ClassName == request.ClassName).Any())
                return BadRequest("Class name already exists");
            
            var classroom = new Classroom(request.ClassName)
            {
                ClassAccessCode = AccessCodeGenerator.GenerateAccessCode(code => _classRepository.Find(x => x.ClassAccessCode == code).Any())
            };

            _classRepository.Add(classroom);
            await _classRepository.SaveChangesAsync();
            return new ClassroomModel(classroom.Id, classroom.ClassName, classroom.ClassAccessCode);
        }

        [HttpPut("{classroomId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult> UpdateClassName(int classroomId, [FromBody]ClassroomDto request)
        {
            var classroom = await _classRepository
                .FindWithTracking(x => x.Id == classroomId)
                .FirstOrDefaultAsync();

            if (classroomId == null)
                return NotFound("Classroom not found");

            if (_classRepository.Find(x => x.ClassName == request.ClassName).Any())
                return BadRequest("Class name already exists");

            classroom.ClassName = request.ClassName;

            await _classRepository.SaveChangesAsync();

            return Ok(classroom);
        }

        [HttpGet("{classroomId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<ClassroomDetailModel>> GetClassroomDetails(int classroomId)
        {
            var classroom = await _classRepository
                .Find(x => x.Id == classroomId)
                .Include(x => x.Pupils)
                .Select(x => new ClassroomDetailModel(
                    x.Id,
                    x.ClassName,
                    x.ClassAccessCode,
                    x.Pupils.Select(pupil => new PupilModel(
                        pupil.Id,
                        pupil.UserData.Email,
                        pupil.UserData.FirstName,
                        pupil.UserData.LastName)).ToList(),
                    x.Courses.Select(course => new CourseModel(
                        course.Id,
                        course.CourseName)).ToList()))
                .FirstOrDefaultAsync();

            if (classroom == null)
                return NotFound("Pupil not found");

            return Ok(classroom);
        }
        [HttpDelete("{classroomId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<ClassroomDetailModel>> DeleteClassroom(int classroomId)
        {
            var classroom = await _classRepository
                .FindWithTracking(x => x.Id == classroomId)
                .Include(x => x.Pupils)
                .FirstOrDefaultAsync();

            if (classroom == null)
                return NotFound("Pupil not found");

            if (classroom.Pupils.Count > 0)
                return BadRequest("Can't delete class with pupils");

            _classRepository.Remove(classroom);

            await _classRepository.SaveChangesAsync();

            return Ok(classroom);
        }
    }
}
