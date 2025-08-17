using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.domain.Models
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string EnrollmentNumber { get; set; } = default!; // unique

        public Guid ClassId { get; set; }
        public Class Class { get; set; } = default!;

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }

}
