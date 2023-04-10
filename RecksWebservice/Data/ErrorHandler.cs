using RecksWebservice.Types;
using System.Diagnostics;

namespace RecksWebservice.Data
{
	public static class ErrorHandler
	{
		//Todo: Make it write to .txt file
		public static void ReportConflictingClasses(List<Class> conflictingClasses)
		{
			foreach (Class classObj in conflictingClasses)
			{
				Debug.WriteLine("Conflicting class: " + classObj.GetClassName());
			}
		}
	}
}
