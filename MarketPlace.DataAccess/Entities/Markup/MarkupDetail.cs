using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Markup
{
    [Table("MarkupDetail")]
    public class MarkupDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal? MinMarkup { get; set; }
        public decimal? MaxMarkup { get; set; }
        public decimal? MinBaseFare { get; set; }
        public decimal? MaxBaseFare { get; set; }
        public decimal? MarkupPercentage { get; set; }
        public int? SupplierId { get; set; }
        public int? SailingId { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
