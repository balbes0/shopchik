﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using Shop.Administrator__manager__courirer__defaultuser_windows;
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop.Pages_in_the_users_window
{
    public partial class AccountInfo : Page
    {
        int ID_User_ai;
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        HistoryOrdersTableAdapter _historyOrdersTableAdapter = new HistoryOrdersTableAdapter();
        
        public AccountInfo(int ID_User)
        {
            ID_User_ai = ID_User;
            InitializeComponent();
            var allUsers = _usersTableAdapter.GetData().Rows;
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (Convert.ToInt32(allUsers[i][0]) == ID_User_ai)
                {
                    UserName.Text = allUsers[i][1].ToString();
                    UserSurname.Text = allUsers[i][2].ToString();
                    UserEmail.Text = allUsers[i][3].ToString();
                    UserPhoneNumber.Text = allUsers[i][5].ToString();
                    DataGridHistoryOrders.ItemsSource = _historyOrdersTableAdapter.GetHistoryOrdersByUserID(ID_User_ai);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Main.IsSelected)
            {
                MainGrid.Visibility = Visibility.Visible;
                ChangePassword.Visibility = Visibility.Collapsed;
                GridWithHistoryOrders.Visibility = Visibility.Collapsed;
            }
            if (Orders.IsSelected)
            {
                MainGrid.Visibility = Visibility.Collapsed;
                ChangePassword.Visibility = Visibility.Collapsed;
                GridWithHistoryOrders.Visibility = Visibility.Visible;
            }
            if (Password.IsSelected)
            {
                MainGrid.Visibility = Visibility.Collapsed;
                ChangePassword.Visibility = Visibility.Visible;
                GridWithHistoryOrders.Visibility= Visibility.Collapsed;
            }
            if (Exit.IsSelected)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is AdministratorWindow)
                        {
                            window.Close();
                        }
                    }
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text != "" && UserSurname.Text != "" && UserEmail.Text != "" && UserPhoneNumber.Text != "")
            {
                string email = UserEmail.Text;
                string phonenumber = UserPhoneNumber.Text;
                string patternforemail = @"^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$";
                string patternforphonenumber = @"^((\+7|7|8)+([0-9]){10})$";
                bool isValidEmail = Regex.IsMatch(email, patternforemail, RegexOptions.IgnoreCase);
                bool isValidPhoneNumber = Regex.IsMatch(phonenumber, patternforphonenumber, RegexOptions.IgnoreCase);
                if (isValidEmail && isValidPhoneNumber && UserName.Text.Length > 2 && UserSurname.Text.Length > 2)
                {
                    _usersTableAdapter.UpdateQueryUserInfo(UserName.Text, UserSurname.Text, UserEmail.Text, UserPhoneNumber.Text, ID_User_ai);
                    MessageBox.Show("Информация и пользователе изменена");
                }
                else
                {
                    MessageBox.Show("Некорректно введены данные");
                }
            }
            else if (UserName.Text == "" || UserSurname.Text == "" || UserEmail.Text == "" || UserPhoneNumber.Text == "")
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }

        private void SaveNewPassword_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = _usersTableAdapter.GetData().Rows;
            string oldpassword = ComputeSHA256Hash(OldPassword.Password.ToString());
            string newpassword = ComputeSHA256Hash(NewPassword.Password.ToString());
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (ID_User_ai == Convert.ToInt32(allUsers[i][0]))
                {
                    if (oldpassword == allUsers[i][4].ToString())
                    {
                        if (NewPassword.Password == ConfirmPassword.Password)
                        {
                            if (NewPassword.Password.Length >= 8)
                            {
                                _usersTableAdapter.UpdateQueryChangePassword(newpassword, ID_User_ai);
                                MessageBox.Show("Пароль успешно изменен");
                                break;
                            }
                            else if (NewPassword.Password.Length < 8)
                            {
                                MessageBox.Show("Пароль слишком короткий");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают");
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Старый пароль указан неверно");
                        break;
                    }
                }
            }
        }
        private static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void UserName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void UserSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void UserPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || UserPhoneNumber.Text.Length > 10) e.Handled = true;
        }
    }
}
