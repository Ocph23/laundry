using System;
using LaundryApp.Models;
using System.Windows;

namespace LaundryApp.Viewmodels
{
    internal class BayarViewModel:DAL.BaseNotifyProperty
    {
        private transaksi selected;
        public BayarViewModel()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = x => SaveValidation, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction=x => WindowClose()};
            this.TotalBiaya = selected.Biaya.GrandTotal;
        }

        private void SaveAction(object obj)
        {

            using (var db = new OcphDbContext())
            {

              var   isUpdated = db.Transaksis.Update(O => new { O.StatusPembayaran }, new transaksi { StatusPembayaran = StatusPembayaran.Lunas }, O => O.TransaksiId == selected.TransaksiId);
                if(isUpdated)
                {
                    selected.StatusPembayaran = StatusPembayaran.Lunas;
                    MessageBox.Show("Data Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.WindowClose();
                }else
                {
                    MessageBox.Show("Data Tidak  Tersimpan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private double _uangDiserahkan;

        public double UangDiserahkan
        {
            get { return _uangDiserahkan; }
            set { _uangDiserahkan = value;
                Kembalian = UangDiserahkan - TotalBiaya;
                OnPropertyChange("UangDiserahkan"); }
        }

        private double biaya;

        public double TotalBiaya
        {
            get { return biaya; }
            set { biaya = value; OnPropertyChange("TotalBiaya"); }
        }
        private double kembalian;

        public double Kembalian
        {
            get { return kembalian; }
            set { kembalian = value; OnPropertyChange("Kembalian"); }
        }

        public bool SaveValidation { get {
                if (UangDiserahkan <= 0 || Kembalian < 0)
                    return false;
                return true;

            } }
        public CommandHandler SaveCommand { get; }
        public CommandHandler CancelCommand { get; }
        public Action WindowClose { get; internal set; }

        public BayarViewModel(transaksi selected)
        {
            this.selected = selected;
            SaveCommand = new CommandHandler { CanExecuteAction = x => SaveValidation, ExecuteAction = SaveAction };
            this.TotalBiaya = selected.Biaya.GrandTotal;
        }
    }
}