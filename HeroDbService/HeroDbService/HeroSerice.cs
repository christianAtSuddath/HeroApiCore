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
            var client = new MongoClient(connStr);
            return client.GetDatabase(_HERODB);
        }
        private static IMongoCollection<Hero> getHeroCollection(string connStr)
        {
            return getMongoDb(connStr).GetCollection<Hero>(_HEROCOLLECTION);
        }

        public static List<Hero> GetHeroList(string connStr)
        {
            return getHeroCollection(connStr).Find(m => true).ToList();
        }

        public static int GetNextHeroId(string connStr)
        {
            try
            {
                var maxHeroId = getHeroCollection(connStr)
                                .Find(m => true)
                                .Limit(1)
                                .SortByDescending(h => h.HeroId)
                                .FirstOrDefault()
                                .HeroId;
                if (maxHeroId == null)
                {
                    return 1;
                }
                int id = Convert.ToInt32(maxHeroId) + 1;
                return id;
            }
            catch (Exception e)
            {
                throw new Exception($"GetNextHeroId: {e.Message}", e);
            }

        }

        public static Hero FindByHeroId(string connStr, int HeroId)
        {
            return getHeroCollection(connStr).Find(h => h.HeroId == HeroId).FirstOrDefault();
        }

        public static List<Hero> FindByHeroesName(string connStr, string HeroName)
        {
            return getHeroCollection(connStr).Find(h => h.Name.StartsWith(HeroName)).ToList();
        }

        public static Hero CreateHero(string connStr, Hero newHero)
        {
            int? newHeroId = null;

            if (newHero.HeroId == null)
            {
                newHero.HeroId = 0;
            }
            if ( Convert.ToInt32( newHero.HeroId ) == 0)
            {
                newHero.HeroId = GetNextHeroId(connStr);
            }
            newHeroId = newHero.HeroId;
            newHero.Created = DateTime.UtcNow;
            newHero.Updated = DateTime.UtcNow;
            if (FindByHeroId(connStr, Convert.ToInt32(newHeroId)) != null)
            {
                throw new InvalidOperationException($"Duplicate HeroId ({newHeroId}).");
            }

            try
            {
                getHeroCollection(connStr).InsertOne(newHero);
                return FindByHeroId(connStr, Convert.ToInt32(newHero.HeroId));
            }
            catch (Exception e)
            {
                throw new Exception($"CreateHero: {e.Message}", e);
            }


        }

        public static Boolean ReplaceHero(string connStr, Hero updateHero)
        {
            bool bSuccess = false;
            try
            {
                updateHero.Updated = DateTime.UtcNow;
                var result = getHeroCollection(connStr).ReplaceOne(h => h.HeroId == updateHero.HeroId, updateHero);
                if (result.IsAcknowledged)
                {
                    if (result.IsModifiedCountAvailable)
                    {
                        if (result.ModifiedCount > 0)
                        {
                            bSuccess = true;
                        }
                    }
                }
                return bSuccess;
            }
            catch (Exception e)
            {
                throw new Exception($"ReplaceHero: {e.Message}", e);
            }

        }

        public static Boolean DeleteHero(string connStr, int HeroId)
        {
            bool bSuccess = true;
            try
            {
                getHeroCollection(connStr).DeleteOne(h => h.HeroId == HeroId);
                return bSuccess;
            }
            catch (Exception e)
            {
                throw new Exception($"DeleteHero: {e.Message}", e);
            }


        }
    }
}
