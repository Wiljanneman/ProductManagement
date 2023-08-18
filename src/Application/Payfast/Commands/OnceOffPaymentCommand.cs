using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Products.Common;
using Application.Users.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using PayFast;

namespace Application.Payfast.Commands;
public class OnceOffPaymentCommand : IRequest<string>
{
    public ProductVM Product { get; set; }
    public UserVM User { get; set; }

    public class OnceOffPaymentCommandHandler : IRequestHandler<OnceOffPaymentCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly PayFastSettings payFastSettings;

        public OnceOffPaymentCommandHandler(IApplicationDbContext context, IMapper mapper, IOptions<PayFastSettings> payFastSettings)
        {
            _context = context;
            _mapper = mapper;
            this.payFastSettings = payFastSettings.Value;
        }
        public async Task<string> Handle(OnceOffPaymentCommand request, CancellationToken cancellationToken)
        {
            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
            // Merchant Details
            onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
            onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
            onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
            onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
            onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            onceOffRequest.email_address = request.User.Email;

            // Transaction Details
            onceOffRequest.m_payment_id = request.User.Id;
            onceOffRequest.amount = (double)request.Product.IncludingVATAmount;
            onceOffRequest.item_name = request.Product.Name;
            onceOffRequest.item_description = request.Product.Description;

            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = request.User.Email;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

            return redirectUrl;
        }
    }
}