using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Repositories.Interface;
using System.Security.Claims;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PupilController : ControllerBase
    {
        private readonly IPupilRepository _pupilRepository;
        private readonly IClassRepository _classRepository;

        public PupilController(IPupilRepository pupilRepository, IClassRepository classRepository)
        {
            _pupilRepository = pupilRepository;
            _classRepository = classRepository;
        }

        [HttpGet("{pupilId:int}/plan"), Auth(Role.Pupil)]
        public async Task<ActionResult> GetPupilPlanById(int pupilId)
        {
            var pupil = await _pupilRepository
                .Find(x => x.Id == pupilId)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            return RedirectToAction("GetClassImage", "classroom", new { classroomId = pupil.ClassroomId });
        }

        [HttpGet("plan"), Auth(Role.Pupil)]
        public async Task<ActionResult> GetPupilPlan()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var pupil = await _pupilRepository
                .Find(x => x.UserData.Email == email)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            return RedirectToAction("GetClassImage", "classroom", new { classroomId = pupil.ClassroomId });
        }

        [HttpPost("{pupilId:int}/changeClass"), Auth(Role.Admin)]
        public async Task<ActionResult> ChangePupilClass( int pupilId, [FromBody] int classroomId)
        {
            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            var classroom =  _classRepository
                .Find(x => x.Id == classroomId)
                .Any();

            if (!classroom)
                return NotFound("Maksim net takogo klasa dolbojeb:)");

            pupil.ClassroomId = classroomId;

            await _pupilRepository.SaveChangesAsync();

            return Ok(pupil);
        }

        [HttpDelete("{pupilId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult> DeletePupil(int pupilId)
        {
            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .Include(x => x.UserData)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            _pupilRepository.Remove(pupil);

            await _pupilRepository.SaveChangesAsync();

            return Ok($"Pupil {pupil.UserData.Email}, {pupil.UserData.FirstName} {pupil.UserData.LastName} was delated");
        }
    }
}
