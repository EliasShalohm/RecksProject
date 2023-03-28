using RecksWebservice.Types;
using System.Diagnostics;

namespace RecksWebservice.Data
{
	public class ErrorHandler
	{
		//Todo: Make it write to .txt file
		public void ReportConflictingClasses(List<Class> conflictingClasses)
		{
			foreach (Class classObj in conflictingClasses)
			{
				Debug.WriteLine("Conflicting class: " + classObj.GetClassName());
			}
		}
	}
}
