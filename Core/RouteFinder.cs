using System;
using System.Collections.Generic;

namespace Core
{
    public class RouteFinder
    {
        private readonly IRouteExplorer routeExplorer;
        private readonly IShortestRouteFinder shortestRouteFinder;
        private readonly List<Path> paths;

        public RouteFinder(List<Path> paths, IRouteExplorer routeExplorer, IShortestRouteFinder shortestRouteFinder)
        {
            this.paths = paths;
            this.routeExplorer = routeExplorer;
            this.shortestRouteFinder = shortestRouteFinder;
        }
        
        public Route CalculateShortestRoute(Points startPoint, Points endPoint)
        {
            var allPossibleRoutes = this.routeExplorer.GetAllPossibleRoutes(this.paths, startPoint, endPoint);

            var shortestRoute = this.shortestRouteFinder.GetShortestRoute(allPossibleRoutes);

            return shortestRoute;

        }
    }
}