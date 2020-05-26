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

        #region constructor
        public Fach(string name)
        {
            this.Name = name;
            this.Exams = new List<Exam>();
            CalculateAverage();
        } 

        // needed for xml serialization
        public Fach()
        {

        }
        #endregion

        #region methods
        /// <summary>
        /// add new exam
        /// </summary>
        /// <param name="exam"></param>
        public void AddExam(Exam exam)
        {
            Exams.Add(exam);
        }

        /// <summary>
        /// calculate average grade in this subject (Fach)
        /// </summary>
        private void CalculateAverage()
        {
            double summe = 0;
            foreach (var exam in Exams){
                summe = summe + exam.Grade;
            }

            Average = summe / Exams.Count;
        } 

        /// <summary>
        /// calculate needed grade on final exam to get your wish grade
        /// </summary>
        /// <returns></returns>
        public double CalculateNWishGrade(double TargetGrade)
        {
            CalculateAverage();
            double currentGrade = Average;
            double targetGrade = TargetGrade;
            double finalExamGrade;
            finalExamGrade = targetGrade * 2 / currentGrade;
            return finalExamGrade;
        }
        #endregion
    }
}
