using Application.Exceptions;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discount
{
    public class Update
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public int Amount { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var coupon = await _context.Coupons.FindAsync(request.Id);

                if (coupon == null)
                    throw new RestException(HttpStatusCode.NotFound, new { discount = "Not found" });

                coupon.ProductName = request.ProductName ?? coupon.ProductName;
                coupon.Description = request.Description ?? coupon.Description;
                coupon.Ammount = request.Amount;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}