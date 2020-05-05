using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrader
{
    class Exam
    {
        #region properties
        public double Grade { get; set; }
        public double PointsScored { get; set; }
        public double MaxPoints { get; set; }
        public double Weight { get; set; }
        #endregion

        #region constructor
        public Exam(double pointsScored, double maxPoints, double weight, double grade = 0)
        {
            PointsScored = pointsScored;
            MaxPoints = maxPoints;
            Weight = weight;
            if (grade != 0){
                CalculateGrade();
            }
            else{
                Grade = grade;
            }
        }
        #endregion

        #region methods
        private void CalculateGrade()
        {
            Grade = PointsScored * 5 / MaxPoints + 1;
        } 
        #endregion
    }
}
