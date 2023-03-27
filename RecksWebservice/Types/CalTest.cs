using static RecksWebservice.Pages.ClassScheduler;

namespace RecksWebservice.Types
{
	public class OrganizedCalendar
	{
		public string Time { get; set; }
		public string Test { get; set; }

		
	}
	public class ScheduleFiller
	{	
		public List<OrganizedCalendar> GetCalendar()
		{
			return Calendar;
		}

		public List<OrganizedCalendar> Calendar = new List<OrganizedCalendar>()
		{
			new OrganizedCalendar() { Time = "5:00 AM", Test = "" },
			new OrganizedCalendar() { Time = "5:30 AM" },
			new OrganizedCalendar() { Time = "6:00 AM" },
			new OrganizedCalendar() { Time = "6:30 AM" },
			new OrganizedCalendar() { Time = "7:00 AM" },
			new OrganizedCalendar() { Time = "7:30 AM" },
			new OrganizedCalendar() { Time = "8:00 AM" },
			new OrganizedCalendar() { Time = "8:30 AM" },
			new OrganizedCalendar() { Time = "9:00 AM" },
			new OrganizedCalendar() { Time = "9:30 AM" },
			new OrganizedCalendar() { Time = "10:00 AM" },
			new OrganizedCalendar() { Time = "10:30 AM" },
			new OrganizedCalendar() { Time = "11:00 AM" },
			new OrganizedCalendar() { Time = "11:30 AM" },
			new OrganizedCalendar() { Time = "12:00 PM" },
			new OrganizedCalendar() { Time = "12:30 PM" },
			new OrganizedCalendar() { Time = "1:00 PM" },
			new OrganizedCalendar() { Time = "1:30 PM" },
			new OrganizedCalendar() { Time = "2:00 PM" },
			new OrganizedCalendar() { Time = "2:30 PM" },
			new OrganizedCalendar() { Time = "3:00 PM" },
			new OrganizedCalendar() { Time = "3:30 PM" },
			new OrganizedCalendar() { Time = "4:00 PM" },
			new OrganizedCalendar() { Time = "4:30 PM" },
			new OrganizedCalendar() { Time = "5:00 PM" },
			new OrganizedCalendar() { Time = "5:30 PM" },
			new OrganizedCalendar() { Time = "6:00 PM" }

		};

		
	}
}
