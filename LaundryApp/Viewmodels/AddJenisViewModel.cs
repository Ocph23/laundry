using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryApp.Models;
using System.Windows;

namespace LaundryApp.Viewmodels
{
   public class AddJenisViewModel:jenis
    {
        #region Properties
        public Action WindowClose { get; internal set; }
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CancelCommand { get; set; }
        private jenis itemSelected;


        public string Error { get; set; }
        public bool Saved { get; private set; }

        public string this[string columnName]
        {
            get
            {
                Error = null;
                if (columnName == "Keterangan")
                {
                    if (string.IsNullOrEmpty(this.Keterangan))
                        Error = "Keterangan Tidak Boleh Kosong";
                }

                if (columnName == "Nama")
                {
                    if (string.IsNullOrEmpty(this.Nama))
                        Error = "Nama Tidak Boleh Kosong";
                }

                if (columnName == "Tarif")
                {
                    if (this.Tarif<=0)
                        Error = "Tarif Tidak Boleh 0 (Nol)";
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
                if (this.JenisId== 0)
                {
                    using (var db = new OcphDbContext())
                    {
                        var savedId = db.Jenises.InsertAndGetLastID(this);
                        if (savedId > 0)
                        {
                            this.JenisId = savedId;
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
                        var saved = db.Jenises.Update(O => new { O.Aktif,O.Keterangan,O.Nama,O.Tarif }, this, O => O.JenisId == this.JenisId);
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
        public AddJenisViewModel()
        {
            this.Load();
        }


        public AddJenisViewModel(jenis itemSelected)
        {
            this.Load();
            this.Aktif = itemSelected.Aktif;
            this.Nama = itemSelected.Nama;
            this.Tarif = itemSelected.Tarif;
            this.Keterangan = itemSelected.Keterangan;
        }

        #endregion












    }
}
