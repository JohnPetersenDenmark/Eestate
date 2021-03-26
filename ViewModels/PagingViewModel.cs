using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{

   
    public class PagingViewModel<T>
    {
       

        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
