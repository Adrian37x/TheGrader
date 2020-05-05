using System;
using System.Collections.Generic;
using System.Text;

namespace TheGrader
{ 
    class Semester
    {
        public string Name { get; set; } 
        public DateTime StartDate { get; set; }
        public bool Completed { get; set; } 

        public Semester(string name, DateTime startDate, bool completed)
        {
            Name = name;
            StartDate = startDate;
            Completed = completed;
        }
    }
}

