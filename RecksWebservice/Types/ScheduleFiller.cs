﻿#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

namespace RecksWebservice.Types
{
	public class OrganizedCalendar
	{
		public string? Time { get; set; }
		public string? Monday { get; set; }
		public string? Tuesday { get; set; }
		public string? Wednesday { get; set; }
		public string? Thursday { get; set; }
		public string? Friday { get; set; }
		public string? Saturday { get; set; }
		public string? Sunday { get; set; }
	}

	public class ScheduleFiller
	{
		public List<OrganizedCalendar> GetCalendar()
		{
			return Calendar;
		}

		public List<OrganizedCalendar> Calendar = new()
		{
			new OrganizedCalendar() { Time = "5:00 AM" },
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

		public void ResetCalendar()
		{
			Calendar = new()
			{
				new OrganizedCalendar() { Time = "5:00 AM" },
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

			public List<Class> CompareClasses()
		{
			List<Class> conflictingClasses = new List<Class>();
			//for (int i = 0; i < classes.Count - 1; i++)
			//{
			//	for (int j = i + 1; j < classes.Count; j++)
			//	{
			//		if (classes[i].StartTime < classes[j].EndTime && classes[j].StartTime < classes[i].EndTime)
			//		{
			//			conflictingClasses.Add(classes[i]);
			//			conflictingClasses.Add(classes[j]);
			//		}
			//	}
			//}
			return conflictingClasses;
		}
		//D_ Add error handling?
	}
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.