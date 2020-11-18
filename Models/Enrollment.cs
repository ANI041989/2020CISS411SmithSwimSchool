using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CoachId { get; set; }
        public int LessonId { get; set; }
        public Coach Coach { get; set; }
        public Lesson Lesson { get; set; }
    }
    
}
