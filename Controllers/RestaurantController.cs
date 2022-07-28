using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestaurantRaterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _context;
        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }
    
        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromForm] RestaurantEdit model)
        {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Ratings.Add(new Rating()
        {
            FoodScore = model.FoodScore,
            CleanlinessScore = model.CleanlinessScore,
            AtmosphereScore = model.AtmosphereScore,
            RestaurantId = model.RestaurantId,

        });
        await _context.SaveChangesAsync();
        return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
        var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
        // List<RestaurantListItem> restaurantList = await _context.Restaurants.Select(r => new RestaurantListItem()
        // {
        //     Id = r.Id,
        //     Name = r.Name,
        //     Location = r.Location,
        //     AverageFoodScore = r.AverageFoodScore,
        // }).ToListAsync();
        return Ok(restaurants);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
        var restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);

        if(restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
    {
        var oldRestaurant = await _context.Restaurants.FindAsync(id);
        if(oldRestaurant == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        if(!string.IsNullOrEmpty(model.Name))
        {
            oldRestaurant.Name = model.Name;
        }
        if(!string.IsNullOrEmpty(model.Location))
        {
            oldRestaurant.Location = model.Location;
        }
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        if(restaurant == null)
        {
            return NotFound();
        }
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
        return Ok();
    }
    }
}
