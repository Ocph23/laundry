using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("itemtransaksi")] 
     public class itemtransaksi:BaseNotifyProperty  
   {
        [PrimaryKey("ItemTransaksiId")]
        [DbColumn("ItemTransaksiId")]
        public int ItemTransaksiId
        {
            get { return _itemtransaksiid; }
            set
            {
                _itemtransaksiid = value;
                OnPropertyChange("ItemTransaksiId");
            }
        }

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

        [DbColumn("JenisId")]
        public int JenisId
        {
            get { return _jenisid; }
            set
            {
                _jenisid = value;
                OnPropertyChange("JenisId");
            }
        }

        [DbColumn("Jumlah")]
        public int Jumlah
        {
            get { return _jumlah; }
            set
            {
                _jumlah = value;
                OnPropertyChange("Jumlah");
            }
        }


        public virtual jenis Jenis
        {
            get { return _jenis; }
            set
            {
                _jenis = value;
                if(value!=null && Jumlah>0)
                {
                    Biaya = value.Tarif * Jumlah;
                }
                OnPropertyChange("Jenis");
            }
        }


        private double _biaya;

        public double Biaya
        {
            get { return _biaya; }
            set { _biaya = value; OnPropertyChange("Biaya"); }
        }

        public string NamaJenis { get; set; }
        public double Tarif { get; set; }


        private jenis _jenis;
        private int _itemtransaksiid;
        private int _transaksiid;
        private int _jenisid;
        private int _jumlah;
    

        
      }
}


