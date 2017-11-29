using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApp.Models
{
   public  class BillModel
    {
        public string NomorTransaksi { get; set; }
        public string RoomNumber { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double TaxValue { get; set; }
        public double GrandTotal { get; set; }
        


    }
}
