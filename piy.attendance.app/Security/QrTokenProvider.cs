using Microsoft.Extensions.Configuration;
using piy.attendance.app.contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.Security
{
    public class QrTokenProvider : IQrTokenProvider
    {
        private readonly string _secret;

        public QrTokenProvider(IConfiguration config)
        {
            // Use the GetSection method to retrieve the value manually if GetValue is unavailable
            _secret = config.GetSection("Attendance:QrSecret")?.Value ?? "dev-secret-change";
        }

        public string GenerateToken(Guid lectureId, DateTime startUtc, DateTime endUtc)
        {
            var payload = $"{lectureId}|{startUtc:O}|{endUtc:O}";
            var signature = Sign(payload);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(payload + "|" + signature));
        }

        public bool ValidateToken(string token, Guid lectureId, out DateTime startUtc, out DateTime endUtc)
        {
            startUtc = default;
            endUtc = default;
            try
            {
                var raw = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = raw.Split('|');
                if (parts.Length != 4) return false;
                if (!Guid.TryParse(parts[0], out var lid) || lid != lectureId) return false;
                if (!DateTime.TryParse(parts[1], null, System.Globalization.DateTimeStyles.RoundtripKind, out startUtc)) return false;
                if (!DateTime.TryParse(parts[2], null, System.Globalization.DateTimeStyles.RoundtripKind, out endUtc)) return false;

                var signature = parts[3];
                var expected = Sign(string.Join('|', parts[0], parts[1], parts[2]));
                return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(signature), Encoding.UTF8.GetBytes(expected));
            }
            catch { return false; }
        }

        private string Sign(string payload)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToBase64String(hash);
        }
    }
}
