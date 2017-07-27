using System;

namespace Heroes.Api.Model
{
    public class CreateHeroDto
    {

            public int? HeroId { get; set; }

            public string Name { get; set; }

            public DateTime? Created { get; set; }

            public DateTime? Updated { get; set; }
     
    }
}
