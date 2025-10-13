using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscolar.Services.Models
{
    public class BaseResult
    {
        public bool Sucesso { get; set; }
        public string? MensagemErro { get; set; }
    }
}
