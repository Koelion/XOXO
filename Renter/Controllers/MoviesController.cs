using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Renter.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieRepositoryService movieRepositoryService;

        public MoviesController(IMovieRepositoryService movieRepositoryService)
        {
            this.movieRepositoryService = movieRepositoryService;
        }

        
        public IActionResult Index()
        {
            var latestMovies = movieRepositoryService.Queryable().OrderByDescending(x => x.CreatedDate).ToList();

            return View(latestMovies);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return View();
        }
    }
}