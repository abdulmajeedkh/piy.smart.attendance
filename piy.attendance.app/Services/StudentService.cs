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
    public class StudentService(AppDbContext db) : IStudentService
    {
        public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
        {
            var entity = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                EnrollmentNumber = dto.EnrollmentNumber,
                ClassId = dto.ClassId
            };
            db.Students.Add(entity);
            await db.SaveChangesAsync(ct);
            return entity.ToDto();
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(CancellationToken ct = default)
            => (await db.Students.AsNoTracking().ToListAsync(ct)).Select(x => x.ToDto());

        public async Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => (await db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct))?.ToDto();
    }

}
