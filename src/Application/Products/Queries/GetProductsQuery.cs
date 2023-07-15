using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Products.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries;
public class GetProductsQuery : IRequest<List<ProductVM>>
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductVM>>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductVM>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            List<ProductVM> products = _mapper.Map<List<ProductVM>>(await _context.Products.ToListAsync());
            return products;
        }
    }
}