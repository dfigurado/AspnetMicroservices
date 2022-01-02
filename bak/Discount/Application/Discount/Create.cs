using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Domain;

namespace Application.Discount
{
    public class Create
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public int Ammount { get; set; }
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
                var coupon = new Coupon
                {
                    Id = request.Id,
                    ProductName = request.ProductName,
                    Description = request.Description,
                    Ammount = request.Ammount
                };

                _context.Coupons.Add(coupon);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Error saving discount");

            }
        }
    }
}
