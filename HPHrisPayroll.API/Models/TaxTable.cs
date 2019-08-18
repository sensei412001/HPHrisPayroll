using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class TaxTable
    {
        public int TaxTableId { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime EffectiveDateTo { get; set; }
    }
}
