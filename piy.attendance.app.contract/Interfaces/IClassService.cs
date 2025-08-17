using piy.attendance.app.contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.Interfaces
{
    public interface IClassService
    {
        Task<ClassDto> CreateAsync(ClassCreateDto dto, CancellationToken ct = default);
        Task<IEnumerable<ClassDto>> GetAllAsync(CancellationToken ct = default);
    }
}
