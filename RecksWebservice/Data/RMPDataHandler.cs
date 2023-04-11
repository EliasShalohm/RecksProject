
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PuppeteerSharp;
using RecksWebservice.Types;
using Syncfusion.Blazor.PivotView;
using System.Diagnostics;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace RecksWebservice.Data
{
    //Source for HTML Scraping
    //https://www.scrapingdog.com/blog/web-scraping-with-csharp/#Selenium_in_C-sharp_Part-I

    public class RMPDataHandler
    {
        private Professor professor = new Professor(); // Initialize professor object
        private List<string> classes = new List<string>();
        private double rating; // add private field for rating

        public void TearDown()
        {
            //_webDriver.Quit();
        }

        public double GetProfessorRating()
        {
            return rating; // return the private field
        }
        public async Task GetProfessorData(string professorName)
        {
            //string url = $"https://www.ratemyprofessors.com/search/teachers?query={professorName}&sid=U2Nob29sLTMwNzE=";
            string lowercase_professor_name = professorName.ToLower();
            switch (lowercase_professor_name)
            {
                case "patti aymond":
                    rating = 4.5;
                    break;
                case "anas nash mahmoud":
                    rating = 5.0;
                    break;
                case "nash":
                    rating = 5.0;
                    break;
                case "sukhamay kundu":
                    rating = 2.6;
                    break;
                case "kundu":
                    rating = 2.6;
                    break;
                case "nabanita bhattacharyya":
                    rating = 5.0;
                    break;
                case "dr. nita":
                    rating = 5.0;
                    break;
                case "nathan brener":
                    rating = 2;
                    break;
                case "nate brener":
                    rating = 2.4;
                    break;
                case "colin turley":
                    rating = 5.0;
                    break;
                case "raymond stock":
                    rating = 5.0;
                    break;
                case "paul anderson":
                    rating = 4.6;
                    break;
                case "qingyang wang":
                    rating = 4.8;
                    break;
                case "qinyang wang":
                    rating = 4.8;
                    break;
                case "dr. wang":
                    rating = 4.8;
                    break;
                default:
                    rating = -1;
                    break;
            }
        }
    }
}


