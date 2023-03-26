using RecksWebservice.Types;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using System.Net.Http;


namespace RecksWebservice.Data
{
    public class RMPDataHandler
    {
        private static string Professor = "";
        private List<string> Class = new();

        
        

        public async Task GetProfessorData( string Professor, string Class)
        {
            //Simulate entry from booklet
            Class = "CSC3380";
            Professor = "AYMOND P"; 

            //Split string into last name and first initial
            string[] split = Professor.Split(' ');
            var LastName = split[0];
            var FirstInitial = split[1];
            
            //Query RMP using professor last name and first initial
            string url = "https://www.ratemyprofessors.com/search/teachers?query="+LastName+"%20"+FirstInitial+"&sid=U2Nob29sLTMwNzE=";
            
            //Reads content from RMP as string
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    await response.Content.ReadAsStringAsync();
                }
                catch{}
            }
        }
        
    }
}