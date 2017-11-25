using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
using System.Collections.ObjectModel;
using System.Windows;

namespace LaundryApp.Models 
{

    [TableName("transaksi")]
    public class transaksi : BaseNotifyProperty
    {
        [PrimaryKey("TransaksiId")]
        [DbColumn("TransaksiId")]
        public int TransaksiId
        {
            get { return _transaksiid; }
            set
            {
                _transaksiid = value;
                OnPropertyChange("TransaksiId");
            }
        }

        [DbColumn("Kode")]
        public string Kode
        {
            get { return _kode; }
            set
            {
                _kode = value;
                OnPropertyChange("Kode");
            }
        }

        [DbColumn("TanggalMasuk")]
        public DateTime TanggalMasuk
        {
            get { return _tanggalmasuk; }
            set
            {
                _tanggalmasuk = value;
                OnPropertyChange("TanggalMasuk");
            }
        }

        [DbColumn("StatusPembayaran")]
        public StatusPembayaran StatusPembayaran
        {
            get { return _statuspembayaran; }
            set
            {
                _statuspembayaran = value;
                OnPropertyChange("StatusPembayaran");
            }
        }

        [DbColumn("PelangganId")]
        public int PelangganId
        {
            get { return _pelangganid; }
            set
            {
                _pelangganid = value;
                OnPropertyChange("PelangganId");
            }
        }

        [DbColumn("UserId")]
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserId");
            }
        }

        [DbColumn("SettingId")]
        public int SettingId
        {
            get { return _settingid; }
            set
            {
                _settingid = value;
                OnPropertyChange("SettingId");
            }
        }

        [DbColumn("StatusPengerjaan")]
        public StatusPengerjaan StatusPengerjaan
        {
            get { return _statuspengerjaan; }
            set
            {
                _statuspengerjaan = value;
                OnPropertyChange("StatusPengerjaan");
            }
        }

        [DbColumn("StatusPengambilan")]
        public StatusPengambilan StatusPengambilan
        {
            get { return _statuspengambilan; }
            set
            {
                _statuspengambilan = value;
                if (value == StatusPengambilan.Belum)
                    InputPengambilanVisiblity = Visibility.Visible;
                else
                   InputPengambilanVisiblity = Visibility.Collapsed;
                OnPropertyChange("StatusPengambilan");
            }
        }

        private pelanggan pelanggan;

        public pelanggan Pelanggan
        {
            get { return pelanggan; }
            set { pelanggan = value; OnPropertyChange("Pelanggan"); }
        }

        private setting setting;

        public setting Setting
        {
            get { return setting; }
            set { setting = value; OnPropertyChange("Setting"); }
        }

        public Biaya Biaya
        {
            get
            {
               
                return _biaya;
            }
            set
            {
                _biaya = value;
                OnPropertyChange("Biaya");
            }
        }


        private DateTime tanggalPengambilan;

        public DateTime TanggalPengambilan
        {
            get {
                if(this.Setting!=null)
                {
                    tanggalPengambilan = TanggalMasuk.AddDays(Setting.LamaCuci);
                }
                return tanggalPengambilan; }
            set { tanggalPengambilan = value;
                OnPropertyChange("TanggalPengambilan");
            }
        }


        private Visibility _btnVisible;
        public Visibility ButtonVisiblity
        {
            get { return _btnVisible; }
            set
            {
                _btnVisible = value;
                OnPropertyChange("ButtonVisiblity");
            }
        }


        public Visibility InputPengambilanVisiblity
        {
            get {
              
                return _inputPengambilanVisiblity; }
            set
            {
                _inputPengambilanVisiblity = value;
                OnPropertyChange("InputPengambilanVisiblity");
            }
        }



        private Visibility _PengerjaanVisiblity;
        public Visibility PengerjaanVisiblity
        {
            get {

                if(this.StatusPengerjaan!= StatusPengerjaan.Selesai)
                {
                    _PengerjaanVisiblity = Visibility.Visible;
                }else
                    _PengerjaanVisiblity = Visibility.Hidden;
                return _PengerjaanVisiblity; }
            set
            {
                _btnVisible = value;
                OnPropertyChange("PengerjaanVisiblity");
            }
        }

        private bool _ambilSendiri;

        public bool AmbilSendiri
        {
            get { return _ambilSendiri; }
            set { _ambilSendiri = value;
                if(Pelanggan!=null && Pengembalian!=null)
                {
                    Pengembalian.Nama = Pelanggan.Nama;
                    Pengembalian.NoKTP = Pelanggan.NoKTP;
                    Pengembalian.TanggalPengambilan = DateTime.Now;
                }

                OnPropertyChange("AmbilSendiri"); }
        }


        public ObservableCollection<Models.itemtransaksi> Items { get; set; }
        public pengembalian Pengembalian { get {
                return _pengembalian;
            }  set {
                _pengembalian = value;
                OnPropertyChange("Pengembalian");
            } }

        private int _transaksiid;
        private string _kode;
        private DateTime _tanggalmasuk;
        private StatusPembayaran _statuspembayaran;
        private int _pelangganid;
        private int _userid;
        private int _settingid;
        private StatusPengerjaan _statuspengerjaan;
        private StatusPengambilan _statuspengambilan;
        private Biaya _biaya;
        private Visibility _inputPengambilanVisiblity;
        private pengembalian _pengembalian;
    }

}


