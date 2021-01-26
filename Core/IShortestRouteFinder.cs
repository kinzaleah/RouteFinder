namespace Core
{
    using System.Collections.Generic;

    public interface IShortestRouteFinder
    {
        Route GetShortestRoute(IEnumerable<Route> routes);
    }
}