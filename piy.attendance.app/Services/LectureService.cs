using Microsoft.EntityFrameworkCore;
using piy.attendance.app.contract.DTOs;
using piy.attendance.app.contract.Interfaces;
using piy.attendance.app.Mapping;
using piy.attendance.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.Services
{

    public class LectureService(AppDbContext db, IQrTokenProvider qr) : ILectureService
    {
        public async Task<LectureDto> CreateAsync(LectureCreateDto dto, CancellationToken ct = default)
        {
            // Validate times
            if (dto.EndTimeUtc <= dto.StartTimeUtc)
                throw new ArgumentException("EndTimeUtc must be greater than StartTimeUtc");

            // Validate class exists
            var exists = await db.Classes.AnyAsync(c => c.Id == dto.ClassId, ct);
            if (!exists) throw new KeyNotFoundException("Class not found");

            var lecture = new Lecture
            {
                ClassId = dto.ClassId,
                Topic = dto.Topic,
                StartTimeUtc = dto.StartTimeUtc,
                EndTimeUtc = dto.EndTimeUtc,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                RadiusMeters = dto.RadiusMeters
            };

            // Generate QR token
            lecture.QrToken = qr.GenerateToken(lecture.Id, lecture.StartTimeUtc, lecture.EndTimeUtc);
            // NOTE: We need Id before token if token embeds Id; EF sets Id on add. Workaround: assign Id now.
            lecture.Id = Guid.NewGuid();
            lecture.QrToken = qr.GenerateToken(lecture.Id, lecture.StartTimeUtc, lecture.EndTimeUtc);

            db.Lectures.Add(lecture);
            await db.SaveChangesAsync(ct);
            return lecture.ToDto();
        }

        public async Task<LectureDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => (await db.Lectures.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct))?.ToDto();
    }

}
