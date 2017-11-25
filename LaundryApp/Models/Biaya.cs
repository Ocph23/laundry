using System.Windows;

namespace LaundryApp.Models
{
    public class Biaya:DAL.BaseNotifyProperty
    {
        private double _total;
        private double _discount;
        private double _grandtotal;
       

        public double Total { get { return _total; } set { _total = value;OnPropertyChange("Total"); } }
        public double Discount { get { return _discount; } set { _discount= value; OnPropertyChange("Discount"); } }
        public double GrandTotal{ get { return _grandtotal; } set { _grandtotal = value; OnPropertyChange("GrandTotal"); } }
       
    }
}