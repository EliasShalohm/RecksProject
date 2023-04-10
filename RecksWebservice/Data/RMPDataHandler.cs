using RecksWebservice.Types;
using HtmlAgilityPack;


namespace RecksWebservice.Data
{

    public class RMPDataHandler
    {
        private Professor professor = new Professor(); // Initialize professor object
        private List<string> classes = new List<string>();
        private int rating; // add private field for rating

        public int GetProfessorRating()
        {
            return rating; // return the private field
        }
        public async Task GetProfessorData(string professorName)
        {
            // Query RMP using professor last name
            string url = $"https://www.ratemyprofessors.com/search/teachers?query={professorName}&sid=U2Nob29sLTMwNzE=";

            // Reads content from RMP as string
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string mainPageHtml = await response.Content.ReadAsStringAsync();

                    // Parse search results page and extract professor info for the first result
                    //var doc = new HtmlDocument();
                    //doc.LoadHtml(mainPageHtml);
                    var web = new HtmlWeb();
                    var doc = web.Load("https://www.ratemyprofessors.com/search/teachers?query=Aymond%20Patti&sid=U2Nob29sLTMwNzE=");

                    var searchResult = doc.DocumentNode.SelectSingleNode("//div[@class='CardNumRating__CardNumRatingNumber-sc-17t4b9u-2 jvzqBM']");

                    if (searchResult != null)
                    {
                        //var name = searchResult.SelectSingleNode(".//div[contains(@class, 'CardName__Name')]")?.InnerText.Trim();
                        var ratingText = searchResult.SelectSingleNode(".//div[contains(@class, 'CardRating__Rating')]")?.InnerText.Trim();
                        var numRatings = searchResult.InnerText;
                        var cardNumRatingNumber = searchResult?.InnerText.Trim();
                        rating = int.Parse(numRatings);
                        //professor.SetName(name);
                        if (ratingText.ToLower() == "n/a")
                        {
                            rating = -1; // professor has no rating
                        }
                        else
                        {
                            rating = int.Parse(ratingText.Split('.')[0]); // set the private field to the extracted rating
                        }
                    }
                    else
                    {
                        rating = -1; // professor not found
                    }
                }
                catch { }
            }
        }
    }
    }


