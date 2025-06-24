using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Tables
{
    internal class User : DataBaseTable
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Double Phone_No { get; set; }


    }
}
