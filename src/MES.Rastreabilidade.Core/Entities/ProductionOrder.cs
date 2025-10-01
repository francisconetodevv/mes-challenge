using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Core.Enums;

namespace MES.Rastreabilidade.Core.Entities
{
    public class ProductionOrder
    {
        public int Id { get; set; }
        public required string OrderCode { get; set; }
        public decimal QtyPlanned { get; set; }
        public ProductionOrderStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }
        public Produto Produto { get; set; } = null!;
    }
}