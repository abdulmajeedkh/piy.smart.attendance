using piy.attendance.app.contract.DTOs;
using piy.attendance.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.Mapping
{
    public static class MappingExtensions
    {
        public static StudentDto ToDto(this Student s) => new(s.Id, s.FullName, s.Email, s.EnrollmentNumber, s.ClassId);
        public static ClassDto ToDto(this Class c) => new(c.Id, c.ClassName, c.Subject);
        public static LectureDto ToDto(this Lecture l) => new(l.Id, l.ClassId, l.Topic, l.StartTimeUtc, l.EndTimeUtc, l.Latitude, l.Longitude, l.RadiusMeters, l.QrToken);
        public static AttendanceDto ToDto(this Attendance a) => new(a.Id, a.LectureId, a.StudentId, a.MarkedAtUtc, a.Latitude, a.Longitude, a.IsValid);
    }

}
