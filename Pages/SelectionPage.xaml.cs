using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TheGrader.Pages
{
    /// <summary>
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        private static ObservableCollection<Semester> semesters = new ObservableCollection<Semester>();

        private Semester selectedSemester;
        private Button semesterButton;

        private Fach selectedFach;
        private Button fachButton;

        public SelectionPage()
        {
            InitializeComponent();
            LoadSemesters();

            /*semesters = new ObservableCollection<Semester> {
                new Semester("1.1 Lehrjahr", DateTime.Now, false),
                new Semester("1.2 Lehrjahr", DateTime.Now, false)
            };
            semesters[0].Faecher.Add(new Fach("Maths"));
            semesters[0].Faecher.Add(new Fach("German"));
            semesters[1].Faecher.Add(new Fach("History"));

            semesters[0].Faecher[0].Exams.Add(new Exam("Exponential functions", 30, 45, 1f, DateTime.Now));
            semesters[0].Faecher[0].Exams.Add(new Exam("Repetition trigonometrie", 8, 15, .5f, DateTime.Now));
            semesters[0].Faecher[1].Exams.Add(new Exam("Aufsatz: Was bin ich", 42, 50, 1f, DateTime.Now));
            semesters[1].Faecher[0].Exams.Add(new Exam("Absolutism", 19, 24, .5f, DateTime.Now));
            semesters[1].Faecher[0].Exams.Add(new Exam("Second World War", 16, 22, .5f, DateTime.Now));*/

            DisplaySemesters();
        }

        private void DisplaySemesters()
        {
            foreach (Semester semester in semesters)
            {
                Button button = new Button
                {
                    Content = semester.Name,
                };
                SemesterPanel.Children.Add(button);
                button.Click += (s, ev) => OnSemesterBtn_Click(s, ev, semester);
            }
        }

        private void CreateSubject_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SubjectNameBox.Text) && selectedSemester != null)
            {
                selectedSemester.Faecher.Add(new Fach(SubjectNameBox.Text));
                SubjectNameBox.Text = null;
                DisplayFaecher(selectedSemester);
                SaveSemesters();
            }
            else
            {
                if (selectedSemester == null)
                {
                    MessageBox.Show("Select a semester before creating a subject");
                }
                else
                {
                    MessageBox.Show("Fill in all fields (name)");
                }
            }
        }

        private void CreateSemesterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SemesterNameBox.Text) && StartDatePicker.SelectedDate.HasValue)
            {
                Semester semester = new Semester(SemesterNameBox.Text, StartDatePicker.SelectedDate ?? DateTime.Now, false);
                semesters.Add(semester);
                Button button = new Button
                {
                    Content = SemesterNameBox.Text,
                };
                SemesterPanel.Children.Add(button);
                button.Click += (s, ev) => OnSemesterBtn_Click(s, ev, semester);
                SemesterNameBox.Text = "";
                StartDatePicker.SelectedDate = null;
                SaveSemesters();
            }
            else
            {
                MessageBox.Show("Fill in all fields (name, start date)");
            }
        }

        private void OnSemesterBtn_Click(object sender, RoutedEventArgs e, Semester semester)
        {
            if (selectedSemester == semester)
            {
                selectedSemester = null;

                DeleteSemesterBtn.Visibility = Visibility.Collapsed;
                CompleteBtn.Visibility = Visibility.Collapsed;
                SemesterForm.Visibility = Visibility.Visible;
                CreateSemesterBtn.Visibility = Visibility.Visible;

                CreateSubject.IsEnabled = false;
                CreateSubject.Background = Brushes.LightGray;

                SubjectPanel.Children.Clear();
            }
            else
            {
                selectedSemester = semester;

                SemesterForm.Visibility = Visibility.Collapsed;
                CreateSemesterBtn.Visibility = Visibility.Collapsed;
                DeleteSemesterBtn.Visibility = Visibility.Visible;
                CompleteBtn.Visibility = Visibility.Visible;

                CreateSubject.IsEnabled = true;
                CreateSubject.ClearValue(BackgroundProperty);

                semesterButton = (Button)sender;
                DisplayFaecher(semester);
            }
        }

        private void DeleteSemesterBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete " + selectedSemester.Name + "?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                semesters.Remove(selectedSemester);
                SemesterPanel.Children.Remove(semesterButton);
            }
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to complete " + selectedSemester.Name + "?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                selectedSemester.Completed = true;
                CompleteBtn.IsEnabled = false;
                CompleteBtn.Background = Brushes.LightGray;
                SaveSemesters();
            }
        }

        private void DisplayFaecher(Semester semester)
        {
            SubjectPanel.Children.Clear();
            foreach (Fach fach in semester.Faecher)
            {
                Button btn = new Button
                {
                    Content = fach.Name
                };
                btn.Click += (s, ev) => GoToFachPage(fach);
                SubjectPanel.Children.Add(btn);
            }
        }

        private void GoToFachPage(Fach fach)
        {
            MainWindow.SetContent(new SubjectPage(fach));
        }

        #region XML Serialization Methods
        private void SaveSemesters()
        {
            if (!File.Exists("data.xml"))
            {
                FileStream fs = File.Create("data.xml");
            }
            else
            {
                StreamWriter filestream = new StreamWriter("data.xml");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Semester>));
                xmlSerializer.Serialize(filestream, semesters);
                filestream.Close();
            }
        }

        private void LoadSemesters()
        {
            if (!File.Exists("data.xml"))
            {
                FileStream fs = File.Create("data.xml");
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Semester>));
                using (FileStream stream = File.OpenRead("data.xml"))
                {
                    semesters = (ObservableCollection<Semester>)serializer.Deserialize(stream);
                }
            }
        } 
        #endregion
    }
}
