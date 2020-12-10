using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModels
{
    public class SessionAddSessionViewModel
    {
        public ApplicationUser User { get; set; }

        public Lesson Lesson { get; set; }

        public Session Session { get; set; }


    }
}
