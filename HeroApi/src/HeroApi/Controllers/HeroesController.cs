using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Heroes.Api.Repository;
using Heroes.Api.Model;

namespace Heroes.Api.Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        IConfiguration _Configuration;
        IMongoDBService _DbService;
        ILogger _Logger;


        public HeroesController(IConfiguration config, IMongoDBService dbService, ILogger<HeroesController> logger )
        {
            _Configuration = config;
            _DbService = dbService;
            _Logger = logger;
        }
        // GET api/heroes
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                return Json( _DbService.getAllHeroes() );
            }
            catch(Exception e )
            {
                _Logger.LogError( 1, e, "GetAllHeroes", null );
                return Json( StatusCode( 500 ) );
            }
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var h = _DbService.getHeroByHeroId(heroId: id);
                if ( h == null )
                {
                    return Json( NotFound( id ) );
                }
                return Json( h );
            }
            catch ( Exception e )
            {
                _Logger.LogError( 2, e, "getHeroByHeroId", null );
                return Json( StatusCode( 500 ) );
            }
        }

        // POST api/heroes
        [HttpPost]
        public JsonResult Post([FromBody]HeroDto value)
        {
            try
            {
                if ( _DbService.createHero(value) )
                {
                    return Json( Created($"api/heroes/{value.HeroId}",value) );
                }
                else
                {
                    return Json( StatusCode( 401 ) );
                }
            }
            catch ( Exception e )
            {
                _Logger.LogError( 3, e, "getHeroByHeroId", null );
                return Json( StatusCode( 500 ) );
            }
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]HeroDto value)
        {
            try
            {
                if ( _DbService.updateHero( value ) )
                {
                    return Ok();
                }
                else
                {
                    return StatusCode( 401 ) ;
                }
            }
            catch ( Exception e )
            {
                _Logger.LogError( 4, e, "UpdateHero", null );
                return StatusCode( 500 ) ;
            }
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                if ( _DbService.deleteHero( heroId: id ) )
                {
                    return Ok();
                }
                else
                {
                    return StatusCode( 401 );
                }
            }
            catch(Exception e )
            {
                _Logger.LogError( 5, e, "DeleteHero", null );
                return StatusCode( 500 );
            }
        }
    }
}
