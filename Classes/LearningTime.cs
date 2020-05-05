using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrader
{
    public class LearningTime
    {
        #region properties
        public DateTime Date { get; set; }
        public double SpentMinutes { get; set; }
        public Fach Subject { get; set; }
        #endregion

        #region constructor
        public LearningTime(DateTime date, double spentMinutes, Fach subject)
        {
            this.Date = date;
            this.SpentMinutes = spentMinutes;
            this.Subject = subject;
        } 
        #endregion
    }
}
