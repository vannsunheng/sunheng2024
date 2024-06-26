using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        private const int MaxPageSize=50;
        public int PageIndex{set; get; } =1;   
        private int _pageSize=6;
      

        public int PageSize{
            get=>_pageSize;
            set=>_pageSize=(value>MaxPageSize) ? MaxPageSize : value;

        }
        public int? BrandId{set;get;}
        public int? TypeId { get; set; }
        public string sort { get; set; }
        private string _search;
        public string Search { 
            get=>_search; 
            set=>_search=value.ToLower();}
    }
}