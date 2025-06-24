using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Tables
{
    internal class Booking : DataBaseTable
    {
        public int Book_Id { get; set; }

        public int Booking_Id { get; set; }
        public int User_Id { get; set; }

        public string Start_Date { get; set; }

        public string End_Date { get; set; }
    }
}
