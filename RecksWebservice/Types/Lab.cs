using System;
namespace RecksWebservice.Types
{
	public class Lab : ClassBuilder
	{
        public override void SetTotalEnrollCount()
        {
            classObject._totalEnrollCount = 0;
        }
        public override void SetAvailableSlots()
        {
            classObject._availableSlots = 0;
        }
        public override void SetClassName()
        {
            classObject._className = "";
        }
        public override void SetClassID()
        {
            classObject._classID = "";
        }
        public override void SetClassType()
        {
            classObject._classType = "";
        }
        public override void SetSection()
        {
            classObject._section = 0;
        }
        public override void SetCredits()
        {
            classObject._credits = 0;
        }
        public override void SetCourseTitle()
        {
            classObject._courseTitle = "";
        }
        public override void SetStartTimes()
        {
            //classObject._startTimes = 
        }
        public override void SetEndTimes()
        {
            //classObject._endTimes = 
        }
        public override void SetDays()
        {
            //classObject._days =
        }
        public override bool SetIsFull()
        {
            return false;
        }
        public override bool SetIsNightClass()
        {
            return false;
        }
        public override void SetLabs()
        {
            //classObject._labs =
        }
        public override void SetProfessor()
        {
            //classObject._professor =
        }
        public override bool SetTBAClass()
        {
            return false;
        }
        public override void SetSpecialEnrollment()
        {
            classObject._specialEnrollment = "";
        }
        public override void SetRoomNumber()
        {
            classObject._roomNumber = "";
        }
        public override void SetCourseBuilding()
        {
            classObject._courseBuilding = "";
        }
        public override void SetColor()
        {
            classObject._colour = "";
        }
    }
}

