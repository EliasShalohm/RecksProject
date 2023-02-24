namespace RecksWebservice.Data
{
	public class ClassData
	{

		public async Task<string> GetClassData()
		{
			var values = new Dictionary<string, string>
			{
				{ "%%Surrogate_SemesterDesc","1"},
				{ "SemesterDesc", "Spring 2022" },
				{"%%Surrogate_Department", "1"},
				{ "Department", "ACCOUNTING" }
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

			//return first line of html
			await Task.Delay(0);
			return split[1];
		}
	}
}