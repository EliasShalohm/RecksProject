using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace RecksWebservice.Types
{
    #region The "Day" Enum ; Enclosed For Space
    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    #endregion

    public class Class
    {
        private string _classID = "";
        private int _section = 0;
        private int _totalEnrollCount;
        private int _availableSlots;
        private double _credits;
        private List<Day> _days;
        private int _startTime;
        private int _endTime;
        private bool _isFull;
        private bool _isNightClass;
        private List<Class> _labs;
        private Professor _professor;
        private bool _isTBAClass = false;
        private string _classInfo = "";

        /// <summary>
        /// Constructors
        /// </summary>
        #region Constructors
        public Class()
        {
            _totalEnrollCount = 0;
            _availableSlots = 0;
            _days = new List<Day>();
            _startTime = 0;
            _endTime = 0;
            _isFull = false;
            _isNightClass = false;
            _labs = new List<Class>();
            _professor = new Professor();
        }
        #endregion

        #region An assortment of GET-methods to access variables outside of class.
        public string GetClassID() => _classID;
        public int GetSection() => _section;
        public int GetTotalEnrollCount() => _totalEnrollCount;
        public int GetAvailableSlots() => _availableSlots;
        public double GetCredits() => _credits;
        public bool GetTBAStatus() => _isTBAClass;
        public List<Day> GetDays() => _days;
        public int GetStartTime() => _startTime;
        public int GetEndTime() => _endTime;
        public bool GetFullState() => _isFull;
        public bool CheckNightClassState() => _isNightClass;
        public List<Class> GetLabs() => _labs;
        public Professor GetProfessor() => _professor;
        public string GetClassInfo() => _classInfo;
        #endregion
        
        #region Methods that are utilized for SET operations for instances.
        public void SetClassID(string id) => _classID = id;
        public void SetClassSection(int section) => _section = section;
        public void SetTotalEnrollCount(int count) => _totalEnrollCount = count;
        public void SetAvailableSlots(int count) => _availableSlots = count;
        public void SetCredits(double credits) => _credits = credits;
        public void SetTBAStatus(bool isTBAClass) => _isTBAClass = isTBAClass;
        public void AddDay(Day day) => _days.Add(day);
        public void RemoveDay(Day day) => _days.Remove(day);
        public void SetFullState(bool full) => _isFull = full;
        public void SetDays(List<Day> days) => _days = days;
        public void SetStartTime(int startTime) => _startTime = startTime;
        public void SetEndTime(int endTime) => _endTime = endTime;
        public void SetNightClass(bool isNightClass) => _isNightClass = isNightClass;
        public void AssignLabs(List<Class> labs) => _labs = labs;
        public void AddLab(Class lab) => _labs.Add(lab);   
        public void SetProfessor(Professor professor) => _professor = professor;
        public void SetClassInfo(string classInfo) => _classInfo = classInfo;
        public void AddClassInfo(string classInfo) => _classInfo += "\n" + classInfo;
        #endregion
    
        public void PrintTestValues()
        {
            // Get the current enrollment count
            int currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine($"Current enrollment count: {currentEnrollCount}");

            Console.WriteLine($"availabe slot count: {GetAvailableSlots()}");

            Console.WriteLine($"section: {GetSection()}");

			Console.WriteLine($"classId: {GetClassID()}");

			Console.WriteLine($"days: {GetDays().ToString()}");

			Console.WriteLine($"startTime: {GetStartTime()}");

			Console.WriteLine($"ENDTime: {GetEndTime()}");

			Console.WriteLine($"isFull: {GetFullState()}");

			Console.WriteLine($"isTBA: {GetTBAStatus()}");

            Console.WriteLine($"isNightClass: {CheckNightClassState()}");

            if (GetLabs().Count > 0)
            {
                Class lab = GetLabs().First();
				Console.WriteLine($"Lab ----------------------- Lab");
                lab.PrintTestValues();
			}
		}

    }


}
