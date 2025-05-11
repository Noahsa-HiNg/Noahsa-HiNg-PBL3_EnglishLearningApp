// ViewModel/UserProfileViewModel.cs
using DTO;
using BLL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input; // If using WPF Commands
using System;

namespace GUI.ViewModel
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private User_AccountManagementBLL _bll;

        private Account _account;
        public Account CurrentAccount
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private Customer _customer;
        public Customer CurrentCustomer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CourseCategory> _registeredCourses;
        public ObservableCollection<CourseCategory> RegisteredCourses
        {
            get => _registeredCourses;
            set
            {
                _registeredCourses = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CourseCategory> _favoriteCourses;
        public ObservableCollection<CourseCategory> FavoriteCourses
        {
            get => _favoriteCourses;
            set
            {
                _favoriteCourses = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Lesson> _lessonHistory;
        public ObservableCollection<Lesson> LessonHistory
        {
            get => _lessonHistory;
            set
            {
                _lessonHistory = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Lesson> _favoriteLessons;
        public ObservableCollection<Lesson> FavoriteLessons
        {
            get => _favoriteLessons;
            set
            {
                _favoriteLessons = value;
                OnPropertyChanged();
            }
        }

        // Command for loading data (example for UI interaction)
        public ICommand LoadProfileCommand { get; private set; }

        public UserProfileViewModel()
        {
            _bll = new User_AccountManagementBLL(); // Initialize BLL
            RegisteredCourses = new ObservableCollection<CourseCategory>();
            FavoriteCourses = new ObservableCollection<CourseCategory>();
            LessonHistory = new ObservableCollection<Lesson>();
            FavoriteLessons = new ObservableCollection<Lesson>();

            // Initialize command if using WPF Commands
            // LoadProfileCommand = new RelayCommand(ExecuteLoadProfile); // You'll need a RelayCommand implementation
        }

        // Example method to load data, typically called from the View's Loaded event or similar
        public void LoadUserProfile(string username)
        {
            UserProfileData data = _bll.GetUserProfileData(username);

            if (data != null)
            {
                CurrentAccount = data.Account;
                CurrentCustomer = data.Customer;

                RegisteredCourses.Clear();
                if (data.RegisteredCourses != null)
                {
                    foreach (var course in data.RegisteredCourses)
                    {
                        RegisteredCourses.Add(course);
                    }
                }

                FavoriteCourses.Clear();
                if (data.FavoriteCourses != null)
                {
                    foreach (var course in data.FavoriteCourses)
                    {
                        FavoriteCourses.Add(course);
                    }
                }

                LessonHistory.Clear();
                if (data.LessonHistory != null)
                {
                    foreach (var lesson in data.LessonHistory)
                    {
                        LessonHistory.Add(lesson);
                    }
                }

                FavoriteLessons.Clear();
                if (data.FavoriteLessons != null)
                {
                    foreach (var lesson in data.FavoriteLessons)
                    {
                        FavoriteLessons.Add(lesson);
                    }
                }
            }
            else
            {
                // Handle case where no user data is found or an error occurred
                Console.WriteLine($"User profile data for {username} not found or could not be loaded.");
                // Clear existing data in ViewModel
                CurrentAccount = null;
                CurrentCustomer = null;
                RegisteredCourses.Clear();
                FavoriteCourses.Clear();
                LessonHistory.Clear();
                FavoriteLessons.Clear();
            }
        }

        // Helper for WPF Commands if you decide to use them
        // private void ExecuteLoadProfile(object parameter)
        // {
        //     if (parameter is string username)
        //     {
        //         LoadUserProfile(username);
        //     }
        // }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // You might need a simple RelayCommand implementation for WPF binding if not using a framework that provides one
    /*
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
    }
    */
}