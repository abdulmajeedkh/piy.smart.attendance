using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.contract.Interfaces
{
    // Abstractions for crypto/qr (to keep host free of implementation details)
    public interface IQrTokenProvider
    {
        string GenerateToken(Guid lectureId, DateTime startUtc, DateTime endUtc);
        bool ValidateToken(string token, Guid lectureId, out DateTime startUtc, out DateTime endUtc);
    }
}
