using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.ENums;

namespace Shared.DTOs.Product
{
    public class ProductQueryParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public ProductSortingOptions productSortingOptions { get; set; }

        public string? Search  { get; set; }

        private const int _defaultPageSize = 5;
        private const int _MaxPageSize = 10;
        private int pageSize;
        public int PageSize {
            get { return pageSize; }
            set { pageSize = value > 0 && value < _MaxPageSize ? value : _defaultPageSize; }
        }
        public int PageIndex { get; set; } = 1;

    }
}
