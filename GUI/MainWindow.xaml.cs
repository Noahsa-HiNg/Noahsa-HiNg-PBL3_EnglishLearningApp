using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Main.Content = new ListCourse();
        } 
    }
}
