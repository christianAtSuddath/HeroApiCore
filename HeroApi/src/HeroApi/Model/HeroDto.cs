using System;

namespace Heroes.Api.Model
{
    public class HeroDto
    {

            public Guid Id { get; set; }
            
            public int HeroId { get; set; }

            public string Name { get; set; }

            public DateTime Created { get; set; }

            public DateTime Updated { get; set; }
     
    }
}
