using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMvcClient.Services
{
    public interface ICartService
    {
        Task Checkout(string buyerId, Dictionary<int, int> tickets);
    }
}
