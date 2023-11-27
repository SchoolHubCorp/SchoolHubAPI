using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.Topic;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Models.Pupil;
using System.Linq;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IPupilRepository _pupilRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ITopicRepository _topicRepository;
        public HomeworkController(IHomeworkRepository homeWorkRepository, ITopicRepository topicRepository, IPupilRepository pupilRepository)
        {
            _homeworkRepository = homeWorkRepository;
            _topicRepository = topicRepository;
            _pupilRepository = pupilRepository;
        }

        [HttpPost("{topicId:int}/submit-homework"), Auth(Role.Pupil)]
        public async Task<ActionResult> UploadHomeworkFile(int topicId, IFormFile file)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var pupil = await _pupilRepository
                .Find(x => x.UserData.Email == email)
                .FirstOrDefaultAsync();
            if (pupil == null)
                return NotFound("Pupil not found");

            var topic = _topicRepository
                .FindWithTracking(x => x.Id == topicId)
                .Include(x => x.Homeworks)
                .FirstOrDefault();

            if (topic == null)
                return NotFound("Topic not found");

            if ((file.ContentType != "application/pdf") &&
               (file.ContentType != "application/msword") &&
               (file.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                return BadRequest("Inappropriate file type");

            var newHomework = new Homework
            {
                HomeworkFile = new byte[0],
                HomeworkFileType = file.ContentType,
                PupilId = pupil.Id,
            };

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    newHomework.HomeworkFile = stream.ToArray();
                }
            }
            topic.Homeworks.Add(newHomework);
            await _homeworkRepository.SaveChangesAsync();

            return Ok("Homework was submitted successfully.");
        }
        [HttpDelete("{homeworkId:int}/deleteРomework"), Auth(Role.Pupil)]
        public async Task<ActionResult> DeleteHomeworkFile(int homeworkId)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var pupil = await _pupilRepository.Find(x => x.UserDataEmail == email).FirstOrDefaultAsync();
            if (pupil == null)
                return NotFound("Pupil not Found");
            var homework = await _homeworkRepository
                .FindWithTracking(x => x.Id == homeworkId)
                .FirstOrDefaultAsync();

            if (homework == null)
                return NotFound("Homework not found");

            homework.HomeworkFile = null;
            homework.HomeworkFileType = null;

            await _homeworkRepository.SaveChangesAsync();

            return Ok("Homework file content was deleted successfully");
        }

        [HttpGet("{topicId:int}/Homeworks"), Auth(Role.Teacher)]
        public async Task<ActionResult<List<PupilHomeworkModel>>> CheckHomework(int topicId)
        {
            var pupilsWithHomework = await _pupilRepository
                 .Find(p => p.Classroom.Courses.Any(s => s.Topic.Any(t => t.Id == topicId)))
                 .Include(p => p.UserData)
                 .Include(p => p.Homeworks)
                 .ThenInclude(x =>x.Mark)
                 .ToListAsync();

            var result = pupilsWithHomework.Select(pupil => new PupilHomeworkModel(
                pupil.Id,
                pupil.UserData.FirstName,
                pupil.UserData.LastName,
                pupil.Homeworks.FirstOrDefault(h => h.TopicId == topicId)?.Id,
                pupil.Homeworks.FirstOrDefault(h => h.TopicId == topicId)?
                    .HomeworkFile,
                pupil.Homeworks?
                    .FirstOrDefault(h => h.TopicId == topicId)?
                    .Mark?.MarkName
            ));

            return Ok(result);
        }


    }
}
