using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Coupons.Any())
            {
                var discounts = new List<Coupon>
                {
                    new Coupon
                    {
                        ProductName= "IPhone X",
                        Description = "IPhone Discount",
                        Ammount = 150
                    },
                    new Coupon
                    {
                        ProductName = "Samsung 10",
                        Description = "Samsung Discount",
                        Ammount = 100
                    }
                };

                await context.Coupons.AddRangeAsync(discounts);
                await context.SaveChangesAsync();
            }
        }
    }
}
