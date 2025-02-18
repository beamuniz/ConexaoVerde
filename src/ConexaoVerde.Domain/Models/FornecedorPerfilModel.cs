﻿using ConexaoVerde.Domain.Entities;

namespace ConexaoVerde.Domain.Models;

public class FornecedorPerfilModel
{
    public FornecedorModel Fornecedor { get; set; }
    public List<ProdutoModel> Produtos { get; set; }
    public UsuarioModel Usuario { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; }
}