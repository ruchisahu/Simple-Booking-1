using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public class EventManagementService : IEventManagementService
    {
        private readonly IOptionsSnapshot<AppSettings> settings;
        private readonly IHttpClient apiClient;
        private readonly string remoteServiceBaseUrl;

        public EventManagementService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient)
        {
            this.settings = settings;
            this.apiClient = httpClient;
            this.remoteServiceBaseUrl = $"{settings.Value.EventManagementUrl}/api/EventManagement";
        }

        public async Task<Event> GetEvent(int eventId)
        {
            var getEventUri = ApiPaths.EventManagement.GetEvent(remoteServiceBaseUrl, eventId);
            var data = await apiClient.GetStringAsync(getEventUri);
            var eventData = JsonConvert.DeserializeObject<Event>(data);
            return eventData;
        }
    }
}
