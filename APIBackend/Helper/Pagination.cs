using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackend.Helper
{
    public class Pagination<T> where T: class
    {

        public Pagination(int pageIndex, int count, int pageSize, IReadOnlyList<T> data)
        {
            this.PageIndex = pageIndex;
            this.Count = count;
            this.PageSize = pageSize;
            this.Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}