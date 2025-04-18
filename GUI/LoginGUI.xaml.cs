﻿using System;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;
using DTO;

namespace GUI
{
    public partial class LoginGUI : Window
    {
        public string ID_user;

        public LoginGUI()
        {
            InitializeComponent();

            // Đặt focus vào ô username khi khởi động
            txtUsername.Focus();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            AttemptLogin();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AttemptLogin();
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Please contact admin to reset your password", "Forgot Password",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SignIn_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You are already on the login page", "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AttemptLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;
            bool rememberMe = chkRememberMe.IsChecked ?? false;

            if (ValidateInput(username, password))
            {
                try
                {
                    string loginResult = AuthenticateUser(username, password);

                    if (loginResult != "NULL_Username" && loginResult != "NULL_Password")
                    {
                        HandleSuccessfulLogin(loginResult, rememberMe);
                    }
                    else
                    {
                        MessageBox.Show(GetLoginErrorMessage(loginResult), "Login Failed",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login error: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidateInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your email or username", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private string AuthenticateUser(string username, string password)
        {
            Account account = new Account
            {
                Username = username,
                Password = password
            };

            AccountAccess acAccess = new AccountAccess();
            string infor = acAccess.CheckLoginData(account);
            this.ID_user = infor;
            return infor;
        }

        private void HandleSuccessfulLogin(string userInfo, bool rememberMe)
        {
            if (rememberMe)
            {
                SaveLoginInfo(txtUsername.Text, txtPassword.Password);
            }

            MessageBox.Show("Login successful!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Mở cửa sổ chính và đóng cửa sổ đăng nhập
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private string GetLoginErrorMessage(string errorCode)
        {
            return errorCode switch
            {
                "NULL_Username" => "Username does not exist",
                "NULL_Password" => "Incorrect password",
                _ => "Login failed. Please try again."
            };
        }

        private void SaveLoginInfo(string username, string password)
        {
            // Lưu thông tin đăng nhập (có thể sử dụng Properties.Settings hoặc file config)
            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Save();
        }
    }
}