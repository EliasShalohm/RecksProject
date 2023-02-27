using System.Reflection.Emit;

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
        private List<Day> _days;
        private int _startTime;
        private int _endTime;
        private bool _isFull;
        private bool _isNightClass;
        private List<Class> _labs;
        private Professor _professor;

        /// <summary>
        /// Constructors
        /// </summary>
        #region Constructors
        public Class()  {
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
        public Class(int totalEnrollCount, int availAbleSlots, List<Day> days, int startTime, int endTime, bool isNightClass, List<Class> labs, Professor professor)
        {
            _totalEnrollCount = totalEnrollCount;
            _availableSlots = availAbleSlots;

            if (availAbleSlots == 0)
                _isFull = true;
            
            _days = days;
            _startTime = startTime;
            _endTime = endTime;
            _isFull = isNightClass;
            _labs = labs;
            _isNightClass = isNightClass;
            _professor = professor;
        }
        public Class(int totalEnrollCount, int availAbleSlots, List<Day> days, int startTime, int endTime, bool isNightClass, Professor professor)
        {
            _totalEnrollCount = totalEnrollCount;
            _availableSlots = availAbleSlots;
            if (availAbleSlots == 0)
                _isFull = true;
            _days = days;
            _startTime = startTime;
            _endTime = endTime;
            _isFull = isNightClass;
            _isNightClass = isNightClass;
            _labs = new();
            _professor = professor;
        }

        public Class(int totalEnrollCount, int availAbleSlots, List<Day> days, int startTime, int endTime, bool isNightClass)
        {
            _totalEnrollCount = totalEnrollCount;
            _availableSlots = availAbleSlots;
            if (availAbleSlots == 0)
                _isFull = true;
            _days = days;
            _startTime = startTime;
            _endTime = endTime;
            _isFull = isNightClass;
            _isNightClass = isNightClass;
            _labs = new();
            _professor= new();
        }
		#endregion

		/// <summary>
		/// An assortment of GET-methods to access variables outside of class.
		/// </summary>
		/// <returns></returns>
		#region An assortment of GET-methods to access variables outside of class.
		public string GetClassID() => _classID;
		public int GetSection() => _section;
		public int GetTotalEnrollCount() => _totalEnrollCount;
        public int GetAvailableSlots() => _availableSlots;
        public List<Day> GetDays() => _days;
        public int GetStartTime() => _startTime;
        public int GetEndTime() => _endTime;
        public bool GetFullState() => _isFull;
        public bool CheckNightClassState() => _isNightClass;
        public List<Class> GetLabs() => _labs;
        public Professor GetProfessor() => _professor;
        #endregion

        /// <summary>
        /// Methods that are utilized for SET operations for instances.
        /// </summary>
        /// <param name="count"></param>
        #region Methods that are utilized for SET operations for instances.
        public void SetClassID(string id) => _classID = id;
        public void SetClassSection(int section) => _section = section;
        public void SetTotalEnrollCount(int count) => _totalEnrollCount = count;
        public void SetAvailableSlots(int count) => _availableSlots = count;
        public void AddDay(Day day) => _days.Add(day);
        public void RemoveDay(Day day) => _days.Remove(day);
        public void SetDays(List<Day> days) => _days = days;
        public void SetStartTime(int startTime) => _startTime = startTime;
        public void SetEndTime(int endTime) => _endTime = endTime;
        public void SetNightClass(bool isNightClass) => _isNightClass = isNightClass;
        public void AssignLabs(List<Class> labs) => _labs = labs;
        public void SetProffessor(Professor professor) => _professor = professor;
        #endregion

        public void TestForValues() ///Is this needed? -Z
        {
            // Get the current enrollment count
            int currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine($"Current enrollment count: {currentEnrollCount}");

            // Get the updated enrollment count
            currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine($"Updated enrollment count: {currentEnrollCount}");

            //Cancel enrollment for a student();
            Console.WriteLine("Canceled enrollment for a student.");

            //Get  the updated enrollment count
            currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine($"Updated enrollment count: {currentEnrollCount}");

            //Set the enrollment count to a temporary value
            SetTotalEnrollCount(20);
            Console.WriteLine("Set enrollment count to 20.");

            //Get the updated enrollment count
            currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine("$Updated enrollment count: {currentEnrollCount}");
        }

    }


}
