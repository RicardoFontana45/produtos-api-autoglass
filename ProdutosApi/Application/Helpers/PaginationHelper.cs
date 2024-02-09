using System.Collections.Generic;

namespace ProdutosApi.Application.Helpers
{
    public class PaginationHelper<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
