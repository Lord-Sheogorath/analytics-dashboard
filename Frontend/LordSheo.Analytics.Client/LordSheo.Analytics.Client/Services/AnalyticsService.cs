using System.Net.Http.Json;
using LordSheo.Analytics.Client.Model;

namespace LordSheo.Analytics.Client.Services
{
	public class AnalyticsService
	{
		private readonly HttpClient _httpClient;

		public AnalyticsService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<AnalyticsEvent>> GetEventsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<AnalyticsEvent>>("api/analytics");
		}
	}
}