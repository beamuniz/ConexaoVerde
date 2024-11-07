using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConexaoVerdeMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }     
        public string Telefone { get; set; }   
        public byte[]? FotoPerfil { get; set; }  
        public string Perfil { get; set; }
    }
}