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
    }
    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromForm] RestaurantEdit model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Restaurants.Add(new Restaurant()
        {
            Name = model.Name,
            Location = model.Location,
        });
        await _context.SaveChangesAsync();
        return OkResult();
    }
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
        List<RestaurantListItem> restaurantList = restaurantdata.Select(r => new RestaurantListItem()
        {
            Id = r.Id,
            Name = r.Name,
            Location = r.Location,
            AverageScore = r.AverageScore,
        }).ToList();
        return OkResult(restaurantList);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);

        if(restaurant == null)
        {
            return NotFoundResult();
        }
        return OkResult(restaurant);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
    {
        var oldRestaurant = await _context.Restaurants.FindAsync(id);
        if(oldRestaurant == null)
        {
            return NotFoundResult();
        }
        if (!ModelState.IsValid)
        {
            return BadRequestResult();
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
        return OkResult();
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var restaurant = await _context.Restauants.FindAsync(id);
        if(restaurant == null)
        {
            return NotFoundResult();
        }
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
        return OkResult();
    }
    
}
