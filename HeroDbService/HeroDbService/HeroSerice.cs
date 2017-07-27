using System;
using System.Collections.Generic;
using System.Linq;


using MongoDB.Driver;

using Heroes.DB.Model;

namespace Heroes.DB
{
    public static class HeroSerice
    {

        private const string _HERODB = "herodb";
        private const string _HEROCOLLECTION = "herocollection";
    private static IMongoDatabase getMongoDb(string connStr)
    {
        var client = new MongoClient ( connStr );
        return client.GetDatabase( _HERODB );
    }
        private static IMongoCollection<Hero> getHeroCollection(string connStr )
        {
            return getMongoDb( connStr ).GetCollection<Hero>( _HEROCOLLECTION );
        }

        public static List<Hero> GetHeroList(string connStr )
        {
            return getHeroCollection(connStr).Find(m => true).ToList();
        }

        public static Hero FindByHeroId(string connStr, int HeroId )
        {
            return getHeroCollection( connStr ).Find( h => h.HeroId == HeroId ).FirstOrDefault();
        }

        public static Hero FindByHeroName( string connStr, string HeroName )
        {
            return getHeroCollection( connStr ).Find( h => h.Name == HeroName ).FirstOrDefault();
        }

        public static Boolean CreateHero( string connStr, Hero newHero )
        {
            bool bSuccess = true;
            try
            {
                getHeroCollection( connStr ).InsertOne( newHero );                
                return bSuccess;
            }
            catch ( Exception e )
            {
                throw new Exception( $"CreateHero: {e.Message}", e );
            }


        }

        public static Boolean ReplaceHero(string connStr, Hero updateHero )
        {
            bool bSuccess = false;
            try {
                var result = getHeroCollection( connStr ).ReplaceOne( h => h.HeroId == updateHero.HeroId, updateHero );
                if ( result.IsAcknowledged )
                {
                    if ( result.IsModifiedCountAvailable )
                    {
                        if ( result.ModifiedCount > 0 )
                        {
                            bSuccess = true;
                        }
                    }
                }
                return bSuccess;
            }
            catch(Exception e )
            {
                throw new Exception( $"ReplaceHero: {e.Message}", e );
            }
            
        }

        public static Boolean DeleteHero( string connStr, int HeroId )
        {
            bool bSuccess = true;
            try
            {
                getHeroCollection( connStr ).DeleteOne(h => h.HeroId == HeroId);
                return bSuccess;
            }
            catch ( Exception e )
            {
                throw new Exception( $"DeleteHero: {e.Message}", e );
            }


        }
    }
}
