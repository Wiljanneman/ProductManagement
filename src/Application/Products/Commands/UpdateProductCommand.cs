using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Products.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands;
public class UpdateProductCommand : IRequest
{
    public ProductVM Product { get; set; }

    public class Handler : IRequestHandler<CreateProductCommand>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Domain.Entities.Product>(request.Product);
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}