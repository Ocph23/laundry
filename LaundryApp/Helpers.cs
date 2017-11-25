using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApp
{
    public class Helpers
    {
        internal static string GenerateCode()
        {

            using (var db = new OcphDbContext())
            {
                var id = 1;
                var item = db.Transaksis.GetLastItem();
                
                if(item!=null)
                {
                    id = item.TransaksiId+1;
                }

                return string.Format("{0:D5}",id);
            }
        }
    }
}
