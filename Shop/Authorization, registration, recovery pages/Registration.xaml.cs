using System;
using System.Collections.Generic;
using System.Globalization;
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
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop
{
    public partial class Registration : Page
    {
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        public Registration()
        {
            InitializeComponent();
        }

        private void ReturnToAuthorization_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Authorization();
        }

        private void ReturnToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Authorization();
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

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string email = RegistrationMailTextBox.Text;
            string phonenumber = RegistrationPhoneNumberTextBox.Text;
            string patternforemail = @"^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$";
            string patternforphonenumber = @"^((\+7|7|8)+([0-9]){10})$";
            bool isValidEmail = Regex.IsMatch(email, patternforemail, RegexOptions.IgnoreCase);
            bool isValidPhoneNumber = Regex.IsMatch(phonenumber, patternforphonenumber, RegexOptions.IgnoreCase);
            if (PasswordTextBox.Password.Length >= 8 && PasswordTextBox.Password == PasswordConfirmationTextBox.Password &&
                RegistrationNameTextBox.Text.Length >= 2 &&
                RegistrationSurnameTextBox.Text.Length >= 2 && 
                isValidPhoneNumber && isValidEmail)
            {
                var allLogins = _usersTableAdapter.GetData().Rows;
                StringBuilder hashPassword = ComputeSHA256Hash(PasswordTextBox.Password);
                bool auth = true;
                for (int i = 0; i < allLogins.Count; i++)
                {
                    if (allLogins[i][3].ToString() == RegistrationMailTextBox.Text || allLogins[i][5].ToString() == RegistrationPhoneNumberTextBox.Text)
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                        auth = false;
                        break;
                    }
                }
                if (auth)
                {
                    _usersTableAdapter.InsertQueryUsers(RegistrationNameTextBox.Text, RegistrationSurnameTextBox.Text, RegistrationMailTextBox.Text, hashPassword.ToString(), RegistrationPhoneNumberTextBox.Text, 4);
                    MessageBox.Show("Регистрация проведена успешно");
                    ((MainWindow)Application.Current.MainWindow).Authorization();
                }
            }
            else if (PasswordTextBox.Password == "" ||
                    PasswordConfirmationTextBox.Password == "" ||
                    RegistrationNameTextBox.Text == "" || RegistrationSurnameTextBox.Text == "" || 
                    RegistrationPhoneNumberTextBox.Text == "" || RegistrationMailTextBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else if (RegistrationNameTextBox.Text.Length < 2 || RegistrationSurnameTextBox.Text.Length < 2)
            {
                MessageBox.Show("Слишком короткое имя или фамилие");
            }
            else if (PasswordTextBox.Password.Length < 8)
            {
                MessageBox.Show("Пароль слишком короткий");
            }
            else if (!isValidEmail)
            {
                MessageBox.Show("Некорректно введена почта");
            }
            else if (!isValidPhoneNumber)
            {
                MessageBox.Show("Некорректно введен номер телефона");
            }
            else if (PasswordTextBox.Password != PasswordConfirmationTextBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }

        private void RegistrationPhoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || RegistrationPhoneNumberTextBox.Text.Length > 10) e.Handled = true;
        }

        private void RegistrationNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void RegistrationSurnameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void RegistrationSurnameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox.Text);
            }
        }

        private void RegistrationNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox.Text);
            }
        }
    }
}
