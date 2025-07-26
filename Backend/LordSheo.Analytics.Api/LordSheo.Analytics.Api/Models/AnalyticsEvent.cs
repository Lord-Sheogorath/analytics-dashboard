namespace LordSheo.Analytics.Api.Models
{
	public class AnalyticsEvent
	{
		public int Id { get; set; }
		public string EventName { get; set; }
		public string UserId { get; set; }
		public DateTime Timestamp { get; set; }
		public string MetadataJson { get; set; }
	}
}