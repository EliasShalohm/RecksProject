using Microsoft.VisualBasic;
using Syncfusion.Blazor.RichTextEditor.Internal;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace RecksWebservice.Types
{
	#region The "Day" Enum ; Enclosed For Space
	public enum Day
	{
		Monday = 1,
		Tuesday = 2,
		Wednesday = 3,
		Thursday = 4,
		Friday = 5,
		Saturday = 6,
		Sunday = 7
	}
	#endregion

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

	public class Class
	{
		private int _totalEnrollCount;
		private int _availableSlots;
		private string _className = "";
		private string _classID = "";
		private string _classType = "";
		private int _section = 0;
		private double _credits;
		private string _courseTitle = "";
		private List<Time> _startTimes;
		private List<Time> _endTimes;
		private List<Day> _days;
		private bool _isFull;
		private bool _isNightClass;
		private List<Class> _labs;
		private Professor _professor;
		private bool _isTBAClass = false;
		private string _specialEnrollment;
		private string _roomNumber;
		private string _courseBuilding;
		private string _colour;

		/// <summary>
		/// Constructors
		/// </summary>
		#region Constructors
		public Class()
		{
			_totalEnrollCount = 0;
			_availableSlots = 0;
			_days = new List<Day>();
			_isFull = false;
			_labs = new List<Class>();
			_professor = new Professor();
		}
		public Class(string id, string courseName, DateTime startTime, DateTime endTime, string color)
		{
			_classID = id;
			_className = courseName;
			//_startTime = startTime.ToString(); //.ToString() is temporary until DateTime is figured out.
			//_endTime = endTime.ToString(); //.ToString() is temporary until DateTime is figured out.
			_colour = color;
		}
		#endregion

		#region An assortment of GET-methods to access variables outside of class.
		public string GetClassName() => _className;
        public string GetClassID() => _classID;
        public int GetSection() => _section;
        public int GetTotalEnrollCount() => _totalEnrollCount;
        public int GetAvailableSlots() => _availableSlots;
        public double GetCredits() => _credits;
        public bool GetTBAStatus() => _isTBAClass;
        public string GetCourseTitle() => _courseTitle;
        public List<Day> GetDays() => _days;
        public List<Time> GetStartTimes() => _startTimes;
        public List<Time> GetEndTimes() => _endTimes;
		public bool GetFullState() => _isFull;
        public bool CheckNightClassState() => _isNightClass;
        public List<Class> GetLabs() => _labs;
        public Professor GetProfessor() => _professor;
        public string GetClassType() => _classType;
        public string GetSpecialEnrollment() => _specialEnrollment;
        public string GetRoomNumber() => _roomNumber;
        public string GetCourseBuilding() => _courseBuilding;
        public string GetColor() => _colour;
        public override string ToString() => _className+_classID;
		public string GetHoursAsString()
		{
			if (_startTimes != null || _endTimes != null)
			{
				var start = string.Format("{0}:{1} {2}", _startTimes[0].hour, _startTimes[0].minutes, _startTimes[0].meridium);
				var end = string.Format("{0}:{1} {2}", _endTimes[0].hour, _endTimes[0].minutes, _endTimes[0].meridium);
				return " at " + start + " to " + end;
			} else
			{
				return string.Empty;
			}
		}
		#endregion

		#region Methods that are utilized for SET operations for instances.
		public void SetClassName(string name) => _className = name;
		public void SetClassNumber(string id) => _classID = id;
		public void SetClassSection(int section) => _section = section;
		public void SetTotalEnrollCount(int count) => _totalEnrollCount = count;
		public void SetAvailableSlots(int count) => _availableSlots = count;
		public void SetCredits(double credits) => _credits = credits;
		public void SetCourseTitle(string title) => _courseTitle = title;
		public void SetTBAStatus(bool isTBAClass) => _isTBAClass = isTBAClass;
		public void AddDay(Day day) => _days.Add(day);
		public void RemoveDay(Day day) => _days.Remove(day);
		public void SetFullState(bool full) => _isFull = full;
		public void SetDays(List<Day> days) => _days = days;
		public void SetStartHours(string startTime) => _startTimes = ParseToDateTime(startTime);
		public void SetEndHours(string endTime) => _endTimes = ParseToDateTime(endTime);
		public void SetNightClass(bool isNightClass) => _isNightClass = isNightClass;
		public void AssignLabs(List<Class> labs) => _labs = labs;
		public void AddLab(Class lab) => _labs.Add(lab);
		public void SetProfessor(Professor professor) => _professor = professor;
		public void SetClassType(string classInfo) => _classType = classInfo;
		public void SetSpecialEnrollment(string specialEnrollment) => _specialEnrollment = specialEnrollment;
		public void SetRoomNumber(string roomNumber) => _roomNumber = roomNumber;
		public void SetCourseBuilding(string courseBuilding) => _courseBuilding = courseBuilding;
		public void SetColor(string color) => _colour = color;
		public void SetColor(Color color) => _colour = color.ToString(); //May need adjustment for better parsing
		#endregion

		private List<Time> ParseToDateTime(string input)
		{
			var newTime = input;
			if (input[0].Equals('0'))
				newTime = input.Substring(1);

			string hour = "";
			switch (newTime.Length)
			{
				case 3:
					hour = Strings.Left(newTime, 1);
					break;
				case 4:
					hour = Strings.Left(newTime, 2);
					break;
			}
			string minutes = Strings.Right(newTime, 2);

			List<Time> days = new();
			foreach (var day in _days)
			{
				string meridium = "AM";
				if (_isNightClass)
				{
					meridium = "PM";
				}
				else
				{
					if (int.Parse(hour) <= 6 || int.Parse(hour) == 12)
					{
						meridium = "PM";
					}
				}

				Console.WriteLine("The class hour: " + hour + " minutes: " + minutes + " meridium: " + meridium);
				days.Add(new Time(
					(int)day,
					int.Parse(hour),
					minutes,
					meridium
					));
			}
			return days;
		}

		public void PrintTestValues()
		{
			// Get the current enrollment count
			int currentEnrollCount = GetTotalEnrollCount();
			Console.WriteLine($"Current enrollment count: {currentEnrollCount}");

			Console.WriteLine($"availabe slot count: {GetAvailableSlots()}");

			Console.WriteLine($"section: {GetSection()}");

			Console.WriteLine($"classId: {GetClassID()}");

			Console.WriteLine($"days: {GetDays().ToString()}");

			Console.WriteLine($"startTime: {GetStartTimes()}");

			Console.WriteLine($"endTime: {GetEndTimes()}");

			Console.WriteLine($"isFull: {GetFullState()}");

			Console.WriteLine($"isTBA: {GetTBAStatus()}");

			Console.WriteLine($"isNightClass: {CheckNightClassState()}");

			Console.WriteLine($"roomNumber: {GetRoomNumber()}");

			Console.WriteLine($"building: {GetCourseBuilding()}");


			if (GetLabs().Count > 0)
			{
				Class lab = GetLabs().First();
				Console.WriteLine($"Lab ----------------------- Lab");
				lab.PrintTestValues();
			}
		}

	}


}
