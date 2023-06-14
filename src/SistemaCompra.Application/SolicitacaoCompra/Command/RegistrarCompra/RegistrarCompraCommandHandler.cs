using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SC = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using Prod = SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Linq;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler: CommandHandler, IRequestHandler<RegistrarCompraCommand,bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;

        public RegistrarCompraCommandHandler(
            ISolicitacaoCompraRepository solicitacaoCompraRepository, 
            IProdutoRepository produtoRepository,
            IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SC.SolicitacaoCompra(request.Solicitante, request.Fornecedor);

            foreach (var (item, produto) in from item in request.Itens
                                            let produto = _produtoRepository.Obter(item.ProdutoId)
                                            select (item, produto))
            {
                solicitacaoCompra.AdicionarItem(produto, item.Quantidade);
            }

            solicitacaoCompra.RegistrarCompra(solicitacaoCompra.Itens);
            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}
