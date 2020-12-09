using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string SkillLevel { get; set; }
        public double Tuition { get; set; }
        public int CoachId { get; set; }
        public  Coach coach { get; set; }
        
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
