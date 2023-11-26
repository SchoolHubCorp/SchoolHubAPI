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
using SchoolHubApi.Models.UserDto;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.Course;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Models.Topic;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PupilController : ControllerBase
    {
        private readonly IPupilRepository _pupilRepository;
        private readonly IClassRepository _classRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITopicRepository _topicRepository;

        public PupilController(IPupilRepository pupilRepository, IClassRepository classRepository, IParentRepository parentRepository, IUserRepository userRepository, ICourseRepository courseRepository, ITopicRepository topicRepository)
        {
            _pupilRepository = pupilRepository;
            _classRepository = classRepository;
            _parentRepository = parentRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _topicRepository = topicRepository;
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
            _userRepository.Remove(pupil.UserData);
            await _pupilRepository.SaveChangesAsync();
            await _userRepository.SaveChangesAsync();
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
                    x.Classroom.ClassName)).ToListAsync(); 
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
                    x.UserData.LastName,
                    x.UserData.PhoneNumber,
                    x.UserData.Pesel,
                    x.AccessCode,
                    x.Classroom.ClassName,
                    x.Classroom.Id,
                    x.Parents.Select(parent => new ParentModel(
                        parent.Id,
                        parent.UserData.FirstName,
                        parent.UserData.LastName,
                        parent.UserData.PhoneNumber)).ToList()))
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            return Ok(pupil);
        }
        [HttpGet("pupilCourses"), Auth(Role.Pupil)]
        public async Task<ActionResult<List<CourseTeacherModel>>> GetPupilCourses()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);

            var pupil = await _pupilRepository
                .Find(x => x.UserDataEmail == email)
                .Include(t => t.Classroom.Courses)
                .ThenInclude(c => c.Teacher)
                .ThenInclude(u => u.UserData)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("pupil not found");

            if (pupil.Classroom.Courses == null || pupil.Classroom.Courses.Count == 0)
                return NotFound("Teacher doesn't have courses");

            return pupil.Classroom.Courses.Select(x => new CourseTeacherModel(x.Id, x.CourseName, x.Teacher.UserData.FirstName,x.Teacher.UserData.LastName)).ToList();
        }
        [HttpGet("{courseId:int}/Topics"), Auth(Role.Pupil)]
        public async Task<ActionResult<List<CourseTopicModel>>> GetPupilTopics(int courseId)
        {
            var course = await _courseRepository
                .Find(x => x.Id == courseId)
                .Include(t => t.Topic)
                .Select(x => new CourseTopicModel(
                    x.Id,
                    x.CourseName,
                    x.Topic.Select(topic => new TopicModel(
                        topic.Id,
                        topic.TopicName,
                        topic.Description)).ToList()))
                .FirstOrDefaultAsync();

            if (course == null)
                return NotFound("Teacher not found");

            if (course.Topics == null || course.Topics.Count == 0)
                return NotFound("Course doesn't have topics");

            return Ok(course);
        }
        [HttpPut("{pupilId:int}"), Auth(Role.Admin)]
        public async Task<ActionResult<PupilShortModel>> UpdatePupilInfo(int pupilId, [FromBody] PupilDetailUpdateModel request)
        {
            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .Include(x => x.UserData)
                .Include(x => x.Classroom)
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            pupil.UserData.FirstName = request.FirstName;
            pupil.UserData.LastName = request.LastName;
            pupil.UserData.PhoneNumber = request.PhoneNumber;
            pupil.UserData.Pesel = request.Pesel;
           
            await _pupilRepository.SaveChangesAsync();

            return new PupilShortModel(pupil.Id, pupil.UserData.FirstName, pupil.UserData.LastName, pupil.Classroom.ClassName); 
        }
        [HttpPut("{pupilId:int}/changePupilClass"), Auth(Role.Admin)]
        public async Task<ActionResult<PupilShortModel>> UpdatePupilClass(int pupilId, [FromBody] PupilClassroomModel request)
        {
            var pupil = await _pupilRepository
                .FindWithTracking(x => x.Id == pupilId)
                .Include(x => x.UserData)
                .Include(x => x.Classroom) 
                .FirstOrDefaultAsync();

            if (pupil == null)
                return NotFound("Pupil not found");

            var classroom = await _classRepository.FindWithTracking(x => x.Id == request.ClassroomId).FirstOrDefaultAsync();

            if (classroom == null)
                return BadRequest("Classroom not found");

            pupil.ClassroomId = request.ClassroomId;

            await _pupilRepository.SaveChangesAsync();
            return new PupilShortModel(pupil.Id, pupil.UserData.FirstName, pupil.UserData.LastName, pupil.Classroom.ClassName);
        }
    }
}
