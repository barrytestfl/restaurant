using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Shops.Commands.DeleteShop
{
    public class DeleteShopCommand : IRequest<Shop>
    {
        public DeleteShopCommand(Shop shop)
        {
            Shop = shop;
        }

        public Shop Shop { get; }
    }
    
    public class DeleteShopHandler : IRequestHandler<DeleteShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository;

        public DeleteShopHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Shop> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            return await _shopRepository.DeleteAsync(request.Shop);
        }
    }
}