using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConexãoVerdeAppData.Entities
{
    public class Avaliacao
    {
        public int Id { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public int Nota { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}