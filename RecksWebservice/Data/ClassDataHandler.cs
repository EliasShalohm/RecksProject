using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using RecksWebservice.Types;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Syncfusion.Blazor.Schedule;

namespace RecksWebservice.Data
{
	public class ClassDataHandler
	{
		private List<string> semesterList = new();
		private List<string> departmentList = new();
		private List<Class> classes = new();

		/// <summary>
		/// Gets LSU's Booklet Data as a whole
		/// </summary>
		/// <returns></returns>
		public async Task GetBookletData()
		{
			HttpClient client = new HttpClient();

			HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
			string mainPageHtml = await mainPage.Content.ReadAsStringAsync();
			mainPage.Dispose();

			FillSearchData(mainPageHtml);
		}
		public async Task GetClassData(string Semester, string Department)
		{
			var values = new Dictionary<string, string>
			{
				{ "%%Surrogate_SemesterDesc","1"},
				{ "SemesterDesc", Semester.Replace("\n", "").Replace("\r", "") },
				{"%%Surrogate_Department", "1"},
				{ "Department", Department.Replace("\n", "").Replace("\r", "") }
			};

			var content = new FormUrlEncodedContent(values);
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.PostAsync("http://appl101.lsu.edu/booklet2.nsf/f5e6e50d1d1d05c4862584410071cd2e?CreateDocument", content);

			string htmlData = await response.Content.ReadAsStringAsync();

			//Checks for "There are no courses found for this Semester / Department combination"
			if (!htmlData.Contains("There are no courses found"))
				FillClassData(htmlData, Semester, Department);
			else
				Console.WriteLine("There are no courses found for this Semester / Department combination: {0} & {1}", Semester, Department);
		}

		private void FillClassData(string htmlData, string Semester, string Department)
		{
			Console.WriteLine(htmlData); //For testing class values.

			var semesterName = Semester.Split(' ')[0];
			int startIndex = htmlData.LastIndexOf("--");
			int endIndex = htmlData.LastIndexOf(semesterName);
			//The "+2" Neglects to include the "--" as starting index; for easier reading.
			htmlData = htmlData.Substring(startIndex, endIndex - startIndex);
			string[] unfilteredClasses = htmlData.Split("\n");

			/* Test Classes !(Fall 2023 -> Astronomy)!
				 ENRL  COURSE           SEC						   HR     TIME		  DAYS							 SPECIAL
			AVL  CNT   ABBR NUM  TYPE	NUM COURSE TITLE           CR  BEGIN-END	  MTWTFS	ROOM	BUILDING    ENROLLMENT     INSTRUCTOR
			----------------------------------------------------------------------------------------------------------------------------------
			300        ASTR 1101         1  THE SOLAR SYSTEM       3.0   830 - 0920   M W F		0130	NICHOLSON					TURLEY C
			300        ASTR 1101         2  THE SOLAR SYSTEM       3.0   130 - 0250    T TH		0130	NICHOLSON					TURLEY C
			300        ASTR 1102         1  STELLAR ASTRONOMY      3.0  1230 - 0120   M W F		0130	NICHOLSON					TURLEY C
			193        ASTR 1102         2  STELLAR ASTRONOMY      3.0   900 - 1020    T TH		0016	LOCKETT						BURNS E
			 25        ASTR 1108 LAB     1  ASTRONOMY LAB          1.0   700 - 0850N  M			0365	NICHOLSON					TURLEY C
			 25        ASTR 1108 LAB     2  ASTRONOMY LAB          1.0   700 - 0850N  T			0365	NICHOLSON					TURLEY C
			 25        ASTR 1109 LAB     1  ASTRONOMY LAB          1.0   700 - 0850N  W			0365	NICHOLSON					TURLEY C
			 25        ASTR 1109 LAB     2  ASTRONOMY LAB          1.0   700 - 0850N  TH		0365	NICHOLSON					TURLEY C
			 15        ASTR 1401         1  PLANETARY ASTROPHYS    3.0   130 - 0250   T TH		0118	NICHOLSON					
			 10        ASTR 4261         1  MOD OBSERVATIONAL T    3.0   230 - 0320       F		0262	NICHOLSON					PENNY M
								 LAB                                     730 - 1020N  TW		0262	NICHOLSON					PENNY M
				  *** ASTR 7741 * **CROSS - LISTED WITH PHYS 7741
			 15        ASTR 7741         1  STELLAR ASTROPHYSICS   3.0  1130 - 1220   M W F  0106 NICHOLSON CHATZOPOULOS

				  ***ASTR 7777 * **CROSS - LISTED WITH PHYS 7777
			 15        ASTR 7777 SEM     1  SEM: ASTR & ASTROPHY   1 - 6  1200 - 0120    T     0262 NICHOLSON CHATZOPOULOS
			*//* Summer Class Exceptions
			----------------------------------------------------------------------------------------------------------------------------------
					 SESSION  B  OFFERINGS BELOW    05/22/2023 - 06/24/2023
			23     7  ASTR 1101         1  THE SOLAR SYSTEM       3.0  1100-1230   MTWTF  0108 NICHOLSON
					 SESSION  C  OFFERINGS BELOW    07/03/2023 - 08/05/2023
			25     5  ASTR 1102         1  STELLAR ASTRONOMY      3.0  1100-1230   MTWTF  0108 NICHOLSON
			*/

			//Going through each class in the table. This setup assumes there is no class list that BEGINS with a lab.
			Class previousClass = new();
			for (int i = 1; i < unfilteredClasses.Length; i++)
			{
				string line = unfilteredClasses[i];
				if (!line.Contains("*") && !string.IsNullOrWhiteSpace(line)) //Is not *** Comment "Class"
				{
					if (!line.Contains("LAB")) //Regulars
						previousClass = ProcessClassFromLine(line);
					else //Labs
					{
						//Checking If LAB Is Not Dependant On Previous Class
						if (IsLabStandardClass(line))
							previousClass = ProcessClassFromLine(line);
						else
							previousClass.AddLab(ProcessLabFromLine(line)); ///Not entirely functional {!}
					}
					classes.Add(previousClass);
				}

			}
		}

