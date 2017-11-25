﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LaundryApp.Models;
using System.Windows.Controls;
using System.Windows;

namespace LaundryApp.Viewmodels
{
    public class MainViewModels:DAL.BaseNotifyProperty
    {
       
        #region Properties
        private transaksi _selected;
        private TransactionCollection collection;
        private TabItem tabSelected;
        public CommandHandler PengembalianCommand { get; set; }
        public CommandHandler BayarCommand { get; set; }
        public CommandHandler PengerjaanCommand { get; set; }
        public CommandHandler AddNewOrderCommand { get; }
        public CommandHandler ChangePasswordCommand { get; set; }
        public CommandHandler AddNewUserCommand { get; set; }
        public CollectionView TransaksiView { get; set; }
        public ObservableCollection<Models.itemtransaksi> ItemsData { get; set; }
        public CollectionView ItemsView { get; set; }
        public TabItem TabSelected
        {
            get { return tabSelected;}
            set
            {
                tabSelected = value;
               if(value.Name=="biayaInfo" && Selected!=null)
                {
                    GetBiaya(Selected);
                }else if(value.Name=="pengembalianInfo" && Selected!=null)
                {
                    if (Selected.StatusPengambilan == StatusPengambilan.Sudah)
                        Selected.InputPengambilanVisiblity = Visibility.Collapsed;
                    if(Selected.Pengembalian==null)
                    {
                        Selected.Pengembalian = new Models.pengembalian { TransaksiId=Selected.TransaksiId, TanggalPengambilan = DateTime.Now };
                    }
                }
                OnPropertyChange("TabSelected");
            }
        }

        private void GetPelanggan(transaksi selected)
        {
           if(Selected.Pelanggan==null)
            {

                using (var db = new OcphDbContext())
                {
                    Selected.Pelanggan = db.Pelanggans.Where(O => O.PelangganId == selected.PelangganId).FirstOrDefault();
                }
            }
        }

