using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Models.Classroom;
using SchoolHubApi.Models.Pupil;
using SchoolHubApi.Models.Parent;
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
        private readonly IParentRepository _parentRepository;

        public PupilController(IPupilRepository pupilRepository, IClassRepository classRepository, IParentRepository parentRepository)
        {
            _pupilRepository = pupilRepository;
            _classRepository = classRepository;
            _parentRepository = parentRepository;
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
                return NotFound("Сlass not found");

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
        [HttpGet, Auth(Role.Admin)]
        public async Task<ActionResult<List<PupilShortModel>>> GetAllPupils()
        {
            return await _pupilRepository
                .GetAll()
                .OrderBy(x => x.UserData.FirstName)
                .Select(x => new PupilShortModel(
                    x.Id,
                    x.UserData.FirstName,
                    x.UserData.LastName,
                    x.Classroom.ClassName,
                    x.Classroom.Id)).ToListAsync(); 
        }

        [HttpGet("{pupilId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<PupilDetailModel>> GetPupilDetails(int pupilId)
        {
            var pupil = await _pupilRepository
                .Find(x => x.Id == pupilId)
                .Include(x => x.Parents)
                .Select(x => new PupilDetailModel(
                    x.Id,
                    x.UserData.Email,
                    x.UserData.FirstName,
                    x.UserData.FirstName,
                    x.UserData.PhoneNumber,
                    x.UserData.Pesel,
                    x.Classroom.ClassName,
                    x.Classroom.Id,
                    x.Parents.Select(pupil => new ParentModel(
                        pupil.Id,
                        pupil.UserData.Email,
                        pupil.UserData.FirstName,
                        pupil.UserData.LastName)).ToList()))
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            return Ok(pupil);
        }
    }
}
