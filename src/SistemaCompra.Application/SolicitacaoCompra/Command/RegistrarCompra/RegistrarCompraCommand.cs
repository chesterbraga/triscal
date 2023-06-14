using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarItemCommand : IRequest<bool>
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string Fornecedor { get; set; }
        public string Solicitante { get; set; }
        public IList<RegistrarItemCommand> Itens { get; set; }
    }
}