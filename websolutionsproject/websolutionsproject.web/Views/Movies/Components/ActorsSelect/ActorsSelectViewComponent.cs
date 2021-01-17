using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Actors;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Views.Movies.Components.ActorsSelect
{
    public class ActorsSelectViewComponent : ViewComponent
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public ActorsSelectViewComponent(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<GetActorModel> getMovieModels = await GetActors();

            return View("MovieSelectActors", getMovieModels);

        }

        private async Task<List<GetActorModel>> GetActors()
        {
            List<GetActorModel> getActorModels = await _moviemindAPIService.GetModels<GetActorModel>("Actors");

            return getActorModels;
        }
    }
}
