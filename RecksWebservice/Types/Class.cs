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

        public Class() {
                    
        }

        //Implement Methods

    }


}
