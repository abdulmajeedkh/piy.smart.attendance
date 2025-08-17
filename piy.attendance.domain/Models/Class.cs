using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.domain.Models
{
    public class Class : BaseEntity
    {
        public string ClassName { get; set; } = default!; // e.g., BS-CS-5A
        public string Subject { get; set; } = default!;

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
