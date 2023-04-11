using Microsoft.VisualBasic;
using RecksWebservice.Data;
using Syncfusion.Blazor.RichTextEditor.Internal;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace RecksWebservice.Types
{
    public abstract class ClassBuilder : ClassDataHandler
    {
        protected Class classObject;
        public abstract void SetTotalEnrollCount();
        public abstract void SetAvailableSlots();
        public abstract void SetClassName();
        public abstract void SetClassID();
        public abstract void SetClassType();
        public abstract void SetSection();
        public abstract void SetCredits();
        public abstract void SetCourseTitle();
        public abstract void SetStartTimes();
        public abstract void SetEndTimes();
        public abstract void SetDays();
        public abstract bool SetIsFull();
        public abstract bool SetIsNightClass();
        public abstract void SetLabs();
        public abstract void SetProfessor();
        public abstract bool SetTBAClass();
        public abstract void SetSpecialEnrollment();
        public abstract void SetRoomNumber();
        public abstract void SetCourseBuilding();
        public abstract void SetColor();

        public void CreateNewClass() { classObject = new Class(); }
        public Class GetClass() { return classObject; }
    }
}
