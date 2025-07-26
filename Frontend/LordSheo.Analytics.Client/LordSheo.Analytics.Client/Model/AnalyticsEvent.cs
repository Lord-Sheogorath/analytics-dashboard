namespace LordSheo.Analytics.Client.Model
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