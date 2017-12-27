using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaundryApp.Reports.Forms
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {
        public Summary()
        {
            InitializeComponent();
            this.Loaded += Summary_Loaded;
        }

        private void Summary_Loaded(object sender, RoutedEventArgs e)
        {
            this.Refreshreport(DateTime.Now);
        }

        private void Refreshreport(DateTime now)
        {
            using (var db = new OcphDbContext())
            {

                var parent = (from a in db.Transaksis.Where(O => O.TanggalMasuk.Day==now.Day && O.TanggalMasuk.Month==now.Month && O.TanggalMasuk.Year==now.Year)
                              join b in db.Settings.Select() on a.SettingId equals b.SettingId
                              join c in db.Users.Select() on a.UserId equals c.UserId
                              select new Models.SummaryOfDay
                              {
                                   Nomor = string.Format("{0:D5}",    
                                    a.TransaksiId), Tanggal=a.TanggalMasuk,Setting = b, Petugas=c.Name,
                                    TransaksiId=a.TransaksiId, Kamar= a.NomorKamar
                                 
                              }).ToList();


                foreach (var item in parent)
                {

                    var childs = (from a in db.ItemsTransaksi.Where(O => O.TransaksiId == item.TransaksiId)
                                  join b in db.Jenises.Select() on a.JenisId equals b.JenisId
                                  select new Models.itemtransaksi { NamaJenis = b.Nama, Tarif = b.Tarif, Jenis = b, Biaya = b.Tarif * a.Jumlah, ItemTransaksiId = a.ItemTransaksiId, JenisId = a.JenisId, Jumlah = a.Jumlah, TransaksiId = a.TransaksiId }).ToList();
                    var biaya = childs.Sum(O => O.Biaya);
                    var tax = biaya * (item.Setting.Discount / 100);
                    item.Biaya = biaya + tax;
                    var sb = new StringBuilder();
                    foreach(var item2 in childs)
                    {
                        sb.Append(string.Format(" {0:D4} ", item2.ItemTransaksiId));
                    }
                    item.KodeJenis = sb.ToString();

                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
                DataSet1.Value = parent;




                reportViewer.LocalReport.ReportEmbeddedResource = "LoundryApp.Reports.Layout.Report2.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.RefreshReport();



            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dp = (DatePicker)sender;
            this.Refreshreport(dp.SelectedDate.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
