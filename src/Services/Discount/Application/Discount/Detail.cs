using Application.Dto;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discount
{
    public class Detail
    {
        public class Query : IRequest<DiscountDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DiscountDto>
        {
            public Handler(DataContext context, IMapper mapper)
            {
                _Context = context;
                _Mapper = mapper;
            }

            public DataContext _Context { get; }
            public IMapper _Mapper { get; }

            public async Task<DiscountDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var discount = await _Context.Coupons.FindAsync(request.Id);

                if (discount == null)
                    throw new RestException(HttpStatusCode.NotFound, new { discount = "Not found" });

                var discountReturned = _Mapper.Map<Coupon, DiscountDto>(discount);

                return discountReturned;
            }
        }
    }
}