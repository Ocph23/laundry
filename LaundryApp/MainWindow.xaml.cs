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

namespace LaundryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var vm = new Viewmodels.LoginViewModel();
            var form = new Views.LoginView(vm);
            form.ShowDialog();
            if(vm.UserLogin!=null)
            {
                InitializeComponent();
                this.DataContext = new Viewmodels.MainViewModels() {};
            }else
            {
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            var form = new Reports.Forms.Summary();
            form.ShowDialog();
        }

        private void jenis_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.JenisView();
            form.ShowDialog();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.SettingView();
            form.ShowDialog();
        }

        private void user_Click_1(object sender, RoutedEventArgs e)
        {
            var viewmodel = new Viewmodels.UserViewModel();
            var form = new Views.UserView(viewmodel);
            form.ShowDialog();
        }
        
    }
}
