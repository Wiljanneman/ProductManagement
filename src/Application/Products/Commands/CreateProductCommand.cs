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
public class CreateProductCommand: IRequest
{
    public ProductVM Product { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Domain.Entities.Product>(request.Product);
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}