using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Api.DTOs
{
    public class FinishBatchDTO
    {
        public decimal QtyProduced { get; set; }
        public string? Observations { get; set; }
    }
}