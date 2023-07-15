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
public class GetProductByIdQuery : IRequest<ProductVM>
{
    public int Id { get; set; }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVM>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            ProductVM product = _mapper.Map<ProductVM>(await _context.Products.FirstOrDefaultAsync(a => a.Id == request.Id));
            return product;
        }
    }
}