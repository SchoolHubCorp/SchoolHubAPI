﻿using SchoolHubApi.Models.Course;
using SchoolHubApi.Models.Pupil;

namespace SchoolHubApi.Models.Classroom
{
    public record ClassroomDetailModel
    (int Id,
        string ClassName,
        string ClassAccessCode,
        List<PupilModel> Pupils,
        List<CourseNameModel> Courses
        );
}
