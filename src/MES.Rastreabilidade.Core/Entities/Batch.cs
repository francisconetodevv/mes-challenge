using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Core.Enums;

namespace MES.Rastreabilidade.Core.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public required string BatchCode { get; set; }
        public decimal? QtyProduced { get; set; }
        public BatchStatus Status { get; set; }
        public DateTime DateInitial { get; set; }
        public DateTime? DateFinal { get; set; }

        // Relation with ProductionOrder
        public int ProductionOrderId { get; set; }
        public ProductionOrder ProductionOrder { get; set; } = null!;
    }
}