using AutoMapper;
using Application.Dto;
using Application.Exceptions;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discount
{
    public class ProductDiscount
    {
        public class Query : IRequest<DiscountDto>
        {
            public string ProductName { get; set; }
        }

        public class Handler : IRequestHandler<Query, DiscountDto>
        {
            private DataContext _Context;
            private IMapper _Mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _Context = context;
                _Mapper = mapper;
            }

            public async Task<DiscountDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _Context.Coupons.FindAsync(request.ProductName);

                if (product == null)
                    throw new RestException(HttpStatusCode.NotFound, new { product = "Not found" });

                var discountReturned = _Mapper.Map<Coupon, DiscountDto>(product);

                return discountReturned;
            }
        }
    }
}