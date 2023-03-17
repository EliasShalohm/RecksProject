using Microsoft.Extensions.Options;
using RecksWebservice.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace RecksWebservice.Data
{
	public class ClassDataHandler
	{
		private List<string> semesterList = new();
		private List<string> departmentList = new();
		private List<Class> classes = new();
		private string mainPageHtml = "";

		public async Task GetBookletData()
		{
            HttpClient client = new HttpClient();

            HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
            mainPageHtml = await mainPage.Content.ReadAsStringAsync();
			mainPage.Dispose();
			
			FillSearchData(mainPageHtml); //Something is wrong in the HTML reading as some lines are cut.
		}
		private List<Day> GetDaysFromString(string line)
		{
			//start at 72
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

		private Class MakeClassFromLine(string line)
		{
			Class createdClass = new();
			string availableSlots = line.Substring(0, 3).Trim();
			string takenSlots = line.Substring(6, 3).Trim();

			Console.WriteLine(availableSlots.Length + " " + takenSlots.Length);

			//test that line isnt a random empty line left by WONDERFUL LSU website...
			if (availableSlots.Length >= 1 || takenSlots.Length >= 1)
			{
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
						createdClass.SetFullState(true);
						createdClass.SetAvailableSlots(0);
						createdClass.SetTotalEnrollCount(int.Parse(takenSlots));
					}
					else
					{
						createdClass.SetAvailableSlots(int.Parse(availableSlots));
						createdClass.SetTotalEnrollCount(int.Parse(takenSlots) + int.Parse(availableSlots));
					}

					string className = line.Substring(11, 4).Trim(); // ACCT, BIOL, ETC
					string classNumber = line.Substring(16, 4).Trim();// 1001, ETC
																	  //im assuming className and classNumber will be both added to classID
																	  //(can be changed separate accordingly for ease) of access later
					createdClass.SetClassID(className + classNumber);

					string classType = line.Substring(21, 3).Trim(); // aka RESEDENTIAL ONLY?? ETC.
					createdClass.AddClassInfo(classType);

					string classSection = line.Substring(27, 3).Trim();
					createdClass.SetClassSection(int.Parse(classSection));

					string classCredits = line.Substring(55, 4).Trim();
					if (classCredits.IndexOf("-") > 0) { createdClass.SetCredits(0); }
					else { createdClass.SetCredits(double.Parse(classCredits)); }

					bool isTBAClass = false;
					if (line.IndexOf("TBA") > 0) { isTBAClass = true; }

					if (isTBAClass) { createdClass.SetTBAStatus(true); }
					else
					{
						List<Day> days = GetDaysFromString(line);
						createdClass.SetDays(days);

						string startTime = line.Substring(60, 4).Trim();
						string endTime = line.Substring(65, 4).Trim();
						bool isNight = false;
						if (line[69] == 'N')
						{
							isNight = true;
						}
						createdClass.SetStartTime(int.Parse(startTime));
						createdClass.SetEndTime(int.Parse(endTime));
						createdClass.SetNightClass(isNight);
					}

					//extra info start at 99 ( includes prof name )
					string extraInfo = line.Substring(99, 17).Trim();
					createdClass.AddClassInfo(extraInfo);

					string professorName = line.Substring(116).Trim();

					Professor professor = new Professor();
					professor.SetName(professorName);
					createdClass.SetProfessor(professor);

					return createdClass;
				}
			}
			Console.WriteLine("RETURNING NULL FOR LINE: " + line);
			return null;
		}
		private Class MakeLabFromLine(string line)
		{
			Class createdClass = new();

			List<Day> days = GetDaysFromString(line);
			createdClass.SetDays(days);

			string startTime = line.Substring(60, 4).Trim();
			string endTime = line.Substring(65, 4).Trim();
			bool isNight = false;
			if (line[69] == 'N')
			{
				isNight = true;
			}
			createdClass.SetStartTime(int.Parse(startTime));
			createdClass.SetEndTime(int.Parse(endTime));
			createdClass.SetNightClass(isNight);
			return createdClass;
		}
		public async Task<string> GetClassData(string Semester, string Department)
        {
            var values = new Dictionary<string, string>
            {
                { "%%Surrogate_SemesterDesc","1"},
                { "SemesterDesc", Semester },
                {"%%Surrogate_Department", "1"},
                { "Department", Department }
            };

			var content = new FormUrlEncodedContent(values);
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.PostAsync("http://appl101.lsu.edu/booklet2.nsf/f5e6e50d1d1d05c4862584410071cd2e?CreateDocument", content);

            string htmlData = await response.Content.ReadAsStringAsync();

			int lastIndexOfUselessInformation = htmlData.LastIndexOf("--");

			var firstWord = Semester.Substring(0, Semester.IndexOf(" "));

			// delete all the useless information in the begining
			htmlData = htmlData.Substring(lastIndexOfUselessInformation + 2);

			// splits the HTML into an array of each line
			string[] splitTable = htmlData.Split("\n");

			List<Class> classes = new List<Class>();

			Class previousClass = null;

			for (int i = 1; i < splitTable.Length; i++)
			{
				//going through the lines
				string line = splitTable[i];

				// break when u get to the last line because there is extras
				if (line.IndexOf(firstWord) >= 0)
				{
					Console.WriteLine("breaking at line : " + line);
					break;
				}

				//make sure line is not a *** comment
				string trimmed = line.Trim();
				Console.WriteLine("trimmed: " + trimmed + " TRIMMED LENGTH = " + trimmed.Length);
				if (trimmed.Length > 1 && trimmed[0] == '*')
				{
					previousClass.AddClassInfo(line);
					Console.WriteLine("this line is a comment based on the pervious" + line);
				}
				else if (trimmed.Length > 2 && trimmed.Substring(0,3) == "LAB")
				{
					Console.WriteLine("its a lab " + line);
					Class createdClass = MakeLabFromLine(line);
					if (createdClass != null)
					{
						previousClass.AddLab(createdClass);
					}
				}
				else if (trimmed.Length > 0)
				{
					Console.WriteLine("this is a class");
					Class createdClass = MakeClassFromLine(line);
					if (createdClass != null)
					{
						previousClass = createdClass;
						classes.Add(createdClass);
					}
				}
			}

			Console.WriteLine("--------------------");
			for (int i = 0; i < classes.Count; i++)
			{
				Console.WriteLine("==========================");
				Class c = classes[i];
				c.TestForValues();
			}
				

			///May need adjustment to not just be the first line.
			//return first line of html
			await Task.Delay(0);
			return splitTable[1];
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
		#endregion
	}
}