﻿using MarketPlace.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Common.DTOs.RequestModels.Inventory
{
    public class DestinationRequest
    {
        public int? Id { get; set; }

        public required string Code { get; set; }
        public required string Name { get; set; }

        public RecordBase? RecordBase { get; set; }

    }
}
