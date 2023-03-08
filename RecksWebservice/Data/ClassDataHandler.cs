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
		private List<Class> classes = new(); //Unused At The Moment
		private string mainPageHtml = "";

		public async Task GetBookletData()
		{
            HttpClient client = new HttpClient();

            HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
            mainPageHtml = await mainPage.Content.ReadAsStringAsync();
			mainPage.Dispose();
			
			FillSearchData(mainPageHtml); //Something is wrong in the HTML reading as some lines are cut.
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