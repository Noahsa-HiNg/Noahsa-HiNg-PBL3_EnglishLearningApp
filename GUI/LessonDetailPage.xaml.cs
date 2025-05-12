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
    /// Interaction logic for LessonDetailPage.xaml
    /// </summary>
    public partial class LessonDetailPage : Page
    {
        public LessonDetailPage(Lesson lesson)
        {
            InitializeComponent();
            DataContext = lesson; // Gán dữ liệu của Lesson vào DataContext
        }

        private void PlayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoPlayer.Play();
        }

        private void PauseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoPlayer.Pause();
        }

        private void StopButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoPlayer.Stop();
        }
    }
}
