using System;
using System.Collections.Generic;
using System.Data;
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

namespace Shop.Pages_in_the_users_window
{
    public partial class UsersChange : Page
    {
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        UsersViewTableAdapter _usersViewTableAdapter = new UsersViewTableAdapter();
        Users_rolesTableAdapter _users_RolesTableAdapter = new Users_rolesTableAdapter();
        public UsersChange()
        {
            InitializeComponent();
            DataGridUsersChange.ItemsSource = _usersViewTableAdapter.GetData();
            Roles_IDCMBX.ItemsSource = _users_RolesTableAdapter.GetData();
            Roles_IDCMBX.DisplayMemberPath = "Role_name";
        }

        private void DataGridUsersChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridUsersChange.SelectedItem != null)
            {
                UserNameTBX.Text = (DataGridUsersChange.SelectedItem as DataRowView).Row[1].ToString();
                UserSurnameTBX.Text = (DataGridUsersChange.SelectedItem as DataRowView).Row[2].ToString();
                UserEmailTBX.Text = (DataGridUsersChange.SelectedItem as DataRowView).Row[3].ToString();
                UserPhoneNumberTBX.Text = (DataGridUsersChange.SelectedItem as DataRowView).Row[4].ToString();
                Roles_IDCMBX.Text = (DataGridUsersChange.SelectedItem as DataRowView).Row[5].ToString();
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            var allUsers = _usersTableAdapter.GetData().Rows;
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i][3].ToString() == UserEmailTBX.Text || allUsers[i][5].ToString() == UserPhoneNumberTBX.Text)
                {
                    flag = false;
                    MessageBox.Show("Пользователь с такой почтой или номером уже существует");
                    break;
                }
            }
            if (flag)
            {
                DataRowView selectedRow = (DataRowView)Roles_IDCMBX.SelectedItem;
                int ID_Role = (int)selectedRow["ID_user_role"];
                string patternforemail = @"^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$";
                string email = UserEmailTBX.Text;
                string phone = UserPhoneNumberTBX.Text;
                string patternforphonenumber = @"^((\+7|7|8)+([0-9]){10})$";
                bool isValidEmail = Regex.IsMatch(email, patternforemail, RegexOptions.IgnoreCase);
                bool isValidPhoneNumber = Regex.IsMatch(phone, patternforphonenumber, RegexOptions.IgnoreCase);
                if (NewPasswordTBX.Password != "" && NewPasswordTBX.Password.Length >= 8 && UserNameTBX.Text.Length > 2 && UserSurnameTBX.Text.Length > 2 && isValidEmail && isValidPhoneNumber)
                {
                    string hashPassword = ComputeSHA256Hash(NewPasswordTBX.Password);
                    _usersTableAdapter.InsertQueryUsers(UserNameTBX.Text, UserSurnameTBX.Text, UserEmailTBX.Text, hashPassword, UserPhoneNumberTBX.Text, ID_Role);
                    DataGridUsersChange.ItemsSource = _usersViewTableAdapter.GetData();
                }
                else if (NewPasswordTBX.Password == "" || NewPasswordTBX.Password.Length < 8)
                {
                    MessageBox.Show("Некорректный пароль");
                }
                else
                {
                    MessageBox.Show("Некорректно введены данные");
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsersChange.SelectedItem != "")
            {
                object id = (DataGridUsersChange.SelectedItem as DataRowView).Row[0];
                _usersTableAdapter.DeleteUserByID(Convert.ToInt32(id));
                DataGridUsersChange.ItemsSource = _usersViewTableAdapter.GetData();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UserNameTBX.Text = "";
            UserSurnameTBX.Text = "";
            UserEmailTBX.Text = "";
            UserPhoneNumberTBX.Text = "";
            Roles_IDCMBX.Text = null;
            NewPasswordTBX.Password = "";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsersChange.SelectedItem != null)
            {
                string patternforemail = @"^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$";
                string email = UserEmailTBX.Text;
                string phone = UserPhoneNumberTBX.Text;
                string patternforphonenumber = @"^((\+7|7|8)+([0-9]){10})$";
                bool isValidEmail = Regex.IsMatch(email, patternforemail, RegexOptions.IgnoreCase);
                bool isValidPhoneNumber = Regex.IsMatch(phone, patternforphonenumber, RegexOptions.IgnoreCase);
                object ID_User = (DataGridUsersChange.SelectedItem as DataRowView).Row[0];
                bool flag = true;
                var allUsers = _usersTableAdapter.GetData().Rows;
                for (int i = 0; i < allUsers.Count; i++)
                {
                    if ((allUsers[i][3].ToString() == UserEmailTBX.Text || allUsers[i][5].ToString() == UserPhoneNumberTBX.Text) && Convert.ToInt32(allUsers[i][0]) != Convert.ToInt32(ID_User))
                    {
                        flag = false;
                        MessageBox.Show("Пользователь с такой почтой или номером уже существует");
                        break;
                    }
                }
                if (flag)
                {
                    if (isValidEmail && isValidPhoneNumber && Roles_IDCMBX.SelectedItem != "")
                    {
                        if (NewPasswordTBX.Password != "")
                        {
                            string hashPassword = ComputeSHA256Hash(NewPasswordTBX.Password);
                            _usersTableAdapter.UpdateQueryChangePassword(hashPassword, Convert.ToInt32(ID_User));
                        }
                        _usersTableAdapter.UpdateQueryUserInfo(UserNameTBX.Text, UserSurnameTBX.Text, UserEmailTBX.Text, UserPhoneNumberTBX.Text, Convert.ToInt32(ID_User));
                        MessageBox.Show("Данные пользователя были успешно изменены");
                        DataGridUsersChange.ItemsSource = _usersViewTableAdapter.GetData();
                    }
                    MessageBox.Show("Некорректно введен номер или почта");
                }
                ClearButton_Click(sender, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<usermodel> forimport = jsonconverter.DeserializeObject<List<usermodel>>();
            foreach (var item in forimport)
            {
                string hashpassword = ComputeSHA256Hash(item.User_password);
                _usersTableAdapter.InsertQueryUsers(item.User_name, item.User_surname, item.User_email, hashpassword, item.User_phone, item.User_role);
            }
            DataGridUsersChange.ItemsSource = _usersViewTableAdapter.GetData();
        }

        private void UserNameTBX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void UserSurnameTBX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        private void UserPhoneNumberTBX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