      //OK
        public transaksi Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                GetOrders(value);
                OnPropertyChange("Selected");
            }
        }

        public user UserLogin { get; private set; }


        #endregion

        #region Method


        #region Action
        private void AddNewUserAction(object obj)
        {
            var vm = new Viewmodels.UserViewModel();
            var form = new Views.UserView(vm);
            form.ShowDialog();
        }

        private void ChangePasswordAction(object obj)
        {
            var vm = new Viewmodels.UserViewModel(this.UserLogin);
            var form = new Views.UserView(vm);
            form.ShowDialog();
        }
        private void PengerjaanAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                if (Selected.StatusPengerjaan == StatusPengerjaan.Baru)
                {
                    var isSaved = db.Transaksis.Update(O => new { O.StatusPengerjaan }, new transaksi { StatusPengerjaan = StatusPengerjaan.Dikerjakan }, O => O.TransaksiId == Selected.TransaksiId);
                    if (isSaved)
                        Selected.StatusPengerjaan = StatusPengerjaan.Dikerjakan;
                }
                else if (Selected.StatusPengerjaan == StatusPengerjaan.Dikerjakan)
                {
                    var isSaved = db.Transaksis.Update(O => new { O.StatusPengerjaan }, new transaksi { StatusPengerjaan = StatusPengerjaan.Selesai }, O => O.TransaksiId == Selected.TransaksiId);
                    if (isSaved)
                    {
                        Selected.StatusPengerjaan = StatusPengerjaan.Selesai;
                        Selected.ButtonVisiblity = Visibility.Hidden;
                    }

                }

            }
        }
        private void BayarAction(object obj)
        {
            var form = new Views.BayarView();
            var viewmodel = new Viewmodels.BayarViewModel(Selected) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
        }

        private void AddNewOrderAction(object obj)
        {
            var viewmodel = new Viewmodels.AddNewOrderViewModel();
            var form = new Views.AddNewOrder();
            form.DataContext = viewmodel;
            form.ShowDialog();
            if (viewmodel.IsSaved)
            {
                collection.Transaksi.Add(viewmodel);
                TransaksiView.Refresh();
            }
        }
        #endregion


        private void GetOrders(transaksi selected)
        {
            if (selected.Items == null)
            {
                using (var db = new OcphDbContext())
                {
                    var items = from a in db.ItemsTransaksi.Where(O=>O.TransaksiId==selected.TransaksiId)
                                join b in db.Jenises.Select() on a.JenisId equals b.JenisId
                                select new itemtransaksi { Berat = a.Berat, Biaya = a.Berat * b.Tarif, ItemTransaksiId = a.ItemTransaksiId, Jenis = b, Jumlah = a.Jumlah, JenisId = a.JenisId, TransaksiId = a.TransaksiId };
                    selected.Items = new ObservableCollection<itemtransaksi>(items);
                    ItemsData.Clear();
                    foreach (var item in items)
                    {
                        ItemsData.Add(item);
                    }
                }
               
            }else
            {
                ItemsData.Clear();
                foreach (var item in selected.Items)
                {
                    ItemsData.Add(item);
                }
            }
            ItemsView.Refresh();
        }

        private void GetBiaya(transaksi selected)
        {

            if (selected != null)
            {
                if (selected.Biaya == null)
                {
                    selected.Biaya = new Biaya();
                    if (selected.Setting != null && selected.Items != null && selected.Items.Count > 0)
                    {
                        selected.Biaya.Total = selected.Items.Sum(O => O.Biaya);
                        selected.Biaya.Discount = selected.Setting.Discount;
                        selected.Biaya.GrandTotal = selected.Biaya.Total - (selected.Biaya.Total * selected.Biaya.Discount);

                    }
                }

            }

            if (selected.StatusPembayaran == StatusPembayaran.Belum)
            {
                selected.ButtonVisiblity = Visibility.Visible;
            }
            else
            {
                selected.ButtonVisiblity = Visibility.Hidden;
            }
        }



        #endregion

        #region Constructor
        public MainViewModels()
        {
            PengembalianCommand = new CommandHandler { CanExecuteAction = PengembalianValidation, ExecuteAction = PengembalianAction };
            AddNewOrderCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewOrderAction };
            BayarCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = BayarAction };
            PengerjaanCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = PengerjaanAction };
            ChangePasswordCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = ChangePasswordAction };
            AddNewUserCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddNewUserAction };
            this.collection = new Models.TransactionCollection();
            TransaksiView = (CollectionView)CollectionViewSource.GetDefaultView(collection.Transaksi);
            ItemsData = new ObservableCollection<itemtransaksi>();
            ItemsView = (CollectionView)CollectionViewSource.GetDefaultView(ItemsData);
            ItemsView.Refresh();
            TransaksiView.Refresh();
        }

        private void PengembalianAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    Selected.Pengembalian.PengembalianId = db.Pengembalians.InsertAndGetLastID(Selected.Pengembalian);
                    var updated = db.Transaksis.Update(O => new { O.StatusPengambilan }, new transaksi { StatusPengambilan = StatusPengambilan.Sudah }, O => O.TransaksiId == Selected.TransaksiId);
                    if (Selected.Pengembalian.PengembalianId > 0 && updated)
                    {
                        Selected.StatusPengambilan = StatusPengambilan.Sudah;
                        Selected.InputPengambilanVisiblity = Visibility.Collapsed;
                        MessageBox.Show("Data Tersimpan","Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        trans.Commit();

                    }else
                    {
                        throw new SystemException("Data Tidak tersimpan");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool PengembalianValidation(object obj)
        {
            if (Selected!=null&& Selected.Pengembalian != null && !string.IsNullOrEmpty(Selected.Pengembalian.Nama) && !string.IsNullOrEmpty(Selected.Pengembalian.NoKTP))
                return true;
            else
                return false;
        }



        #endregion

    }
}
