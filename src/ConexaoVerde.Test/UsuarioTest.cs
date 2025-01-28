using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Business.Interfaces;
using ConexaoVerde.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ConexaoVerde.Test;

public class UsuarioTest
{
    [Fact]
    public async Task CadastroClienteTest()
    {
        // Arrange
        var mockCliente = new Mock<IClienteBusiness>();
        var mockUsuario = new Mock<IUsuarioBusiness>();
        var controller = new UsuarioController(null, mockCliente.Object, mockUsuario.Object);

        var usuarioModel = new UsuarioModel
        {
            Id = 1,
            Email = "joao.silva@test.com",
            Senha = "Senha123",
            Telefone = "11999999999",
            Perfil = "Cliente",
            ClienteModel = new ClienteModel
            {
                Nome = "João",
                Sobrenome = "Silva",
                Cpf = "12345678900"
            }
        };

        // Act
        var result = await controller.Cadastro(usuarioModel, null);

        // Assert
        mockCliente.Verify(c => c.RegistrarCliente(It.Is<UsuarioModel>(u =>
            u.Email == "joao.silva@test.com" &&
            u.ClienteModel.Nome == "João" &&
            u.ClienteModel.Cpf == "12345678900"
        )), Times.Once);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Login", redirectResult.ActionName);
    }

    [Fact]
    public async Task CadastroFuncionarioTest()
    {
        // Arrange
        var mockFuncionario = new Mock<IFornecedorBusiness>();
        var mockUsuario = new Mock<IUsuarioBusiness>();
        var controller = new UsuarioController(mockFuncionario.Object, null, mockUsuario.Object);

        var usuarioModel = new UsuarioModel
        {
            Id = 1,
            Email = "natureza.produtos@test.com",
            Senha = "Senha123",
            Telefone = "11999999999",
            Perfil = "Fornecedor",
            FornecedorModel = new FornecedorModel
            {
                RazaoSocial = "Natureza Produtos Ltda",
                NomeFantasia = "Natureza Produtos",
                Cnpj = "94.908.213/0001-60",
                Descricao = "Alimentos Orgânicos"
            }
        };

        // Act
        var result = await controller.Cadastro(usuarioModel, null);

        // Assert
        mockFuncionario.Verify(c => c.RegistrarFornecedor(It.Is<UsuarioModel>(u =>
            u.Email == "natureza.produtos@test.com" &&
            u.FornecedorModel.RazaoSocial == "Natureza Produtos Ltda" &&
            u.FornecedorModel.Cnpj == "94.908.213/0001-60"
        )), Times.Once);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Login", redirectResult.ActionName);
    }
}