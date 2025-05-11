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
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for CourseDetailPage.xaml
    /// </summary>
    public partial class CourseDetailPage : Page
    {
        public CourseDetailPage(CourseCategory course)
        {
            InitializeComponent();
            this.DataContext = course; // Gán dữ liệu của Course vào DataContext
        }
    }
}
