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
using System.Security.Claims;
using SchoolHubApi.Models.Pupil;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly IMarkRepository _markRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IPupilRepository _pupilRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        public MarkController(IMarkRepository markRepository, IHomeworkRepository homeWorkRepository, ITopicRepository topicRepository, IPupilRepository pupilRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _markRepository = markRepository;
            _homeworkRepository = homeWorkRepository;
            _topicRepository = topicRepository;
            _pupilRepository = pupilRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpPost("{homeworkId:int}/placeMark"), Auth(Role.Teacher)]
        public async Task<ActionResult<MarkModel>> AddMark(int homeworkId, int pupilId, [FromBody] MarkDto request)
        {
            var homework = _homeworkRepository
                .FindWithTracking(x => x.Id == homeworkId)
                .Include(x => x.Topic)
                .FirstOrDefault();

            if (homework == null)
                return NotFound("Homework not found");

            var pupil = _pupilRepository.FindWithTracking(x => x.Id == pupilId).FirstOrDefault();

            if (pupil == null)
                return NotFound("Pupil not found");

            var newMark = new Mark(request.MarkName)
            {
                HomeworkId = homeworkId,
                PupilId = pupilId,
            };

            _markRepository.Add(newMark);
            await _markRepository.SaveChangesAsync();

            return new MarkModel(newMark.MarkName, newMark.Homework.Id, newMark.Homework.Topic.TopicName);
        }
        [HttpGet("{pupilId:int}/studentMarksAdmin"), Auth(Role.Admin)]
        public async Task<ActionResult<IEnumerable<CourseWithMarks>>> GetStudentMarks(int pupilId)
        {
            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .Include(p => p.Marks)
                    .ThenInclude(m => m.Homework)
                        .ThenInclude(h => h.Topic)
                            .ThenInclude(t => t.Course)
                                .ThenInclude(c => c.Classroom)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            var coursesWithMarks = pupil.Marks
                .GroupBy(mark => mark.Homework.Topic.Course)
                .OrderBy(group => group.Key.CourseName)
                .Select(group => new CourseWithMarks
                (
                    CourseName: group.Key.CourseName,
                    Marks: group
                        .Select(mark => new MarkDto(mark.MarkName))
                        .ToList()
                ))
                .ToList();

            return Ok(coursesWithMarks);
        }
        [HttpGet("{pupilId:int}/studentMarksPupil"), Auth(Role.Pupil)]
        public async Task<ActionResult<IEnumerable<CourseWithMarks>>> GetStudentsMarks(int pupilId)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var pupil = await _pupilRepository
                .Find(x => x.UserDataEmail == email)
                .Include(p => p.Marks)
                    .ThenInclude(m => m.Homework)
                        .ThenInclude(h => h.Topic)
                            .ThenInclude(t => t.Course)
                                .ThenInclude(c => c.Classroom)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            var coursesWithMarks = pupil.Marks
                .GroupBy(mark => mark.Homework.Topic.Course)
                .OrderBy(group => group.Key.CourseName)
                .Select(group => new CourseWithMarks
                (
                   group.Key.CourseName,
                   group.Select(mark => new MarkDto(mark.MarkName))
                        .ToList()
                ))
                .ToList();

            return Ok(coursesWithMarks);
        }
        [HttpGet("{pupilId:int}/studentMarksTeacher"), Auth(Role.Teacher)]
        public async Task<ActionResult<IEnumerable<PupilWithMarks>>> GetStudentMarksTeacher(int pupilId)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var teacher = await _teacherRepository.Find(x => x.UserDataEmail == email).FirstOrDefaultAsync();

            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .Include(p => p.Marks)
                    .ThenInclude(m => m.Homework)
                        .ThenInclude(h => h.Topic)
                            .ThenInclude(t => t.Course)
                                .ThenInclude(c => c.Classroom)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            var pupilWithMarksList = pupil.Marks
                .GroupBy(mark => mark.Homework.Topic.Course)
                .OrderBy(group => group.Key.CourseName)
                .Select(group => new PupilWithMarks
                (
                    PupilName: pupil.UserData.FirstName,
                    PupilSurname: pupil.UserData.LastName,
                    Marks: group
                        .Select(mark => new MarkDto(mark.MarkName))
                        .ToList()
                ))
                .ToList();

            return Ok(pupilWithMarksList);
        }

    }
}
