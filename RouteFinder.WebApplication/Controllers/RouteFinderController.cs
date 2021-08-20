namespace RouteFinder.WebApplication.Controllers
{
    using Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class RouteFinderController : ControllerBase
    {
        private readonly IRouteFinder routeFinder;

        public RouteFinderController(IRouteFinder routeFinder)
        {
            this.routeFinder = routeFinder;
        }

        [HttpGet]
        public IActionResult Get(string startPoint, string endPoint)
        {
            
            // TODO - move into new method in Routefinder
            if (string.IsNullOrEmpty(startPoint) || string.IsNullOrEmpty(endPoint))
            {
                return this.BadRequest("Must supply startPoint and endPoint");
            }

            var shortestRouteList = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            return this.Ok(shortestRouteList);
        }
    }
}
