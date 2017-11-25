using System;
using LaundryApp.Models;
using System.ComponentModel;
using System.Windows;

namespace LaundryApp.Viewmodels
{
    public class AddCustomerViewModel:Models.pelanggan,IDataErrorInfo
    {

        #region Properties
        public Action WindowClose { get; internal set; }
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CancelCommand { get; set; }

        public string Error { get; set; }
        public bool Saved { get; private set; }

        public string this[string columnName]
        {
            get
            {
                Error= null;
                if(columnName=="Alamat")
                {
                    if (string.IsNullOrEmpty(this.Alamat))
                        Error = "Alamat Tidak Boleh Kosong";
                }

                if (columnName == "Nama")
                {
                    if (string.IsNullOrEmpty(this.Nama))
                        Error = "Nama Tidak Boleh Kosong";
                }

                if (columnName == "NoKTP")
                {
                    if (string.IsNullOrEmpty(this.NoKTP))
                        Error = "No KTP atau Tanda Pengenal  Tidak Boleh Kosong";
                }

                if (columnName == "telepon")
                {
                    if (string.IsNullOrEmpty(this.telepon))
                        Error = "Nomor Telepon  Tidak Boleh Kosong";
                }


                return Error;
            }
        }
        #endregion

        #region Action
        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidation, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        private void SaveAction(object obj)
        {
            try
            {
                if (this.PelangganId == 0)
                {
                    using (var db = new OcphDbContext())
                    {
                        var savedId = db.Pelanggans.InsertAndGetLastID(this);
                        if (savedId > 0)
                        {
                            this.PelangganId = savedId;
                            MessageBox.Show("Data  Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Saved = true;
                            this.WindowClose();
                        }

                        else
                            MessageBox.Show("Data Tidak Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        var saved = db.Pelanggans.Update(O => new { O.Aktif, O.Alamat, O.JenisKelamin, O.Nama, O.NoKTP, O.telepon }, this, O => O.PelangganId == this.PelangganId);
                        if (saved)
                        {
                            MessageBox.Show("Data Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Saved = true;
                            this.WindowClose();
                        }

                        else
                            MessageBox.Show("Data Tidak Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        #endregion

        #region Validation
        private bool SaveValidation(object obj)
        {
            if (!string.IsNullOrEmpty(Error))
                return false;
            else
                return true;
        }
        #endregion

        #region Constructor
        public AddCustomerViewModel()
        {
            this.Load();
        }

     
        public AddCustomerViewModel(pelanggan itemSelected)
        {
            this.Load();
            this.Aktif = itemSelected.Aktif;
            this.Alamat = itemSelected.Alamat;
            this.JenisKelamin = itemSelected.JenisKelamin;
            this.Nama = itemSelected.Nama;
            this.NoKTP = itemSelected.NoKTP;
            this.PelangganId = itemSelected.PelangganId;
            this.Tanggal = itemSelected.Tanggal;
            this.telepon = itemSelected.telepon;
           
        }

        #endregion





    }
}