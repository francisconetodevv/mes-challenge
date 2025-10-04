using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Core.Entities
{
    public class RegistroDeEtapa
    {
        public int Id { get; set; }
        public DateTime DateInitial { get; set; }
        public DateTime? DateFinal { get; set; }
        public string? Operator { get; set; }
        public string? Observations { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; } = null!;

        public int EtapaDoProcessoId { get; set; }
        public EtapaDoProcesso EtapaDoProcesso { get; set; } = null!;
    }
}