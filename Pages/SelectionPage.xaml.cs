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
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        private ObservableCollection<Semester> semesters;
        private Semester selectedSemester;
        private Button selectedButton;

        public SelectionPage()
        {
            InitializeComponent();
            semesters = new ObservableCollection<Semester>();

            SemesterPanel.DataContext = semesters;
        }

        private void CreateSubject_Click(object sender, RoutedEventArgs e)
        {

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
            selectedButton = (Button)sender;
        }

        private void DeleteSemesterBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete " + selectedSemester.Name + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                semesters.Remove(selectedSemester);
                SemesterPanel.Children.Remove(selectedButton);
            }
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to complete " + selectedSemester.Name + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                selectedSemester.Completed = true;
                CompleteBtn.IsEnabled = false;
                CompleteBtn.Background = Brushes.LightGray;
            }
        }
    }
}
