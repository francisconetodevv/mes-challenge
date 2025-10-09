using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Api.DTOs
{
    public class AdvanceBatchStepDTO
    {
        public int NextStepId { get; set; }
        public string? Operator { get; set; }
        public string? Observations { get; set; }
    }
}