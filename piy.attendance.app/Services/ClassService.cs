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

    public class ClassService(AppDbContext db) : IClassService
    {
        public async Task<ClassDto> CreateAsync(ClassCreateDto dto, CancellationToken ct = default)
        {
            var entity = new Class
            {
                ClassName = dto.ClassName,
                Subject = dto.Subject
            };
            db.Classes.Add(entity);
            await db.SaveChangesAsync(ct);
            return entity.ToDto();
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync(CancellationToken ct = default)
            => (await db.Classes.AsNoTracking().ToListAsync(ct)).Select(x => x.ToDto());
    }

}
