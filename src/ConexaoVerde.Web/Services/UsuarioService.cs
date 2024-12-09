using ConexaoVerde.Web.Extensions;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Services;

public class UsuarioService
{
    public async Task VerificarAtualizarUsuario(UsuarioModel usuarioModel, IFormFile fotoPerfil, UsuarioModel usuarioExistente)
    {
        if (!string.IsNullOrEmpty(usuarioModel.Email))
            usuarioExistente.Email = usuarioModel.Email;

        if (!string.IsNullOrEmpty(usuarioModel.Telefone))
            usuarioExistente.Telefone = usuarioModel.Telefone;

        if (fotoPerfil is { Length: > 0 })
            usuarioExistente.FotoPerfil = await fotoPerfil.OpenReadStream().ReadToEndAsync();
    }

    public void VerificarAtualizarCliente(UsuarioModel usuarioModel, UsuarioModel usuarioExistente)
    {
        usuarioExistente.ClienteModel ??= new ClienteModel();

        if (!string.IsNullOrEmpty(usuarioModel.ClienteModel.Cpf))
            usuarioExistente.ClienteModel.Cpf = usuarioModel.ClienteModel.Cpf;

        if (!string.IsNullOrEmpty(usuarioModel.ClienteModel.Nome))
            usuarioExistente.ClienteModel.Nome = usuarioModel.ClienteModel.Nome;

        if (!string.IsNullOrEmpty(usuarioModel.ClienteModel.Sobrenome))
            usuarioExistente.ClienteModel.Sobrenome = usuarioModel.ClienteModel.Sobrenome;
    }

    public void VerificarAtualizarFornecedor(UsuarioModel usuarioModel, UsuarioModel usuarioExistente)
    {
        usuarioExistente.FornecedorModel ??= new FornecedorModel();

        if (!string.IsNullOrEmpty(usuarioModel.FornecedorModel.RazaoSocial))
            usuarioExistente.FornecedorModel.RazaoSocial = usuarioModel.FornecedorModel.RazaoSocial;

        if (!string.IsNullOrEmpty(usuarioModel.FornecedorModel.NomeFantasia))
            usuarioExistente.FornecedorModel.NomeFantasia = usuarioModel.FornecedorModel.NomeFantasia;

        if (!string.IsNullOrEmpty(usuarioModel.FornecedorModel.Cnpj))
            usuarioExistente.FornecedorModel.Cnpj = usuarioModel.FornecedorModel.Cnpj;

        if (!string.IsNullOrEmpty(usuarioModel.FornecedorModel.Descricao))
            usuarioExistente.FornecedorModel.Descricao = usuarioModel.FornecedorModel.Descricao;
    }
}