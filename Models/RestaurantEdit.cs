using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantEdit
    {
        public string Name { get; set; }

        public string Location { get; set;}
        public float FoodScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float AtmosphereScore { get; set; }
        public int RestaurantId { get; set; }
    }
}