using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Api.DTOs
{
    public class CreateProductionOrderDTO
    {
        public required string OrderCode { get; set; }
        public decimal QtyPlanned { get; set; }
        public int ProductId { get; set; }
    }
}