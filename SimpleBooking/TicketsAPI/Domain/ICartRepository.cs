using System.Threading.Tasks;

namespace TicketsCartAPI.Domain
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> UpdateCartAsync(Cart basket);
        Task<bool> DeleteCartAsync(string id);
    }
}
