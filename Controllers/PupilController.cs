using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PupilController : ControllerBase
    {
        private readonly IPupilRepository _pupilRepository;

        public PupilController(IPupilRepository pupilRepository)
        {
            _pupilRepository = pupilRepository;
        }

        [HttpGet("{pupilId:int}/plan"), Auth(Role.Pupil, Role.Teacher)]
        public async Task<ActionResult> GetClassImage(int pupilId)
        {
            var pupil = await _pupilRepository
                .Find(x => x.Id == pupilId)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            return RedirectToAction("GetClassImage", "classroom", new { classroomId = pupil.ClassroomId });
        }
    }
}
