
namespace Core
{
    using System;
    using System.Linq;
    using Core.Data;

    public class RouteFinder : IRouteFinder
    {
        private readonly IRouteExplorer routeExplorer;
        private readonly IShortestRouteFinder shortestRouteFinder;
        private readonly IDatabaseReader databaseReader;
        private readonly IRouteValidator routeValidator;


        public RouteFinder(IRouteExplorer routeExplorer, IShortestRouteFinder shortestRouteFinder, IDatabaseReader databaseReader, IRouteValidator routeValidator)
        {
            this.routeExplorer = routeExplorer;
            this.shortestRouteFinder = shortestRouteFinder;
            this.databaseReader = databaseReader;
            this.routeValidator = routeValidator;
        }

        public Route CalculateShortestRoute(string startPoint, string endPoint)
        {
            var points = this.databaseReader.GetPoints(startPoint, endPoint).ToList();

            //var result = this.CalculateShortestRoute(points.First(x => x.Name == startPoint), points.First(x => x.Name == endPoint));

            var pointStartPoint = this.databaseReader.GetPoint(startPoint);
            var pointEndPoint = this.databaseReader.GetPoint(endPoint);

            var result = this.CalculateShortestRoute(pointStartPoint, pointEndPoint);

            return result;
        }
        
        public Route CalculateShortestRoute(Point startPoint, Point endPoint)
        {
            this.routeValidator.ValidateInput(startPoint, endPoint);

            var paths = this.databaseReader.GetAllPaths().ToList();
            
            var allPossibleRoutes = this.routeExplorer.GetAllPossibleRoutes(paths, startPoint, endPoint);

            var shortestRoute = this.shortestRouteFinder.GetShortestRoute(allPossibleRoutes);

            return shortestRoute;
        }
    }
}