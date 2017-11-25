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
using System.Windows.Shapes;
using LaundryApp.Viewmodels;

namespace LaundryApp.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private UserViewModel viewmodel;

        public UserView(Viewmodels.UserViewModel vm)
        {
            InitializeComponent();
            vm.WindowClose = this.Close;
            this.viewmodel = vm;
            this.DataContext = viewmodel;
        }

        private void oldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            viewmodel.OldPassword = pb.Password;
        }

        private void newPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            viewmodel.NewPassword = pb.Password;
        }

        private void confirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            viewmodel.ConfromPassword = pb.Password;
        }
    }
}
