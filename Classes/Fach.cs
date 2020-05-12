using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;

namespace TheGrader
{
    public class Fach  
    {
        #region properties
        public string Name { get; set; }
        public List<Exam> Exams { get; set; }
        public double Average { get; set; } 
        #endregion

        public Fach(string name)
        {
            this.Name = name;
        }

        #region methods
        private void CalculateAverage()
        {
            double summe = 0;
            foreach (var exam in Exams){
                summe = summe + exam.Grade;
            }

            Average = summe / Exams.Count;
        } 
        #endregion
    }
}
