using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("jenis")] 
     public class jenis:BaseNotifyProperty  
   {
          [PrimaryKey("JenisId")] 
          [DbColumn("JenisId")] 
          public int JenisId 
          { 
               get{return _jenisid;} 
               set{ 
                      _jenisid=value; 
                     OnPropertyChange("JenisId");
                     }
          }
        public string Kode
        {
            get { return string.Format("{0:D5}", JenisId); }
        }
        [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("Tarif")] 
          public double Tarif 
          { 
               get{return _tarif;} 
               set{ 
                      _tarif=value; 
                     OnPropertyChange("Tarif");
                     }
          } 

          [DbColumn("Aktif")] 
          public bool Aktif 
          { 
               get{return _aktif;} 
               set{ 
                      _aktif=value; 
                     OnPropertyChange("Aktif");
                     }
          }


        [DbColumn("Keterangan")]
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                _keterangan = value;
                OnPropertyChange("Keterangan");
            }
        }

        private int  _jenisid;
           private string  _nama;
           private double  _tarif;
           private bool  _aktif;
        private string _keterangan;
    }
}


