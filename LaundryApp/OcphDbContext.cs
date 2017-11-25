using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LaundryApp
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IRepository<Models.pelanggan> Pelanggans { get { return new Repository<Models.pelanggan>(this); } }
        public IRepository<Models.itemtransaksi> ItemsTransaksi { get { return new Repository<Models.itemtransaksi>(this); } }
        public IRepository<Models.jenis> Jenises { get { return new Repository<Models.jenis>(this); } }
        public IRepository<Models.pengembalian> Pengembalians{ get { return new Repository<Models.pengembalian>(this); } }
        public IRepository<Models.setting> Settings { get { return new Repository<Models.setting>(this); } }
        public IRepository<Models.transaksi> Transaksis { get { return new Repository<Models.transaksi>(this); } }
        public IRepository<Models.user> Users { get { return new Repository<Models.user>(this); } }


        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
