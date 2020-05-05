using System;
using System.Collections.Generic;
using System.Text;

namespace TheGrader
{ 
    public class Semester
    {
        #region properties
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public bool Completed { get; set; } 
        public List<Subject> Subjects { get; set; } 
        #endregion

        #region constructor
        public Semester(string name, DateTime startDate, bool completed, List<Subject> subjects)
        {
            Name = name;
            StartDate = startDate;
            Completed = completed;
            Subjects = subjects;
        } 
        #endregion
    }
}

