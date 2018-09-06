using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public interface ICartService
    {
        Task Checkout(string buyerId, Dictionary<int, int> tickets);
        // ToDo : use user identity Task<Cart> GetCart(ApplicationUser user);
        Task<Cart> GetCart(string userID);
    }
}
