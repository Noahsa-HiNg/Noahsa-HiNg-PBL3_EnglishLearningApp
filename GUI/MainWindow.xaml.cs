using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            LoginGUI loginGUI = new LoginGUI();
            loginGUI.Show();
            this.Close();
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Shoping_card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bell_icon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyCourse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Mylesson_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}