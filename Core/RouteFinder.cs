
namespace Core
{
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
        
        public Route CalculateShortestRoute(Point startPoint, Point endPoint)
        {
            var paths = this.databaseReader.GetAllPaths().ToList();
            
            this.routeValidator.ValidateInput(startPoint, endPoint);

            var allPossibleRoutes = this.routeExplorer.GetAllPossibleRoutes(paths, startPoint, endPoint);

            var shortestRoute = this.shortestRouteFinder.GetShortestRoute(allPossibleRoutes);

            return shortestRoute;

        }
    }
}