using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarydlb.Helpers
{
    internal class ENums
    {
        public enum DBTableType
        {
            Books = 1,
            Booking = 2,
            User = 3,
            Author = 4,
            Publisher = 5,
            Genre = 6,
            Exit = 0
        }
        public enum DBOperationType
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Get = 4,
            List = 5,
            Exit = 0
        }
    }
}
