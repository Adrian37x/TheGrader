using System;
using System.Collections.Generic;
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
using TheGrader;

namespace TheGrader.Pages
{
    /// <summary>
    /// Interaction logic for TimestampPage.xaml
    /// </summary>
    public partial class TimestampPage : Page
    {
        private Fach selectedFach;

        public TimestampPage(Fach fach)
        {
            InitializeComponent();
            selectedFach = fach;
            CurrFachLabel.Content = "Fach: " + fach.Name;
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetContent(new SubjectPage(selectedFach));
        }
    }
}
