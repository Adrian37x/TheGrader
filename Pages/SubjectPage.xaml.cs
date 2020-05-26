using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheGrader.Pages
{
    /// <summary>
    /// Interaction logic for SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Page
    {
        public Fach Fach { get; set; }

        //public ObservableCollection<Exam> Exams { get; set; } = new ObservableCollection<Exam> { 
        //    new Exam("File 1", 134, 155, 1, DateTime.Now),
        //    new Exam("Pollution Presentation", 14, 20, .3f, DateTime.Now),
        //    new Exam("Vocab 3", 29, 30, .5f, DateTime.Now),
        //    new Exam("File 2", 120, 148, 1, DateTime.Now),
        //};
        public List<Exam> Exams { get; set; }

        public Exam SelectedExam { get; set; }

        private Button selectedBtn;

        public SubjectPage(Fach fach)
        {
            InitializeComponent();

            this.Fach = fach;
            this.Exams = Fach.Exams;
            Title.DataContext = Fach;

            CreateEditPanel.DataContext = "";

            Exams.OrderBy(exam => exam.Date);
            foreach (Exam exam in Exams)
            {
                Button button = new Button();
                button.Content = exam.Name;
                button.Click += (s, ev) => SelectExam_Click(s, ev, exam);
                ExamPanel.Children.Add(button);
            }
        }

        public void SelectExam_Click(object sender, RoutedEventArgs e, Exam exam)
        {
            selectedBtn = (Button)sender;
            UpdateBtn.Visibility = Visibility.Visible;
            SelectedExam = exam;
            NameBox.Text = exam.Name;
            GradeBox.Text = exam.Grade.ToString();
            ValueBox.Text = exam.Weight.ToString();

            DeleteExamBtn.ClearValue(Button.BackgroundProperty);
            DeleteExamBtn.IsEnabled = true;
        }

        private void BackToSelectionPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new SelectionPage());
        }

        private void CreateExamBtn_Click(object sender, RoutedEventArgs e)
        {
            double grade = 0;
            double value = 0;

            if (
                double.TryParse(ValueBox.Text, out value) &&
                double.TryParse(GradeBox.Text, out grade) &&
                !string.IsNullOrEmpty(NameBox.Text) &&
                !(grade > 6 || grade < 1 || value > 100 || value < 0)
                )
            {
                Exam exam = new Exam(NameBox.Text, value, grade);
                Fach.Exams.Add(exam);
                Button button = new Button
                {
                    Content = exam.Name
                };
                button.Click += (s, eg) => SelectExam_Click(s, eg, exam);
                ExamPanel.Children.Add(button);

                ValueBox.Text = "";
                GradeBox.Text = "";
                NameBox.Text = "";
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            double grade = 0;
            double value = 0;

            if (
                double.TryParse(ValueBox.Text, out value) &&
                double.TryParse(GradeBox.Text, out grade) &&
                !string.IsNullOrEmpty(NameBox.Text) &&
                !(grade > 6 || grade < 1 || value > 100 || value < 0)
                )
            {
                SelectedExam.Name = NameBox.Text;
                SelectedExam.Grade = grade;
                SelectedExam.Weight = value;

                selectedBtn.Content = NameBox.Text;
                UpdateBtn.Visibility = Visibility.Hidden;

                selectedBtn = null;
                DeleteExamBtn.Background = Brushes.LightGray;
                DeleteExamBtn.IsEnabled = false;

                ValueBox.Text = "";
                GradeBox.Text = "";
                NameBox.Text = "";
            }
        }

        private void DeleteExamBtn_Click(object sender, RoutedEventArgs e)
        {
            ExamPanel.Children.Remove(selectedBtn);
            selectedBtn = null;

            Fach.Exams.Remove(SelectedExam);

            DeleteExamBtn.Background = Brushes.LightGray;
            DeleteExamBtn.IsEnabled = false;

            UpdateBtn.Visibility = Visibility.Hidden;

            ValueBox.Text = "";
            GradeBox.Text = "";
            NameBox.Text = "";
        }
    }
}
