using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для PasswordRecovery.xaml
    /// </summary>
    public partial class PasswordRecovery : Page
    {
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        public PasswordRecovery()
        {
            InitializeComponent();
        }
        private static StringBuilder ComputeSHA256Hash(string input)
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
                return builder;
            }
        }
        private void ReturnToAuthorization_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Authorization();
        }

        private void ReturnToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Authorization();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password.Length >= 8 && PasswordTextBox.Password == PasswordConfirmationTextBox.Password)
            {
                StringBuilder hashPassword = ComputeSHA256Hash(PasswordTextBox.Password);
                var allLogins = _usersTableAdapter.GetData().Rows;
                bool flag = false;
                for (int i = 0; i < allLogins.Count; i++)
                {
                    if (allLogins[i][3].ToString() == EmailOrPhoneNumberTextBox.Text || allLogins[i][5].ToString() == EmailOrPhoneNumberTextBox.Text)
                    {
                        flag = true;
                        int ID_User = (int)allLogins[i][0];
                        _usersTableAdapter.UpdateQueryChangePassword(hashPassword.ToString(), ID_User);
                        break;
                    }
                }
                if (flag)
                {
                    MessageBox.Show("Пароль успешно изменен");
                    ((MainWindow)Application.Current.MainWindow).Authorization();
                }
                else if (!flag)
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
            }
            else if (PasswordTextBox.Password == "" || PasswordConfirmationTextBox.Password == "" || EmailOrPhoneNumberTextBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else if (PasswordTextBox.Password.Length < 8)
            {
                MessageBox.Show("Пароль слишком короткий");
            }
            else if (PasswordTextBox.Password != PasswordConfirmationTextBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
