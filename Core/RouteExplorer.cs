using System.Collections.Generic;

namespace Core
{
    using System.Linq;

    public class RouteExplorer : IRouteExplorer
    {
        public IEnumerable<Route> GetAllPossibleRoutes(List<Path> paths, Point startPoint, Point endPoint)
        {
            var routesThatWork = new List<Route>();
            var routeWeAreExploring = new Route();

            LoopThroughRemainingPaths(paths, startPoint, endPoint, routeWeAreExploring, routesThatWork);

            return routesThatWork;
        }

        private void LoopThroughRemainingPaths(
            List<Path> paths,
            Point startPoint,
            Point endPoint,
            Route routeWeAreExploring,
            List<Route> routesThatWork
        )
        {
            foreach (var nextPath in paths)
            {
                var nextPathMatchesForwards = startPoint == nextPath.PointOne;
                var nextPathMatchesBackwards = startPoint == nextPath.PointTwo;

                var subPath = new Route();
                subPath.Paths.AddRange(routeWeAreExploring.Paths);

                if (nextPathMatchesForwards || nextPathMatchesBackwards)
                {
                    subPath.Paths.Add(nextPath);
                }
                else
                {
                    continue;
                }

                var nextPathEndsHere = (nextPathMatchesForwards && nextPath.PointTwo == endPoint) ||
                                       (nextPathMatchesBackwards && nextPath.PointOne == endPoint);

                if (nextPathEndsHere)
                {
                    routesThatWork.Add(subPath);
                }
                else
                {
                    var remainingPaths = paths.Where(x => x.Id != nextPath.Id).ToList();
                    var newStartPoint = nextPathMatchesForwards ? nextPath.PointTwo : nextPath.PointOne;

                    LoopThroughRemainingPaths(remainingPaths, newStartPoint, endPoint, subPath, routesThatWork);
                }
            }
        }
    }
}