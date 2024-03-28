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

namespace Shop.Administrator__manager__courirer__client_windows.Administrator.Pages
{
    public partial class PageChange : Page
    {
        int ID_Role_pc;
        public PageChange(int ID_Role)
        {
            InitializeComponent();
            ID_Role_pc = ID_Role;
            if (ID_Role_pc == 2)
            {
                EditingUsers.Visibility = Visibility.Collapsed;
                EditingOther.Visibility = Visibility.Collapsed;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditingUsers.IsSelected && ID_Role_pc == 1)
            {
                PageFrame.Content = new Pages_in_the_users_window.UsersChange();
            }
            else if (EditingTheStore.IsSelected)
            {
                PageFrame.Content = new Pages_in_the_users_window.StoreChange();
            }
            else if (EditingOther.IsSelected && ID_Role_pc == 1)
            {
                PageFrame.Content = new Pages_in_the_users_window.ChangeOtherTables();
            }
            else if(Orders.IsSelected)
            {
                PageFrame.Content = new Pages_in_the_users_window.Orders(ID_Role_pc);
            }
        }
    }
}
