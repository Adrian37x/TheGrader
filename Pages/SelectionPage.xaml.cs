using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TheGrader.Pages
{
    /// <summary>
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        private ObservableCollection<Semester> semesters;

        private Semester selectedSemester;
        private Button semesterButton;

        private Fach selectedFach;
        private Button fachButton;

        public SelectionPage()
        {
            InitializeComponent();
            semesters = new ObservableCollection<Semester> {
                new Semester("1.1 Lehrjahr", DateTime.Now, false)
            };

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
            }
            else
            {
                MessageBox.Show("Fill in all fields (name, start date)");
            }
        }

        private void OnSemesterBtn_Click(object sender, RoutedEventArgs e, Semester semester)
        {
            selectedSemester = semester;
            DeleteSemesterBtn.IsEnabled = true;
            DeleteSemesterBtn.ClearValue(BackgroundProperty);
            if (!semester.Completed)
            {
                CompleteBtn.IsEnabled = true;
                CompleteBtn.ClearValue(BackgroundProperty);
            }
            else
            {
                CompleteBtn.IsEnabled = false;
                CompleteBtn.Background = Brushes.LightGray;
            }
            semesterButton = (Button)sender;
            DisplayFaecher(semester);
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
    }
}
