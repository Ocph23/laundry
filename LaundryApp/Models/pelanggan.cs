using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("pelanggan")] 
     public class pelanggan:BaseNotifyProperty  
   {
          [PrimaryKey("PelangganId")] 
          [DbColumn("PelangganId")] 
          public int PelangganId 
          { 
               get{return _pelangganid;} 
               set{ 
                      _pelangganid=value; 
                     OnPropertyChange("PelangganId");
                     }
          }

        public string Kode
        {
            get { return string.Format("{0:D5}",PelangganId); }
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

          [DbColumn("NoKTP")] 
          public string NoKTP 
          { 
               get{return _noktp;} 
               set{ 
                      _noktp=value; 
                     OnPropertyChange("NoKTP");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

          [DbColumn("JenisKelamin")] 
          public Gender JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{ 
                      _jeniskelamin=value; 
                     OnPropertyChange("JenisKelamin");
                     }
          } 

          [DbColumn("telepon")] 
          public string telepon 
          { 
               get{return _telepon;} 
               set{ 
                      _telepon=value; 
                     OnPropertyChange("telepon");
                     }
          } 

          [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get
            {
                if (_tanggal == new DateTime())
                {
                    _tanggal = DateTime.Now;
                }
                return _tanggal;

            }
            set
            {
                _tanggal = value;
                OnPropertyChange("Tanggal");
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

          private int  _pelangganid;
           private string  _nama;
           private string  _noktp;
           private string  _alamat;
           private Gender  _jeniskelamin;
           private string  _telepon;
           private DateTime  _tanggal;
           private bool _aktif;
      }
}


