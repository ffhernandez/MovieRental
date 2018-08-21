using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;
using MovieRental.Models.ViewModels;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre);
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            try
            {
                if (movie.Id == 0)
                    _context.Movies.Add(movie);
                else
                {
                    var editedMovie = _context.Movies.Single(c => c.Id == movie.Id);
                    editedMovie.Name = movie.Name;
                    editedMovie.ReleaseDate = movie.ReleaseDate;
                    editedMovie.DateAdded = movie.DateAdded;
                    editedMovie.Stock = movie.Stock;

                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Movies");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(c => c.Id == id);
            if (movie == null)
                return NotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            //Deleting using GET method for now
            try
            {
                // TODO: Add delete logic here
                _context.Movies.Remove(_context.Movies.Single(c => c.Id == id));
                _context.SaveChanges();

                return RedirectToAction("Index", "Movies");
            }
            catch
            {
                return View();
            }
        }
    }
}