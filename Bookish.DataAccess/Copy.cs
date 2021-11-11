using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class Copy
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public int user_id { get; set; }
        public DateTime due_date { get; set; }


    }
}
