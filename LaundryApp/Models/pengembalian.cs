using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("pengembalian")] 
     public class pengembalian:BaseNotifyProperty  
   {
          [PrimaryKey("PengembalianId")] 
          [DbColumn("PengembalianId")] 
          public int PengembalianId 
          { 
               get{return _pengembalianid;} 
               set{ 
                      _pengembalianid=value; 
                     OnPropertyChange("PengembalianId");
                     }
          } 

          [DbColumn("TanggalPengambilan")] 
          public DateTime TanggalPengambilan 
          { 
               get{return _tanggalpengambilan;} 
               set{ 
                      _tanggalpengambilan=value; 
                     OnPropertyChange("TanggalPengambilan");
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

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("TransaksiId")] 
          public int TransaksiId 
          { 
               get{return _transaksiid;} 
               set{ 
                      _transaksiid=value; 
                     OnPropertyChange("TransaksiId");
                     }
          } 

          private int  _pengembalianid;
           private DateTime  _tanggalpengambilan;
           private string  _noktp;
           private string  _nama;
           private int  _transaksiid;
      }
}


