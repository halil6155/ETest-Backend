using System.Collections.Generic;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class PaginationListModel<T> : IModel where T : class, IDto, new()
    {
        public int TotalCount { get; set; }
        public List<T> ListItems { get; set; }

    }
}