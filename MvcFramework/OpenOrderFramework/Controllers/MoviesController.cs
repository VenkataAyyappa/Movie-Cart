using OpenOrderFramework.Class;
using OpenOrderFramework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace OpenOrderFramework.Controllers
{
    public class MoviesController : Controller
    {
 
        public ActionResult Index(string peopleName, int? page)
        {
            if (page != null)
                CallAPI(peopleName, Convert.ToInt32(page));

            Models.Movies theMovieDb = new Models.Movies();
            theMovieDb.searchText = peopleName;
            return View(theMovieDb);
        }

        [HttpPost]
        public ActionResult Index(Models.Movies theMovieDb, string searchText)
        {
            if (ModelState.IsValid)
            {
                CallAPI(searchText, 0);
            }
            return View(theMovieDb);
        }
        public void CallAPI(string searchText, int page)
        {
            int pageNo = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);

            /*Calling API */
            string apiKey = "599bfa492ecd7abfb724d92bacb4f8fb";
            System.Net.HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&page=" + pageNo + "&include_adult=false") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
           

  
            ResponseSearchMovie rootObject = JsonConvert.DeserializeObject<ResponseSearchMovie>(apiResponse);
            StringBuilder sb = new StringBuilder();
            if (rootObject.results.Count()>0)
            {
                sb.Append("<div class=\"resultDiv\"><p class ='as'>Movies</p>");
            }
            else
            {
                sb.Append("<div class=\"resultDiv\"><p class ='as'>>not Movies found</p>");
            }


            foreach (Result result in rootObject.results)
            {
                string image = result.poster_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + result.poster_path;
                string link = Url.Action("Getmovie", "Movies", new { id = result.id });
                sb.Append("<div class=\"result\" resourceId=\"" + result.id + "\">" + "<a href=\"" + link + "\"><img src=\"" + image + "\" />" + "<p>" + result.name + "</a></p></div>");
            }
            ViewBag.Result = sb.ToString();

            int pageSize = 20;
            Paging Paging = new Paging();
            Paging.CurrentPage = pageNo;
            Paging.TotalItems = rootObject.total_results;
            Paging.ItemsPerPage = pageSize;
            ViewBag.Paging = Paging;
        }

        public ActionResult GetMovie(int id)
        {
            /*Calling API*/
            string apiKey = "599bfa492ecd7abfb724d92bacb4f8fb";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
         

   
            Infomovie rootObject = JsonConvert.DeserializeObject<Infomovie>(apiResponse);
            Movies movieData = new Movies();
            movieData.name = rootObject.name;
            movieData.popularity = rootObject.popularity;
            movieData.overview = rootObject.overview;
            movieData.release_date = rootObject.release_date;
            movieData.title = rootObject.title;
            movieData.original_language = rootObject.original_language;
            movieData.original_title = rootObject.original_title;
            movieData.poster_path = rootObject.poster_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + rootObject.poster_path;


            return View(movieData);
        }
    }
}