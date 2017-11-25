using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LaundryApp.Models;

namespace LaundryApp.Viewmodels
{
   public  class JenisViewModel:DAL.BaseNotifyProperty
    {
        private jenis itemSelected;


        #region Properties
        public ObservableCollection<Models.jenis> Jenises { get; set; }
        public CollectionView JenisView { get; set; }
        public Models.jenis ItemSelected
        {
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
        public JenisViewModel()
        {
            AddCommand = new CommandHandler { CanExecuteAction = AddValidation, ExecuteAction = AddAction };
            EditCommand = new CommandHandler { CanExecuteAction = EditValidation, ExecuteAction = EditAction };
            CloseCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };

            using (var db = new OcphDbContext())
            {
                var jenises = db.Jenises.Select().ToList();
                Jenises = new ObservableCollection<Models.jenis>(jenises);
                JenisView = (CollectionView)CollectionViewSource.GetDefaultView(Jenises);
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
            var form = new Views.AddJenisView();
            var viewmodel = new Viewmodels.AddJenisViewModel(this.ItemSelected) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.Saved)
            {
                ItemSelected.Aktif = viewmodel.Aktif;
                ItemSelected.Nama = viewmodel.Nama;
                itemSelected.Tarif = viewmodel.Tarif;
                ItemSelected.Keterangan = viewmodel.Keterangan;

            }
            JenisView.Refresh();
        }
        private void AddAction(object obj)
        {
            var form = new Views.AddJenisView();
            var viewmodel = new Viewmodels.AddJenisViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.Saved)
            {
                jenis data = viewmodel;
                this.Jenises.Add(data);
                JenisView.Refresh();
            }
        }

        #endregion

    }
}
