using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("setting")] 
     public class setting:BaseNotifyProperty  
   {
          [PrimaryKey("SettingId")] 
          [DbColumn("SettingId")] 
          public int SettingId 
          { 
               get{return _settingid;} 
               set{ 
                      _settingid=value; 
                     OnPropertyChange("SettingId");
                     }
          } 

          [DbColumn("Discount")] 
          public double Discount 
          { 
               get{return _discount;} 
               set{ 
                      _discount=value; 
                     OnPropertyChange("Discount");
                     }
          } 

          [DbColumn("LamaCuci")] 
          public int LamaCuci 
          { 
               get{return _lamacuci;} 
               set{ 
                      _lamacuci=value; 
                     OnPropertyChange("LamaCuci");
                     }
          }

        [DbColumn("Aktif")]
        public bool Aktif
        {
            get { return _aktif; }
            set
            {
                _aktif = value;
                OnPropertyChange("Aktif");
            }
        }

        private int  _settingid;
           private double  _discount;
           private int  _lamacuci;
        private bool _aktif;
    }
}


