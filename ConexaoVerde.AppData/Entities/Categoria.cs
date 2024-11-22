using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string NomeCategoria { get; set; }
    }
}