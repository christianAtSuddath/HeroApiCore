using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Heroes.DB.Model;
using Heroes.Api.Model;

namespace Heroes.Api.Repository
{
    public interface IMongoDBService
    {
        List<Hero> getAllHeroes();
        Hero getHeroByHeroId( int heroId );

        Hero createHero( CreateHeroDto newHero );
        bool updateHero( HeroDto changedHero );
        bool deleteHero( int heroId );
    }
}
