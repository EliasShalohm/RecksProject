using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace RecksWebservice.Data
{
	public class ClassDataHandler
	{
		private List<string> semesterList = new();
		private List<string> departmentList = new();
		private static string mainPageHtml = null;

		public static async void getMainData()
		{
            HttpClient client = new HttpClient();

            HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
            mainPageHtml = await mainPage.Content.ReadAsStringAsync();
            mainPage.Dispose();
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

			// delete all the useless information in the begining
			htmlData = htmlData.Substring(lastIndexOfUselessInformation + 2);

			// splits the HTML into an array of each line
			string[] split = htmlData.Split("\n");

			///May need adjustment to not just be the first line.
			//return first line of html
			await Task.Delay(0);
			return split[1];
		}

		public async Task FillSemesters()
		{

			//Methodology to getting the semester data from this page.

			// wait until loaded
			while (mainPageHtml == null)
			{
				await Task.Delay(10);
			}

			int firstIndexOfSemester = mainPageHtml.LastIndexOf("SemesterDesc");
			int lastIndexOfSemester = mainPageHtml.IndexOf("</select");

			string[] manipulatedString = { };

			int reached = firstIndexOfSemester + 14;
			while (reached < lastIndexOfSemester - 10)
			{
				int startIndex = mainPageHtml.IndexOf("<", reached);
				reached = mainPageHtml.IndexOf(">", reached + 1);
				int stringLength = reached - startIndex;

				manipulatedString = manipulatedString.Concat(new string[] { mainPageHtml.Substring(startIndex + 15, stringLength - 16) }).ToArray();
			}

			foreach (var item in manipulatedString)
			{
				semesterList.Add(item);
			}
		}

		public async Task FillDepartments()
        {

			// wait until loaded
			while (mainPageHtml == null)
			{
				await Task.Delay(10);
			}

			int firstIndexOfDepartment = mainPageHtml.LastIndexOf("Department");
			int lastIndexOfDepartment = mainPageHtml.LastIndexOf("</select");

			//Methodology to getting the department data from this page.

			string[] manipulatedString = { };

			int reached = firstIndexOfDepartment + 14;
			while (reached < lastIndexOfDepartment - 10)
			{
				int startIndex = mainPageHtml.IndexOf(">", reached);
				reached = mainPageHtml.IndexOf("<", reached + 1);
				int stringLength = reached - startIndex;

				string department = mainPageHtml.Substring(startIndex + 1, stringLength - 1);

				// remove random "amp;" strings that show up after & characters
				int indexOfBad = department.IndexOf("amp;");
				while (indexOfBad > 0)
				{
					department = department.Remove(indexOfBad, 4);
					indexOfBad = department.IndexOf("amp;");
				}
				manipulatedString = manipulatedString.Concat(new string[] { department }).ToArray();
			}

			foreach (var item in manipulatedString)
			{
				departmentList.Add(item);
			}
		}

		public List<string> GetSemesters() => semesterList;
		public List<string> GetDepartments() => departmentList;
	}
}