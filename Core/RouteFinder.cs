using System;
using System.Collections.Generic;

namespace Core
{
    public class RouteFinder
    {
        private readonly IRouteExplorer routeExplorer;
        private readonly IShortestRouteFinder shortestRouteFinder;
        private readonly List<Path> paths;
        private readonly IRouteValidator routeValidator;

        public RouteFinder(IRouteExplorer routeExplorer, IShortestRouteFinder shortestRouteFinder, List<Path> paths, IRouteValidator routeValidator)
        {
            this.routeExplorer = routeExplorer;
            this.shortestRouteFinder = shortestRouteFinder;
            this.paths = paths;
            this.routeValidator = routeValidator;
        }
        
        public Route CalculateShortestRoute(Points startPoint, Points endPoint)
        {
            this.routeValidator.ValidateInput(startPoint, endPoint);

            var allPossibleRoutes = this.routeExplorer.GetAllPossibleRoutes(this.paths, startPoint, endPoint);

            var shortestRoute = this.shortestRouteFinder.GetShortestRoute(allPossibleRoutes);

            return shortestRoute;

        }
    }
}