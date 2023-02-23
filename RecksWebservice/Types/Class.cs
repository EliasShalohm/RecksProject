namespace RecksWebservice.Types
{
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
    public class Class
    {
        private int _totalEnrollCount;
        private int _availableSlots;
        private Day[] _days;
        private int _startTime;
        private int _endTime;
        private bool _isFull;
        private bool _isNightClass;
        private Class[] _labs;
        private Professor _professor;

        public Class() 
        
        {

            _totalEnrollCount = 0;
                    
        }

        public int GetTotalEnrollCount()
        {
            // A method to get the current enrollment count
            return _totalEnrollCount;

        }

        public void SetTotalEnrollCount(int count)
        {
           // Method to set the enrollment count to a specific value
            _totalEnrollCount = count;
        }

        public void IncrementTotalEnrollCount()
        {
            // Methement to increment the enrollment count 
            _totalEnrollCount++;
        }
        public void DecrementTotalEnrollCount()
        {
            // Method to decrement the enrollment count
            if(_totalEnrollCount++ > 0)
            {

                _totalEnrollCount--;
            }
            
        }
        public void ManageEnrollmentCount()
        {
            // Get the current enrollment count
            int currentEnrollCount = GetTotalEnrollCount();
            Console.WriteLine($"Current enrollment count: {currentEnrollCount}");

            // Enroll a new Student 
            IncrementTotalEnrollCount();
            Console.WriteLine("Enrolled a new student. ");

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

        public string GetEnrollmentCountString()
        {
            int enrollCount = GetTotalEnrollCount();
            string enrollCountString = enrollCount.ToString();
            return $"Enrollment count:  {enrollCountString}";

        }


    }


}
