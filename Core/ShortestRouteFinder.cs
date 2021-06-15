namespace Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShortestRouteFinder : IShortestRouteFinder
    {
        public Route GetShortestRoute(IEnumerable<Route> routes)
        {
            return routes.Any() ? routes.OrderBy(x => x.TotalDistance).First() : null;
        }
    }
}