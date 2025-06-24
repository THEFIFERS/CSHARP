using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Tables
{
    internal class Publishers : DataBaseTable
    {
        public int Publisher_Id { get; set; }

        public string Publisher_Name { get; set; }

        public int Status { get; set; }
    }
}
