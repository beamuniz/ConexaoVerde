﻿using ConexaoVerde.Domain.Entities;

namespace ConexaoVerde.Domain.Models;

public class FornecedorModel
{
    public int Id { get; set; }
    public string RazaoSocial { get; set; }
    public string? Descricao { get; set; }
    public string NomeFantasia { get; set; }
    public string Cnpj { get; set; }
    public Endereco? Endereco { get; set; }
    public byte[]? FotoPerfil { get; set; }
}