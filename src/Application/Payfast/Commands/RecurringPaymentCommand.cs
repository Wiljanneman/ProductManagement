//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Application.Commons.Interfaces;
//using Application.Products.Common;
//using AutoMapper;
//using Domain.Entities;
//using MediatR;

//namespace Application.Payfast.Commands;
//public class RecurringPaymentCommand : IRequestHandler<RecurringPaymentCommand>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public RecurringPaymentCommandHandler(IApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }
//    public async Task<Unit> Handle(RecurringPaymentCommand request, CancellationToken cancellationToken)
//    {
//        Product product = _mapper.Map<Domain.Entities.Product>(request.Product);
//        _context.Products.Add(product);
//        await _context.SaveChangesAsync(cancellationToken);
//        return Unit.Value;
//    }
//}
//}