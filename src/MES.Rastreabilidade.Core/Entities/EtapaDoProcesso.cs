using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Core.Entities
{
    public class EtapaDoProcesso
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Order { get; set; }
    }
}