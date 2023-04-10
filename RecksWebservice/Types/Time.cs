namespace RecksWebservice.Types
{
	public class Time
	{
		public int day { get; set; }
		public int hour { get; set; }
		public string minutes { get; set; }
		public string meridium { get; set; }
		public Time(int day, int hour, string minutes, string meridium)
		{
			this.day = day;
			this.hour = hour;
			this.minutes = minutes;
			this.meridium = meridium;
		}
	}
}
