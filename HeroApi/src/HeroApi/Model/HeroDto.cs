using System;

namespace Heroes.Api.Model
{
    public class HeroDto
    {

            public int HeroId { get; set; }

            public string Name { get; set; }

            public DateTime Created { get; set; }

            public DateTime Updated { get; set; }
     
    }
}