		#region Filling & Getting Semester and Department Data
		/// <summary>
		/// Reads semester and department data from booklet html.
		/// </summary>
		/// <param name="bookletHTML"></param>
		private void FillSearchData(string bookletHTML)
		{
			int semesterFirstIndex = bookletHTML.IndexOf(@"<select name=""SemesterDesc"">");
			int semesterLastIndex = bookletHTML.IndexOf("</select>");

			int departmentFirstIndex = bookletHTML.IndexOf(@"<select name=""Department"">");
			int departmentLastIndex = bookletHTML.LastIndexOf("</select>");

			var semesterSubstring = bookletHTML.Substring(semesterFirstIndex, semesterLastIndex - semesterFirstIndex);
			string[] semesters = semesterSubstring.Split("<option value=");

			var departmentSubstring = bookletHTML.Substring(departmentFirstIndex, departmentLastIndex - departmentFirstIndex);
			string[] departments = departmentSubstring.Split("<option>");

			for (int i = 1; i < semesters.Length; i++)
			{
				string semesterName = semesters[i].Substring(semesters[i].IndexOf("\"") + 1, semesters[i].LastIndexOf("\"") - 1);
				//Debug.WriteLine(semesterName); ///Functionally just for debugging.
				semesterList.Add(semesterName);
			}
			for (int i = 1; i < departments.Length; i++)
			{
				string departmentName = departments[i].Replace("&amp;", "&");
				//Debug.WriteLine(departmentName); ///Functionally just for debugging.
				departmentList.Add(departmentName);
			}
		}

		public List<string> GetSemesters() => semesterList;
		public List<string> GetDepartments() => departmentList;
		public List<Class> GetClasses() => classes;
		#endregion

