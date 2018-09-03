using System.Threading.Tasks;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public interface IEventManagementService
    {
        Task<Event> GetEvent(int eventId);
    }
}
