using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Repositories.Interface;
using SchoolHubApi.Models.Topic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SchoolHubApi.Repositories.Implementation;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly IMarkRepository _markRepository;

        public TopicController(ITopicRepository topicRepository, ITeacherRepository teacherRepository, ICourseRepository courseRepository, IClassRepository classRepository, IHomeworkRepository homeworkRepository, IMarkRepository markRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
            _classRepository = classRepository;
            _topicRepository = topicRepository;
            _homeworkRepository = homeworkRepository;
            _markRepository = markRepository;
        }

        [HttpPost, Auth(Role.Teacher)]
        public async Task<ActionResult<TopicModel>> CreateTopic([FromBody] TopicDto request)
        {
            var course = _courseRepository.Find(x => x.Id == request.CourseId).FirstOrDefault();
            if (course == null)
                return BadRequest("Course not found, cant add topic");

            if (_topicRepository.Find(x => x.TopicName == request.TopicName && x.Description ==request.Description).Any())
                return BadRequest("Topic name already exists");

            var topic = new Topic(request.TopicName)
            {
                CourseId = request.CourseId,
                Description =request.Description
            };

            _topicRepository.Add(topic);
            await _topicRepository.SaveChangesAsync();
            return new TopicModel(topic.Id, topic.TopicName,topic.Description);
        }
        [HttpPost("{topicId:int}/plan"), Auth(Role.Teacher)]
        public async Task<ActionResult> UploadTopicFile(int topicId, IFormFile file)
        {
            var topic = _topicRepository
                .FindWithTracking(x => x.Id == topicId)
                .FirstOrDefault();

            if (topic == null)
                return NotFound("Topic not found");

            if ((file.ContentType != "application/pdf") &&
               (file.ContentType != "application/msword") &&
               (file.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                    return BadRequest("Not propriate file type");

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    topic.TopicFileType = file.ContentType;
                    topic.TopicFile = stream.ToArray();
                    await _topicRepository.SaveChangesAsync();
                }
            }
            return Ok("File was uploaded successfully.");
        }
        [HttpDelete("{topicId:int}"), Auth(Role.Teacher)]
        public async Task<ActionResult<TopicModel>> DeleteTopic(int topicId)
        {
            var topic = await _topicRepository
                .FindWithTracking(x => x.Id == topicId)
                .Include(x => x.Homeworks)
                .ThenInclude(x => x.Mark)
                .FirstOrDefaultAsync();

            if (topic == null)
                return NotFound("Topic not found");

            foreach (var homework in topic.Homeworks)
            {
                var mark = homework.Mark;
                if (mark != null)
                {
                    _markRepository.Remove(mark);
                }
            }

            foreach (var homework in topic.Homeworks)
            {
                _homeworkRepository.Remove(homework);
            }

            _topicRepository.Remove(topic);

            await _topicRepository.SaveChangesAsync();

            return Ok("Topic was deleted successfully");
        }

        [HttpDelete("{topicId:int}/file"), Auth(Role.Teacher)]
        public async Task<ActionResult> DeleteTopicFile(int topicId)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var teacher = await _teacherRepository.Find(x => x.UserDataEmail == email).FirstOrDefaultAsync();
            if (teacher == null)
                return NotFound("Teacher not Found");
            var topic = await _topicRepository
                .FindWithTracking(x => x.Id == topicId)
                .FirstOrDefaultAsync();

            if (topic == null)
                return NotFound("Topic not found");

            topic.TopicFileType = null;
            topic.TopicFile = null;

            await _topicRepository.SaveChangesAsync();

            return Ok("File was deleted successfully.");
        }

    }
}