		#region Alternate Methods - Need Fixing Up!
		private bool IsLabStandardClass(string line)
		{
			string availableSlots = line.Substring(0, 3).Trim();
			string takenSlots = line.Substring(6, 3).Trim();
			if (availableSlots.Length >= 1 || takenSlots.Length >= 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private List<Day> GetDaysFromString(string line)
		{
			//Days in line start at index 72
			List<Day> days = new List<Day>();
			if (line[72] == 'M')
			{
				days.Add(Day.Monday);
			}
			if (line[73] == 'T')
			{
				days.Add(Day.Tuesday);
			}
			if (line[74] == 'W')
			{
				days.Add(Day.Wednesday);
			}
			if (line[75] == 'T')
			{
				days.Add(Day.Thursday);
			}
			if (line[76] == 'F')
			{
				days.Add(Day.Friday);
			}
			return new List<Day>();
		}
		private Class ProcessClassFromLine(string line) ///Requires work {!}
		{
			Class newClass = new();
			string availableSlots = line.Substring(0, 3).Trim();
			string takenSlots = line.Substring(6, 3).Trim();

			Console.WriteLine(availableSlots.Length + " " + takenSlots.Length);

			// default taken to 0 if its empty
			if (takenSlots.Length == 0)
			{
				takenSlots = "0";
			}

			// make sure that class isnt on hold
			if (availableSlots == "(H)")
			{
				Console.WriteLine("this line is on hold, ignore" + line);
			}
			else
			{
				if (availableSlots == "(F)")
				{
					newClass.SetFullState(true);
					newClass.SetAvailableSlots(0);
					newClass.SetTotalEnrollCount(int.Parse(takenSlots));
				}
				else
				{
					newClass.SetAvailableSlots(int.Parse(availableSlots));
					newClass.SetTotalEnrollCount(int.Parse(takenSlots) + int.Parse(availableSlots));
				}

				//Name (Ie. BIOL, ASTR, MATH)
				string className = line.Substring(11, 4).Trim();
				newClass.SetClassName(className);

				//ID (Ie. 1001, 1002, 9999)
				string classNumber = line.Substring(16, 4).Trim();
				newClass.SetClassNumber(classNumber);

				//Class Type (Ie. RESIDENTIAL ONLY, LAB, SEM, "SEM:")
				string classType = line.Substring(21, 3).Trim();
				newClass.SetClassType(classType);

				//Class Section (Ie. 1, 2, 3, 21, etc.)
				string classSection = line.Substring(27, 3).Trim();
				newClass.SetClassSection(int.Parse(classSection));

				//Credits (Ie. 1.0, 2.0, 3.0, 1-6, etc.)
				string classCredits = line.Substring(55, 4).Trim();
				if (classCredits.IndexOf("-") > 0) { newClass.SetCredits(0); }
				else { newClass.SetCredits(double.Parse(classCredits)); }

				//Checking if Class is TBA "To Be Announced"
				bool isTBAClass = false;
				if (line.IndexOf("TBA") > 0) { isTBAClass = true; }

				if (isTBAClass) { newClass.SetTBAStatus(true); }
				else
				{
					//Class Times
					List<Day> days = GetDaysFromString(line);
					newClass.SetDays(days);

					var temp = line.Substring(60, 12).Trim().Split('-');
					string startTime = temp[0];
					string endTime = temp[1];
					if (!startTime.Contains('N') && !endTime.Contains('N'))
						newClass.SetNightClass(false);
					else
					newClass.SetNightClass(true);
					newClass.SetStartTime(startTime);
					newClass.SetEndTime(endTime);
					
				}

				///Add reading for course title {!}

				///Need to get class room # & building {!}


				///Will need to be changed to account professor name (& last initial) (and the lack thereof) seperate from building.
				//extra info start at 99 ( includes prof name )
				//string extraInfo = line.Substring(99, 17).Trim();
				//newClass.SetClassType(extraInfo);

				/* CRASHES AT THIS LINE -> 15        ASTR 1401         1  PLANETARY ASTROPHYS    3.0   130-0250    T TH  0118 NICHOLSON
				 * This crash is caused by the lack of a professor name, where the line below is trying to find a substring at an index that does not exist.
				string professorName = line.Substring(116).Trim();

				Professor professor = new Professor();
				professor.SetName(professorName);
				newClass.SetProfessor(professor);
				*/
			}
			return newClass;

		}
		private Class ProcessLabFromLine(string line) //Requires rework
		{
			Class createdClass = new();

			List<Day> days = GetDaysFromString(line);
			createdClass.SetDays(days);

			string startTime = line.Substring(60, 4).Trim();
			string endTime = line.Substring(65, 4).Trim();
			/*bool isNight = false;
			if (line[69] == 'N') //Nice, but indexing may not be a good idea here.
			{
				isNight = true;
			}
			createdClass.SetStartTime(int.Parse(startTime));
			createdClass.SetEndTime(int.Parse(endTime));
			createdClass.SetNightClass(isNight);*/
			return createdClass;
		}
		#endregion

	}
}