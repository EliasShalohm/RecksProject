using Microsoft.VisualBasic;
using Syncfusion.Blazor.RichTextEditor.Internal;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace RecksWebservice.Types
{
    public class LabClass : ClassBuilder
    {
        public override void SetTotalEnrollCount()
        {
            classObject._totalEnrollCount = GetTotalEnrollCount();
        }
        public override void SetAvailableSlots()
        {
            classObject._availableSlots = GetAvailableSlots();
        }
        public override void SetClassName()
        {
            classObject._className = GetClassName();
        }
        public override void SetClassID()
        {
            classObject._classID = GetClassID();
        }
        public override void SetClassType()
        {
            classObject._classType = GetClassType();
        }
        public override void SetSection()
        {
            classObject._section = GetSection();
        }
        public override void SetCredits()
        {
            classObject._credits = GetCredits();
        }
        public override void SetCourseTitle()
        {
            classObject._courseTitle = GetCourseTitle();
        }
        public override void SetStartTimes()
        {
            classObject._startTimes = GetStartTimes();
        }
        public override void SetEndTimes()
        {
            classObject._endTimes = GetEndTimes();
        }
        public override void SetDays()
        {
            classObject._days = GetDays();
        }
        public override bool SetIsFull()
        {
            return GetFullState();
        }
        public override bool SetIsNightClass()
        {
            return CheckNightClassState();
        }
        public override void SetLabs()
        {
            classObject._labs = GetLabs();
        }
        public override void SetProfessor()
        {
            classObject._professor = GetProfessor();
        }
        public override bool SetTBAClass()
        {
            return GetTBAStatus();
        }
        public override void SetSpecialEnrollment()
        {
            classObject._specialEnrollment = GetSpecialEnrollment();
        }
        public override void SetRoomNumber()
        {
            classObject._roomNumber = GetRoomNumber();
        }
        public override void SetCourseBuilding()
        {
            classObject._courseBuilding = GetCourseBuilding();
        }
        public override void SetColor()
        {
            classObject._colour = GetColor();
        }
    }
}
