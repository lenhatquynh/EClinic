using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Paging
{
    public class PaginationModel<T> where T : class
    {
        public T PaginationData { get; set; }
        public PaginationResponseHeader PaginationResponseHeader { get; set; }
    }
}
