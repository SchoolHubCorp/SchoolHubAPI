using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.Classroom;
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

        [HttpGet, Authorize(Roles = nameof(Role.Admin))]
        public async Task<ActionResult<List<ClassroomModel>>> GetAllClassrooms()
        {
            return await _classRepository
                .GetAll()
                .OrderBy(x => x.ClassName)
                .Select(x => new ClassroomModel(x.Id, x.ClassName))
                .ToListAsync();
        }

        [HttpGet("/{classroomId:int}/image")]
        public async Task<ActionResult> GetClassImage(int classroomId)
        {
            var classroom = await _classRepository
                .Find(x => x.Id == classroomId)
                .FirstOrDefaultAsync();

            if (classroom == null)
                return NotFound("Classroom not found");

            return File(classroom.Plan, classroom.PlanContentType);
        }

        [HttpPost("/{classroomId:int}"), Authorize(Roles = nameof(Role.Admin))]
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
            var classroom = new Classroom(request.ClassName)
            {
                ClassAccessCode = AccessCodeGenerator.GenerateAccessCode(code => _classRepository.Find(x => x.ClassAccessCode == code).Any())
            };
            _classRepository.Add(classroom);
            await _classRepository.SaveChangesAsync();
            return new ClassroomModel(classroom.Id, classroom.ClassName);
        }
    }
}
