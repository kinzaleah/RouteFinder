namespace Core
{
    public interface IRouteFinder
    {
        Route CalculateShortestRoute(Point startPoint, Point endPoint);
    }
}