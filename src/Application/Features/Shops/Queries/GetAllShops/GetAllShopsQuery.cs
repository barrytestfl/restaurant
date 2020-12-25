using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Attributes;
using Application.Common.Interfaces;
using Application.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Shops.Queries.GetAllShops
{
    [Cache]
    public class GetAllShopsQuery : IRequest<PagedResponse<IEnumerable<GetAllShopsVm>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    
    public class GetAllShopHandler : IRequestHandler<GetAllShopsQuery, PagedResponse<IEnumerable<GetAllShopsVm>>>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public GetAllShopHandler(IShopRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllShopsVm>>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllShopsParameter>(request);
            var product = await _shopRepository.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
            var shopViewModel = _mapper.Map<IEnumerable<GetAllShopsVm>>(product);
            return new PagedResponse<IEnumerable<GetAllShopsVm>>(shopViewModel, filter.PageNumber, filter.PageSize); 
        }
    }
}