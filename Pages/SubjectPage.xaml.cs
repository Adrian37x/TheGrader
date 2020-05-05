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
        //public ObservableCollection<Exam> Moments { get; set; } = new ObservableCollection<Exam>();
        private Fach selectedFach;

        public SubjectPage(Fach fach)
        {
            InitializeComponent();

            selectedFach = fach;
            CreateEditPanel.DataContext = "öalksdjfasödf";
        }
    }
}
