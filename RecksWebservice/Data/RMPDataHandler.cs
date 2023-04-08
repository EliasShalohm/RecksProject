using RecksWebservice.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using System.Net.Http;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.PivotView;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Syncfusion.Blazor.Data;


namespace RecksWebservice.Data
{
    public class RMPDataHandler
    {
        private Professor professor;
        private static string Professor = "";
        private List<string> Class = new();

        public int GetProfessorRating()
        {
            return professor.GetRating();
        }

        public async Task GetProfessorData(string professor, string @class)
        {
            //Simulate entry from booklet
            @class = "CSC3380";
            professor = "AYMOND P";
            string department = "Computer Science";

            //Split string into last name and first initial
            string[] split = professor.Split(' ');
            var lastName = split[0];
            var firstInitial = split[1];

            //Query RMP using professor last name and first initial
            string url = "https://www.ratemyprofessors.com/search/teachers?query=" + lastName + "%20" + firstInitial + "&sid=U2Nob29sLTMwNzE=";

            //Reads content from RMP as string
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string mainPageHtml = await response.Content.ReadAsStringAsync();
                }
                catch { }
            }
        }

    }
}


