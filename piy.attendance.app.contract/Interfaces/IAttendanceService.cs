using piy.attendance.app.contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> MarkAsync(MarkAttendanceRequest req, CancellationToken ct = default);
        Task<IEnumerable<AttendanceDto>> GetByLectureAsync(Guid lectureId, CancellationToken ct = default);
        Task<PagedResult<AttendanceDto>> GetByStudentAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    }
}
