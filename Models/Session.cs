using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public int SeatCapacity { get; set; }
        public TimeSpan DailyStartTime { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
