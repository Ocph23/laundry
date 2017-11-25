using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaundryApp.Viewmodels
{
    public class UserViewModel:Models.user
    {
        #region Properties
        private string _judul;

        public string Judul
        {
            get { return _judul; }
            set { _judul = value; OnPropertyChange("Judul"); }
        }


        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChange("NewPassword"); }
        }
        private string _confirmPassword;

        public string ConfromPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChange("ConfromPassword"); }
        }

        private string _oldPassword;

        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value;OnPropertyChange("OldPassword"); }
        }

        private Visibility _oldVisibiity;

        public Visibility OldVisibility
        {
            get { return _oldVisibiity; }
            set { _oldVisibiity = value; OnPropertyChange("OldVisibility"); }
        }

        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
        public Action WindowClose { get; internal set; }


        #endregion


        #region Constructore
        public UserViewModel()
        {
            this.Judul = "Tambah User";
            this.Load();
            OldVisibility = Visibility.Collapsed;
        }

      
        public UserViewModel(Models.user u)
        {
            OldVisibility = Visibility.Visible;

            this.Judul = "Edit Password";
            this.Password = u.Password;
            this.UserName = u.UserName;
            this.UserId = u.UserId;
            this.Load();
        }
        #endregion


        #region Method
        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidation, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        private void SaveAction(object obj)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    this.Password = NewPassword;
                    if (UserId <= 0)
                    {
                        UserId = db.Users.InsertAndGetLastID(this);
                        if (this.UserId > 0)
                        {
                            MessageBox.Show("Data  Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }else
                    {
                        var saved = db.Users.Update(O => new { O.Password }, new Models.user { Password = NewPassword },O=>O.UserId==UserId);
                        if (saved)
                        {
                            MessageBox.Show("Data  Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool SaveValidation(object obj)
        {
            if(UserId<=0)
            {
                if (NewPassword == ConfromPassword && !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Name))
                    return true;
                else
                    return false;
            }else
            {
                if (OldPassword == Password && NewPassword == ConfromPassword && !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Name))
                    return true;
                else
                    return false;
            }
        }

        #endregion

    }
}
