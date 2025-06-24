using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Tables
{
    internal class Books : DataBaseTable
    {
        public string Book_name { get; set; }
        public int Book_Id { get; set; }
        public int Author_Id { get; set; }
        public int Genre_Id { get; set; }
        public int Publisher_Id { get; set; }

        public int Total_Quantity { get; set; }

        public int Available_Quantity { get; set; }





    }
}
