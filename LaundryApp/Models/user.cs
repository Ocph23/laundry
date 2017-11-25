using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace LaundryApp.Models 
{ 
     [TableName("user")] 
     public class user:BaseNotifyProperty  
   {
          [PrimaryKey("UserId")] 
          [DbColumn("UserId")] 
          public int UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          } 

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value; 
                     OnPropertyChange("Name");
                     }
          } 

          [DbColumn("Password")] 
          public string Password 
          { 
               get{return _password;} 
               set{ 
                      _password=value; 
                     OnPropertyChange("Password");
                     }
          } 

          [DbColumn("UserName")] 
          public string UserName 
          { 
               get{return _username;} 
               set{ 
                      _username=value; 
                     OnPropertyChange("UserName");
                     }
          } 

          private int  _userid;
           private string  _name;
           private string  _password;
           private string  _username;
      }
}


