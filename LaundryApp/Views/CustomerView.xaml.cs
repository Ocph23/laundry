﻿using System;
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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        private CustomerViewModel viewmodel;

        public CustomerView()
        {
            InitializeComponent();
            this.viewmodel = new Viewmodels.CustomerViewModel() { WindowClose = this.Close };
            this.DataContext = viewmodel; 
        }
    }
}