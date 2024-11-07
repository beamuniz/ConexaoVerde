using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConexaoVerdeAppData.Entities
{
    public class Fornecedor : Usuario
    {
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        public string Descricao { get; set; }
        public Endereco Endereco { get; set;}
        public List<Produto> Produtos { get; set;}
    }
}