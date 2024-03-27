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
using System.Security.Cryptography;
using Shop.Administrator__manager__courirer__defaultuser_windows;
using Shop.Pages_in_the_users_window;


namespace Shop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PageFrame.Content = new Authorization();
        }
        public void RecoveryPassword()
        {
            PageFrame.Content = new PasswordRecovery();
        }
        public void Registration()
        {
            PageFrame.Content = new Registration();
        }
        public void Authorization()
        {
            PageFrame.Content = new Authorization();
        }
    }
}
