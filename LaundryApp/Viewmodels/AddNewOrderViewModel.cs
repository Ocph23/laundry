using System;
using System.Collections.ObjectModel;
using System.Linq;
using LaundryApp.Models;
using System.Windows;

namespace LaundryApp.Viewmodels
{
    internal class AddNewOrderViewModel:Models.transaksi
    {
        public ObservableCollection<Models.jenis> Jenises { get; set; }
        public Action WindowClose { get; set; }
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CancelCommand { get; set; }
        public AddNewOrderViewModel()
        {
            SaveCommand = new CommandHandler { CanExecuteAction =SaveValidation, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
            Items = new ObservableCollection<Models.itemtransaksi>();

            using (var db = new OcphDbContext())
            {
                Jenises = new ObservableCollection<Models.jenis>(db.Jenises.Select());
                this.Setting = db.Settings.Where(O => O.Aktif == true).FirstOrDefault();
            }
        }

       

        private bool SaveValidation(object obj)
        {
            var isValid = true;
            if (this.Items.Count <= 0 || this.Setting==null )
                isValid = false;

            return isValid;
        }

        private void SaveAction(object obj)
        {
            this.SettingId = Setting.SettingId;
            this.StatusPembayaran = StatusPembayaran.Belum;
            this.StatusPengambilan = StatusPengambilan.Belum;
            this.TanggalMasuk = DateTime.Now;
            this.StatusPengerjaan = StatusPengerjaan.Baru;
            this.Kode = Helpers.GenerateCode();
            this.UserId = 1;

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                   

                    this.TransaksiId = db.Transaksis.InsertAndGetLastID(this);
                    if(TransaksiId>0)
                    {
                        foreach(var item in this.Items)
                        {
                            item.TransaksiId = TransaksiId;
                            item.ItemTransaksiId = db.ItemsTransaksi.InsertAndGetLastID(item);
                            if (item.ItemTransaksiId <= 0)
                                throw new SystemException("Data Tidak Tersimapn");
                        }

                        trans.Commit();
                        IsSaved = true;
                        MessageBox.Show("Data Tersimpan !", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                catch (Exception ex )
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        public bool IsSaved { get; internal set; }


        


    }
}