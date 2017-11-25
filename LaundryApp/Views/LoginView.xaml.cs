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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel viewmodel;

        public LoginView(Viewmodels.LoginViewModel viewmodel)
        {
            InitializeComponent();
            this.viewmodel = viewmodel;
            viewmodel.WindowClose = this.Close;
            this.DataContext = viewmodel;
        
        }

        private void newPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pwsd = (PasswordBox)sender;
            viewmodel.Password = pwsd.Password;
        }
    }
}
