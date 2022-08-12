using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerosController : ControllerBase
    {
        private readonly DataContext dataContext;

        public HerosController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<Hero>> Get()
        {         
            return Ok(await dataContext.Heroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Hero>>> Get(int id)
        {
            var hero =  await dataContext.Heroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> AddHero(Hero hero)
        {
            dataContext.Heroes.Add(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Heroes.ToListAsync());
        }

        

        [HttpPut("{id}")]
        public async Task<ActionResult<Hero>> UpdateHero(Hero heroRequest)
        {
            var hero = await dataContext.Heroes.FindAsync(heroRequest.Id);
            if (hero==null)
            {
                return BadRequest("Hero not found");
            }

            hero.Name = heroRequest.Name;
            hero.Place = heroRequest.Place;

            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Heroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Hero>>> Delete(int id)
        {
            var hero = await dataContext.Heroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            dataContext.Heroes.Remove(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Heroes.ToListAsync());
        }
    }
}
