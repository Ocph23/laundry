using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApp.Models
{
   public class SummaryOfDay
    {
        public DateTime Tanggal { get; set; }
        public string Petugas { get; set; }
        public string Nomor { get; set; }
        public string Kamar { get; set; }
        public string KodeJenis { get; set; }
        public double Biaya { get; set; }
        public setting Setting { get; set; }
        public int TransaksiId { get; set; }
    }
}
