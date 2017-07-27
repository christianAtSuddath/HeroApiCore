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
        public ActionResult Get()
        {
            try
            {
                return Json( _DbService.getAllHeroes() );
            }
            catch(Exception e )
            {
                _Logger.LogError( 1, e, "GetAllHeroes", null );
                return StatusCode( 500 );
            }
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var h = _DbService.getHeroByHeroId(heroId: id);
                if ( h == null )
                {
                    return NotFound( id );
                }
                return Json( h );
            }
            catch ( Exception e )
            {
                _Logger.LogError( 2, e, "getHeroByHeroId", null );
                return StatusCode( 500 );
            }
        }

        // POST api/heroes
        [HttpPost]
        public ActionResult Post([FromBody]CreateHeroDto value)
        {
            try
            {
                var h = _DbService.createHero(value);
                if ( h == null )
                {
                    return StatusCode(401);
                }
                else
                {
                    return Json(Created($"api/heroes/{h.HeroId}", h)); 
                }
            }
            catch ( InvalidOperationException ioe)
            {
                return StatusCode(401, ioe.Message);
            }
            catch ( Exception e )
            {
                _Logger.LogError( 3, e, "createHero", null );
                return StatusCode( 500 );
            }
        }
        [HttpPost( "many",Name ="PostMany" )]
        public ActionResult PostMany( [FromBody]CreateHeroDto[] value )
        {
            try
            {
                for ( int i = 0; i < value.Length; i++ )
                {
                    _DbService.createHero( value[ i ] );
                }
                return Created( "api/heroes/", null );
            }
            catch ( Exception e )
            {
                _Logger.LogError( 5, e, "createManyHeroes", null );
                return StatusCode( 500 );
            }
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]HeroDto value)
        {
            try
            {
                if (_DbService.getHeroByHeroId(id) == null)
                {
                    return NotFound(id);
                }
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
                if (_DbService.getHeroByHeroId(id) == null)
                {
                    return NotFound(id);
                }
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
