using Project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{ 
        public enum SessionTime
{
    Fall, Spring

}
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int SwimmerId { get; set; }
        public int SessionId { get; set; }

        public int LessonId { get; set; }
        public Lesson lesson { get; set; }
        [DisplayFormat(NullDisplayText = "No session")]
        public SessionTime? SessionTime { get; set; }
        public string ProgressReport { get; set; }
        public Swimmer Swimmer { get; set; }
        public Session Session { get; set; }
    }
    
}
