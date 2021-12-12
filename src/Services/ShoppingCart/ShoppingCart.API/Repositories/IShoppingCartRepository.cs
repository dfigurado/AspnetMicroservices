using System.Threading.Tasks;

namespace ShoppingCart.API.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Entities.ShoppingCart> GetShoppingCart(string userName);

        Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart);

        Task DeleteShoppingCart(string userName);
    }
}