using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.domain.Models
{
    public class Lecture : BaseEntity
    {
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = default!;

        public string Topic { get; set; } = default!;
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }

        // Location (nullable => optional geofence)
        public decimal? Latitude { get; set; } // e.g., 31.5204
        public decimal? Longitude { get; set; } // e.g., 74.3587
        public int RadiusMeters { get; set; } = 50;

        // QR payload is a secure token bound to LectureId & time window
        public string? QrToken { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }

}
