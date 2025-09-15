using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Entities.Inventory
{
    public class SailDate : EntityBase
    {
        [Key]
        public int SaleDateId { get; set; }
        [ForeignKey("CruiseShipID")]
        public int CruiseShipId { get; set; }
        public virtual CruiseShip CruiseShip { get; set; }
        //
        public DateTime SailDateTime { get; set; }

        public Boolean IsActive { get; set; }
    }
}
