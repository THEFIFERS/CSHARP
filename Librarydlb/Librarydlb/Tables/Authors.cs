using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Tables
{
    internal class Authors : DataBaseTable
    {
        public int Author_Id { get; set; }
        public string Author_Name { get; set; }
        public int Status { get; set; }
    }
}
