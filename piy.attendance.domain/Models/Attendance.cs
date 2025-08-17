using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.domain.Models
{
    public class Attendance : BaseEntity
    {
        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; } = default!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public DateTime MarkedAtUtc { get; set; } = DateTime.UtcNow;

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public bool IsValid { get; set; } = false;
    }

}
