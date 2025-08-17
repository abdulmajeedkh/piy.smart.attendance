using Microsoft.EntityFrameworkCore;
using piy.attendance.app.contract.DTOs;
using piy.attendance.app.contract.Interfaces;
using piy.attendance.app.Mapping;
using piy.attendance.app.Utils;
using piy.attendance.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.Services
{

    public class AttendanceService(AppDbContext db, IQrTokenProvider qr) : IAttendanceService
    {
        public async Task<AttendanceDto> MarkAsync(MarkAttendanceRequest req, CancellationToken ct = default)
        {
            var lecture = await db.Lectures.FirstOrDefaultAsync(l => l.Id == req.LectureId, ct)
                          ?? throw new KeyNotFoundException("Lecture not found");

            var studentExists = await db.Students.AnyAsync(s => s.Id == req.StudentId, ct);
            if (!studentExists) throw new KeyNotFoundException("Student not found");

            // Validate QR token binds to this lecture/time window
            if (string.IsNullOrWhiteSpace(req.QrToken) || !qr.ValidateToken(req.QrToken, lecture.Id, out var startUtc, out var endUtc))
                throw new UnauthorizedAccessException("Invalid QR token");

            var now = DateTime.UtcNow;
            if (now < startUtc || now > endUtc)
                throw new InvalidOperationException("Attendance window closed");

            // Check duplicate
            var exists = await db.Attendances.AnyAsync(a => a.LectureId == req.LectureId && a.StudentId == req.StudentId, ct);
            if (exists) throw new InvalidOperationException("Attendance already marked");

            bool isValid = true;
            if (lecture.Latitude.HasValue && lecture.Longitude.HasValue && req.Latitude.HasValue && req.Longitude.HasValue)
            {
                var distance = Geo.DistanceMeters(lecture.Latitude.Value, lecture.Longitude.Value, req.Latitude.Value, req.Longitude.Value);
                isValid = distance <= lecture.RadiusMeters;
            }

            var attendance = new Attendance
            {
                LectureId = req.LectureId,
                StudentId = req.StudentId,
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                IsValid = isValid,
                MarkedAtUtc = now
            };

            db.Attendances.Add(attendance);
            await db.SaveChangesAsync(ct);
            return attendance.ToDto();
        }

        public async Task<IEnumerable<AttendanceDto>> GetByLectureAsync(Guid lectureId, CancellationToken ct = default)
        {
            var list = await db.Attendances.AsNoTracking()
                .Where(a => a.LectureId == lectureId)
                .OrderByDescending(a => a.MarkedAtUtc)
                .ToListAsync(ct);
            return list.Select(x => x.ToDto());
        }

        public async Task<PagedResult<AttendanceDto>> GetByStudentAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var query = db.Attendances.AsNoTracking().Where(a => a.StudentId == studentId);
            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(a => a.MarkedAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
            return new(items.Select(x => x.ToDto()), total, page, pageSize);
        }
    }

}
