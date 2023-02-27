namespace RecksWebservice.Data
{
	public class ClassData
	{
		/// <summary>
		/// Gets and returns public available semesters to the Booklet for user navigation.
		/// </summary>
		/// <returns></returns>
		public async Task<List<string>> GetSemesters()
		{
			var test = new List<string>();
			return test;
        }

        /// <summary>
        /// Gets and returns public available departments under a given semester to the Booklet for user navigation.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetDepartments()
        {
            var test = new List<string>();
            return test;
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
	}
}