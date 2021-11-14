﻿using System;

namespace ProductManagement.API.DTOs.Output
{
    public class ProdutoFornecedorOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public FornecedorOutput Fornecedor { get; set; }
    }
}
