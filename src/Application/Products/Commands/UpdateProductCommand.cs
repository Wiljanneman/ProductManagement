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
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands;
public class UpdateProductCommand : IRequest<bool>
{
    public ProductVM Product { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(a => a.Id == request.Product.Id);
            if (product == null)
            {
                return false;
            }
            Product productEntity = _mapper.Map<Domain.Entities.Product>(request.Product);
            _context.Products.Update(productEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}