using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Views.Actors.Components.Movies
{
    public class MoviesSelectViewComponent : ViewComponent
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public MoviesSelectViewComponent(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<GetMovieModel> getMovieModels = await GetMovies();

            return View("ActorSelectMovies", getMovieModels);

        }

        private async Task<List<GetMovieModel>> GetMovies()
        {
            List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("Movies");

            return getMovieModels;
        }
    }
}
