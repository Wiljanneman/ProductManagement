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
public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public DeleteProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _context.Products.FirstOrDefault(a => a.Id == request.Id);
            if (product == null)
            {
                return Unit.Value;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}