using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Renter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepositoryService movieRepositoryService;
        public HomeController(IMovieRepositoryService movieRepositoryService)
        {
            this.movieRepositoryService = movieRepositoryService;
        }
        public IActionResult Index()
        {
            var latestMovies = movieRepositoryService.Queryable().OrderByDescending(x => x.CreatedDate).Take(6).ToList();
            
            return View(latestMovies);
        }
    }
}