using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.Actors
{
    public class GetActorModel : BaseActorModel
    {
        [Required]
        public Guid Id { get; set; }
        

    }
}
