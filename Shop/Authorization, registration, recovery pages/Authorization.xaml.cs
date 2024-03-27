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
    public partial class Authorization : Page
    {
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        public Authorization()
        {
            InitializeComponent();
        }

        private void RecoveryPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).RecoveryPassword();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Registration();
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
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool auth = false;
            var allLogins = _usersTableAdapter.GetData().Rows;
            StringBuilder hashBuilder = ComputeSHA256Hash(PasswordTextBox.Password);
            for (int i = 0; i < allLogins.Count ; i++)
            {
                if ((allLogins[i][3].ToString() == EmailOrPhoneNumberTextBox.Text || allLogins[i][5].ToString() == EmailOrPhoneNumberTextBox.Text) && allLogins[i][4].ToString() == hashBuilder.ToString())
                {
                    auth = true;
                    int ID_User = (int)allLogins[i][0];
                    int ID_role_user = (int)allLogins[i][6];
                    /*  Роли пользователей:
                    Администратор ID 1
                    Менеджер ID 2
                    Курьер ID 3
                    Покупатель / Клиент ID 4  */
                    switch (ID_role_user)
                    {
                        case 1:
                            Administrator__manager__courirer__defaultuser_windows.AdministratorWindow administratorWindow = new Administrator__manager__courirer__defaultuser_windows.AdministratorWindow(ID_User, ID_role_user);
                            administratorWindow.Show();
                            ((MainWindow)Application.Current.MainWindow).Close();
                            break;
                        case 2:
                            Administrator__manager__courirer__defaultuser_windows.AdministratorWindow administratorWindow2 = new Administrator__manager__courirer__defaultuser_windows.AdministratorWindow(ID_User, ID_role_user);
                            administratorWindow2.Show();
                            ((MainWindow)Application.Current.MainWindow).Close();
                            break;
                        //case 3: 
                        //Окно для курьера
                        //break;
                        case 4:
                            Administrator__manager__courirer__defaultuser_windows.AdministratorWindow administratorWindow4 = new Administrator__manager__courirer__defaultuser_windows.AdministratorWindow(ID_User, ID_role_user);
                            administratorWindow4.Show();
                            ((MainWindow)Application.Current.MainWindow).Close();
                            break;
                    }
                }
            }
            if (!auth)
            {
                MessageBox.Show("Неверный пароль или почта");
            }
        }
    }
}
