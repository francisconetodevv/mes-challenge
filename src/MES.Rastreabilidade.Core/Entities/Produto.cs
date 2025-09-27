using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Rastreabilidade.Core.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
    }
}