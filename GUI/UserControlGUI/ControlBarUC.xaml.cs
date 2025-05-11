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
using GUI.ViewModel;

namespace GUI.UserControlGUI
{
    /// <summary> 
    /// Interaction logic for ControlBarUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {
        public ControlBarViewModel Viewmodel { get; set; }
        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new ControlBarViewModel();
        }
        void GetWindowParent() { 
        }
        private void Bell_icon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void Shoping_card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mylesson_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MyCourse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
