using System;
using System.Windows;
using System.Linq;

namespace LaundryApp.Viewmodels
{
    internal class SettingViewModel:Models.setting
    {

        #region Constructor
        public SettingViewModel()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidation, ExecuteAction = SaveAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };

            using (var db = new OcphDbContext())
            {
                var result = db.Settings.Where(O => O.Aktif==true).FirstOrDefault();
                if(result!=null)
                {
                    this.SettingId = result.SettingId;
                    this.Discount = result.Discount;
                    this.Aktif = result.Aktif;
                    this.LamaCuci = result.LamaCuci;
                }
            }
        }

       
        #endregion

        #region Action
        private void SaveAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var result = db.Settings.Where(O => O.Aktif==true).FirstOrDefault();
                    if(result!=null)
                    {
                        var changeOld = db.Settings.Update(O => new { O.Aktif }, new Models.setting { Aktif = false }, O => O.SettingId == result.SettingId);
                        if(!changeOld)
                            throw new SystemException("Data Tidak Tersimpan");
                    }

                    var setting = new Models.setting { Aktif = true, Discount = this.Discount, LamaCuci = this.LamaCuci, SettingId = 0 };
                    this.SettingId = db.Settings.InsertAndGetLastID(setting);
                    if (this.SettingId > 0 )
                    {

                        trans.Commit();
                        MessageBox.Show("Data  Tersimpan", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        throw new SystemException("Data Tidak Tersimpan");

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
          
        }
       
        #endregion

        #region Validation
        private bool SaveValidation(object obj)
        {
            if (LamaCuci > 0)
                return true;
            else
             return false;
        }
        #endregion


        #region Properties
        public Action WindowClose { get; internal set; }
        public CommandHandler SaveCommand { get; }
        public CommandHandler CancelCommand { get; }
        #endregion


    }
}