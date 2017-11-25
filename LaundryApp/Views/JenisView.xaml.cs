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
    /// Interaction logic for JenisView.xaml
    /// </summary>
    public partial class JenisView : Window
    {
        private JenisViewModel viewmodel;

        public JenisView()
        {
            InitializeComponent();
            this.viewmodel = new Viewmodels.JenisViewModel() { WindowClose=this.Close};
            this.DataContext = viewmodel;
        }
    }
}
