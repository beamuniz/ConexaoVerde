using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IEnderecoBusiness
{
    Task<Endereco> CriarEndereco(EnderecoModel endereco);
    Task<Endereco> AtualizarEndereco(int id, EnderecoModel endereco);
    Task<bool> DeletarEndereco(int id);
    Task<Endereco> ObterEnderecoPorId(int id);
    Task<Endereco> ObterEnderecoPorBairro(string bairro);
    Task<Endereco> ObterEnderecoPorCidade(string cidade);
}