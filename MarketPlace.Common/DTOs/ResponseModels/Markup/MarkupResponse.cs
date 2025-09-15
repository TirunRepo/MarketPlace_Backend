using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.ResponseModels.Markup
{
    public class MarkupResponse
    {
        public int? Id { get; set; }
        public decimal? MinMarkup { get; set; }
        public decimal? MaxMarkup { get; set; }
        public decimal? MinBaseFare { get; set; }
        public decimal? MaxBaseFare { get; set; }
        public decimal? MarkupPercentage { get; set; }
        public int? SupplierId { get; set; }
        public int? SailingId { get; set; }
        public bool IsActive { get; set; }
    }
}
