using RecksWebservice.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using HtmlAgilityPack;


namespace RecksWebservice.Data
{
    public class RMPDataHandler
    {
        private static string University = "";
        private static string Professor = "";
        private List<string> Class = new();


        public async Task GetProfessorData( string Professor, string Class)
        {
            University = "Louisiana State University";
            Professor = "Aymond";   //replace both with user entry
            Class = "CSC3380";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://www.ratemyprofessors.com");
            if (response.IsSuccessStatusCode)
            {
                //If request success, parse website using University, Professor, and Course
            }
        }
        
    }
}