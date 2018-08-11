using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.ViewModel
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class

    {

        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; set; }



        public PaginatedItemsViewModel(int pageIndex, int pageSize, long Count, IEnumerable<TEntity> Data)

        {

            this.PageSize = PageSize;

            this.PageIndex = pageIndex;

            this.Count = Count;

            this.Data = Data;

        }
    }
}
