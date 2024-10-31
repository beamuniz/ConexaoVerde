using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexãoVerdeAppData.Entities;
using ConexãoVerdeMVC.Models;

namespace ConexãoVerdeMVC.Business.Interfaces
{
    public interface IUsuarioBusiness
    {
        Task<Usuario> Login(UsuarioModel usuarioModel);
        UsuarioModel Logout(string email, string senha);
        void RegistrarUsuario(UsuarioModel usuarioModel);
        Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel);
        List<UsuarioModel> Favoritos(int id);
    }
}