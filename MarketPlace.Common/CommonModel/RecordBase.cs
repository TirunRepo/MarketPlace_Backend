using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.CommonModel
{
    public class RecordBase
    {
        public IdNameModel<int> CreatedBy { get; set; }
        public IdNameModel<int> UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; }
    }

    public class IdNameModel<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
