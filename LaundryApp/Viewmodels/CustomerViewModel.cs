using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using LaundryApp.Models;

namespace LaundryApp.Viewmodels
{
    public class CustomerViewModel :DAL.BaseNotifyProperty
    {
        private pelanggan itemSelected;

        #region Properties
        public ObservableCollection<Models.pelanggan> Pelanggans { get; set; }
        public CollectionView PelangganView { get; set; }
        public Models.pelanggan ItemSelected{
            get { return itemSelected; }
            set
            {
                itemSelected = value;
                OnPropertyChange("ItemSelected");
            }
        }

        public CommandHandler AddCommand { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler CloseCommand { get; private set; }
        public Action WindowClose { get; internal set; }


        #endregion

        #region Constructor
        public CustomerViewModel()
        {

            AddCommand = new CommandHandler { CanExecuteAction = AddValidation, ExecuteAction = AddAction };
            EditCommand = new CommandHandler { CanExecuteAction = EditValidation, ExecuteAction = EditAction };
            CloseCommand = new CommandHandler { CanExecuteAction =x=>true, ExecuteAction =x=> WindowClose()};

            using (var db = new OcphDbContext())
            {
                var pelanggans = db.Pelanggans.Select().ToList();
                Pelanggans = new ObservableCollection<Models.pelanggan>(pelanggans);
                PelangganView = (CollectionView)CollectionViewSource.GetDefaultView(Pelanggans);
            }
        }
        #endregion

        #region Validation

        private bool AddValidation(object obj)
        {
            return true;
        }
        private bool EditValidation(object obj)
        {
            if (ItemSelected != null)
                return true;
            else
                return false;
        }
        #endregion

        #region Action
      
        private void EditAction(object obj)
        {
            var form = new Views.AddCustomerView();
            var viewmodel = new Viewmodels.AddCustomerViewModel(this.ItemSelected) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.Saved)
            {
               ItemSelected.Aktif= viewmodel.Aktif;
                ItemSelected.Alamat = viewmodel.Alamat;
                ItemSelected.JenisKelamin = viewmodel.JenisKelamin;
                ItemSelected.Nama = viewmodel.Nama;
                ItemSelected.NoKTP = viewmodel.NoKTP;
                ItemSelected.telepon = viewmodel.telepon;
                
            }
            PelangganView.Refresh();
        }
        private void AddAction(object obj)
        {
            var form = new Views.AddCustomerView();
            var viewmodel = new Viewmodels.AddCustomerViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if(viewmodel.Saved)
            {
                pelanggan data = viewmodel;
                this.Pelanggans.Add(data);
                PelangganView.Refresh();
            }

        }

        #endregion

    }
}
