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
        public ObservableCollection<Exam> Exams { get; set; } = new ObservableCollection<Exam> { 
            new Exam("File 1", 134, 155, 1, DateTime.Now),
            new Exam("Pollution Presentation", 14, 20, .3f, DateTime.Now),
            new Exam("Vocab 3", 29, 30, .5f, DateTime.Now),
            new Exam("File 2", 120, 148, 1, DateTime.Now),
        };
        public Exam SelectedExam { get; set; }

        public SubjectPage()
        {
            InitializeComponent();

            CreateEditPanel.DataContext = "";

            Exams.OrderBy(exam => exam.Date);
            foreach (Exam exam in Exams)
            {
                Button button = new Button();
                button.Content = exam.Name;
                button.Background = Brushes.Gray;
                button.Click += (s, ev) => SelectExam_Click(s, ev, exam);
                ExamPanel.Children.Add(button);
            }
        }

        public void SelectExam_Click(object sender, RoutedEventArgs e, Exam exam)
        {
            CreateEditPanel.DataContext = "Edit";
            SelectedExam = exam;
            FormPanel.DataContext = SelectedExam;
        }
    }
}
