using piy.attendance.app.contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.Interfaces
{
    public interface ILectureService
    {
        Task<LectureDto> CreateAsync(LectureCreateDto dto, CancellationToken ct = default);
        Task<LectureDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
