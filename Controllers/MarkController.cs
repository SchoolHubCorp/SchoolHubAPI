using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Controllers.Attributes;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.Topic;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Models.Mark;
using SchoolHubApi.Models.Course;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly IMarkRepository _markRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ITopicRepository _topicRepository;
        public MarkController(IMarkRepository markRepository, IHomeworkRepository homeWorkRepository, ITopicRepository topicRepository)
        {
            _markRepository = markRepository;
            _homeworkRepository = homeWorkRepository;
            _topicRepository = topicRepository;
        }

        [HttpPost("{homeworkId:int}/placeMark"), Auth(Role.Teacher)]
        public async Task<ActionResult<MarkModel>> AddMark(int homeworkId, [FromBody] MarkDto request)
        {
            var homework = _homeworkRepository
                .FindWithTracking(x => x.Id == homeworkId)
                .Include(x => x.Topic)
                .FirstOrDefault();

            if (homework == null)
                return NotFound("Homework not found");

            var newMark = new Mark(request.MarkName)
            {
                HomeworkId = homeworkId,
            };

            _markRepository.Add(newMark);
            await _markRepository.SaveChangesAsync();
            return new MarkModel(newMark.MarkName, newMark.Homework.Id, newMark.Homework.Topic.TopicName);
        }
    }
}
