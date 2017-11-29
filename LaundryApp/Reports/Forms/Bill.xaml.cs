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
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        public Bill( int Id)
        {
            InitializeComponent();
            this.Id = Id;
            this.Loaded += Bill_Loaded;
        }

        public int Id { get; }

        private void Bill_Loaded(object sender, RoutedEventArgs e)
        {


            using (var db = new OcphDbContext())
            {
                var parent = (from a in db.Transaksis.Where(O => O.TransaksiId == Id)
                              join b in db.Settings.Select() on a.SettingId equals b.SettingId
                              join c in db.Users.Select() on a.UserId equals c.UserId
                              select new Models.BillModel
                              {
                                  NomorTransaksi = string.Format("{0:D5}",
                                    a.TransaksiId),
                                  RoomNumber = a.NomorKamar,
                                  Tax = b.Discount
                              }).ToList();


                var childs = (from a in db.ItemsTransaksi.Where(O => O.ItemTransaksiId == Id)
                              join b in db.Jenises.Select() on a.JenisId equals b.JenisId
                              select new Models.itemtransaksi { NamaJenis=b.Nama, Tarif=b.Tarif, Jenis = b, Biaya = b.Tarif * a.Jumlah, ItemTransaksiId = a.ItemTransaksiId, JenisId = a.JenisId, Jumlah = a.Jumlah, TransaksiId = a.TransaksiId }).ToList();

                foreach(var item in parent)
                {
                    item.SubTotal = childs.Sum(O => O.Biaya);
                    item.TaxValue =(double) item.SubTotal * (item.Tax / 100);
                    item.GrandTotal = item.SubTotal + item.TaxValue;
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource DataSet1 = new ReportDataSource();
                DataSet1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
                DataSet1.Value = parent;

                ReportDataSource DataSet2 = new ReportDataSource();
                DataSet2.Name = "DataSet2"; // Name of the DataSet we set in .rdlc
                DataSet2.Value = childs;


                

                reportViewer.LocalReport.ReportEmbeddedResource = "LoundryApp.Reports.Layout.Report1.rdlc";
                reportViewer.LocalReport.DataSources.Add(DataSet1);
                reportViewer.LocalReport.DataSources.Add(DataSet2);
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.RefreshReport();
            }


            
        }
    }
}
