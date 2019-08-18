using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Ssstable
    {
        public int SsstableId { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime EffectiveDateTo { get; set; }
    }
}
