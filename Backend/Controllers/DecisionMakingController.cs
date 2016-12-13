using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class DecisionMakingController : Controller
    {
        public class Place
        {
            public string name, price, distance, funness, quality;
            // Hacky, cuz not sure if it's needed for json.
        }
        public class Vote
        {
            public Place[] places;
        }

        [HttpPost]
        [Route("api/algorithm")]
        public IEnumerable<Place> RunAlgorithm([FromBody] Vote[] votes)
        {
            return from vote in votes from place in vote.places select place;
        }
    }
}
