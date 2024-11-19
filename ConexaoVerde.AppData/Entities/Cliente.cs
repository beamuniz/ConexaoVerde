using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities
{
    public class Cliente : Usuario
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
  
    }
}