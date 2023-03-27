using System;
namespace RecksWebservice.Types
{
    public class User
    {
        private List<Class> currentClasses = new List<Class>();
        private double currentHoursScheduled = 0;
        private int maximumHours = 19;
        private bool legal = true;

        public User()
        {

        }

        public void AddClass(Class @class)
        {
            currentClasses.Add(@class);
            currentHoursScheduled += @class.GetCredits();
        }

        public void CheckIfHoursAreLegal()
        {
            if (currentHoursScheduled > maximumHours)
            {
                legal = false;
            }
        }

        public void DisplayHours()
        {

        }

        private void getClasses()
        {

        }

        public void OpenScheduler()
        {

        }

        public void RemoveClass(Class @class)
        {
            currentClasses.Remove(@class);
        }

        private void UpdateUI()
        {

        }
    }
}

