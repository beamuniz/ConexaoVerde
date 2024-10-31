using System.Threading.Tasks;
using ConexãoVerdeAppData.Context;
using ConexãoVerdeAppData.Entities;
using ConexãoVerdeMVC.Business.Interfaces;
using ConexãoVerdeMVC.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ConexãoVerdeMVC.Business.Services
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly DbContextConfig _context;

        public UsuarioBusiness(DbContextConfig dbContextConfig)
        {
            _context = dbContextConfig;

        }

        public async Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuarioModel.Id);

            if (usuarioExistente != null)
            {
                usuarioExistente.Email = usuarioModel.Email;
                var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);
                usuarioExistente.Senha = senhaHash; 
                usuarioExistente.Telefone = usuarioModel.Telefone;
                usuarioExistente.FotoPerfil = usuarioModel.FotoPerfil;

                _context.Usuarios.Update(usuarioExistente);

                await _context.SaveChangesAsync();
            }

            return usuarioExistente;
        }

        public List<UsuarioModel> Favoritos(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Login(UsuarioModel usuarioModel)
        {
            var usuario = await _context.Usuarios
                .Where(a => a.Email == usuarioModel.Email && a.Senha == usuarioModel.Senha)
                .FirstOrDefaultAsync();

            return usuario;
        }

        public UsuarioModel Logout(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public void RegistrarUsuario(UsuarioModel usuarioModel)
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);
            var usuario = new Usuario
            {
                Email = usuarioModel.Email,
                Senha = senhaHash,
                Telefone = usuarioModel.Telefone,
                FotoPerfil = usuarioModel.FotoPerfil,
                Perfil = usuarioModel.Perfil
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}