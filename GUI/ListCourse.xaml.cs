﻿using System;
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
using BLL;
using System.Collections.ObjectModel;
namespace GUI
{
    /// <summary>
    /// Interaction logic for ListCourse.xaml
    /// </summary>
    public partial class ListCourse : Page
    {
        public ObservableCollection<CourseCategory> Courses { get; set; }
        
        public ListCourse()
        {
            
           
            InitializeComponent();
            
        }
    }
}
