using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoggedIn { get; set; } = false;
        public MainViewModel() {
            LoginGUI login = new LoginGUI();

            //if(!IsLoggedIn)
            //{
            //    IsLoggedIn = true;
            //    login.ShowDialog();
            //}
            //else
            //{
            //    // Load the main application window
            //    MainWindow mainWindow = new MainWindow();
            //    mainWindow.Show();
            //}
            
        }
    }
}
