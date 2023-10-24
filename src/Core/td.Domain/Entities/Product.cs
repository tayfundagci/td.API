﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Common;

namespace td.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; } 
    }
}
