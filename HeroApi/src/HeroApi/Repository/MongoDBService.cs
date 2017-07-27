using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Heroes.DB.Model;
using Heroes.Api.Model;

namespace Heroes.Api.Repository
{
    public class MongoDBService : IMongoDBService
    {
        string _connectionString;
        public MongoDBService( IConfiguration config )
        {
            this._connectionString = config.GetValue<string>( "Database" );
        }
        public List<Hero> getAllHeroes()
        {
            return Heroes.DB.HeroSerice.GetHeroList( _connectionString );
        }

        public Hero getHeroByHeroId(int heroId )
        {
            return Heroes.DB.HeroSerice.FindByHeroId( _connectionString, heroId );
        }

        public int? createHero( CreateHeroDto newHero )
        {
            var createHero = new Hero
            {
                HeroId = newHero.HeroId,
                Name = newHero.Name
            };

            return Heroes.DB.HeroSerice.CreateHero( _connectionString, createHero );
        }

        public bool updateHero(HeroDto changedHero )
        {
            var updateHero = Heroes.DB.HeroSerice.FindByHeroId( _connectionString, changedHero.HeroId );
            updateHero.Name = changedHero.Name;
            return Heroes.DB.HeroSerice.ReplaceHero( _connectionString, updateHero );
        }

        public bool deleteHero(int heroId )
        {
            return Heroes.DB.HeroSerice.DeleteHero( _connectionString, heroId );
        }
    }
}
