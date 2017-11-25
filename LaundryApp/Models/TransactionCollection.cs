﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LaundryApp.Models
{
    internal class TransactionCollection
    {
        public ObservableCollection<Models.transaksi> Transaksi { get; set; }

        public TransactionCollection()
        {
            using (var db = new OcphDbContext())
            {
                var data = from a in db.Transaksis.Select()
                           join b in db.Pelanggans.Select() on a.PelangganId equals b.PelangganId
                           join c in db.Settings.Select() on a.SettingId equals c.SettingId
                           select new transaksi
                           {
                               Kode = a.Kode,
                               Pelanggan = b,
                               PelangganId = a.PelangganId,
                               SettingId = a.SettingId,
                               StatusPembayaran = a.StatusPembayaran,
                               StatusPengambilan = a.StatusPengambilan,
                               StatusPengerjaan = a.StatusPengerjaan,
                               TanggalMasuk = a.TanggalMasuk,
                               TransaksiId = a.TransaksiId,Setting=c,
                               UserId = a.UserId
                           };
                           
                Transaksi = new ObservableCollection<transaksi>(data.OrderByDescending(O=>O.TanggalMasuk));
            }
        }

        
    }
}