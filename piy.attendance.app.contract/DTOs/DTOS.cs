using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.DTOs
{
    public record StudentCreateDto(string FullName, string Email, string EnrollmentNumber, Guid ClassId);
    public record StudentDto(Guid Id, string FullName, string Email, string EnrollmentNumber, Guid ClassId);

    public record ClassCreateDto(string ClassName, string Subject);
    public record ClassDto(Guid Id, string ClassName, string Subject);

    public record LectureCreateDto(
        Guid ClassId,
        string Topic,
        DateTime StartTimeUtc,
        DateTime EndTimeUtc,
        decimal? Latitude,
        decimal? Longitude,
        int RadiusMeters
    );

    public record LectureDto(
        Guid Id,
        Guid ClassId,
        string Topic,
        DateTime StartTimeUtc,
        DateTime EndTimeUtc,
        decimal? Latitude,
        decimal? Longitude,
        int RadiusMeters,
        string? QrToken
    );

    public record MarkAttendanceRequest(Guid LectureId, Guid StudentId, decimal? Latitude, decimal? Longitude, string QrToken);

    public record AttendanceDto(Guid Id, Guid LectureId, Guid StudentId, DateTime MarkedAtUtc, decimal? Latitude, decimal? Longitude, bool IsValid);

    public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);

}
