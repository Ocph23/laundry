using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryApp.Models;

namespace LaundryApp.Viewmodels
{
  public  class LoginViewModel :Models.user
    {
        public LoginViewModel()
        {
            LoginCommand = new CommandHandler { CanExecuteAction = LoginValidation, ExecuteAction=LoginAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        public void LoginAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var user = db.Users.Where(O => O.UserName == this.UserName).FirstOrDefault();
                    if (user == null)
                        throw new SystemException("Anda Tidak Memiliki Akses, Hubungi Operator");
                    else
                    {
                        if (user.Password != this.Password)
                            throw new SystemException("Password Anda Salah Coba Ulangi Lagi ...!");
                        else
                        {
                            this.UserLogin = user;
                            this.WindowClose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageError = ex.Message;
                }
            }
        }

        private string _message;

        public string MessageError
        {
            get { return _message; }
            set { _message = value; OnPropertyChange("MessageError"); }
        }

        private bool LoginValidation(object obj)
        {
            if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
            {
                return false;
            }
            else
                return true;
        }

        public Action WindowClose { get;  set; }
        public CommandHandler LoginCommand { get; }
        public CommandHandler CancelCommand { get; }
        public user UserLogin { get; private set; }
    }
}
