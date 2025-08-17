using piy.attendance.app.contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
        Task<IEnumerable<StudentDto>> GetAllAsync(CancellationToken ct = default);
        Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
