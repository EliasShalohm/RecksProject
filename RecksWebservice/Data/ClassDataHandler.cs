using System.Runtime.InteropServices;

namespace RecksWebservice.Data
{
	public class ClassDataHandler
	{
		private List<string> semesterList = new();
		private List<string> departmentList = new();
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

			HttpClient client = new HttpClient();

			// <select name="SemesterDesc">
			// <select name="Department">

			HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
			string mainPageHtml = await mainPage.Content.ReadAsStringAsync();
			mainPage.Dispose();

			//Methodology to getting the semester data from this page.

			string[] manipulatedString = { "Test1", "Test2" };

			foreach (var item in manipulatedString)
			{
				semesterList.Add(item);
			}
		}

		public async Task FillDepartments()
        {

            HttpClient client = new HttpClient();

            // <select name="SemesterDesc">
            // <select name="Department">

            HttpResponseMessage mainPage = await client.GetAsync("http://appl101.lsu.edu/booklet2.nsf/Selector2?OpenForm");
            string mainPageHtml = await mainPage.Content.ReadAsStringAsync();
			mainPage.Dispose();

			//Methodology to getting the department data from this page.

			string[] manipulatedString = { "Test1", "Test2" };

			foreach (var item in manipulatedString)
			{
				departmentList.Add(item);
			}
		}

		public List<string> GetSemesters() => semesterList;
		public List<string> GetDepartments() => departmentList;
	}
}