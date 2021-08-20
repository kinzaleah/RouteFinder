namespace Core
{
    public interface IRouteFinder
    {
        Route CalculateShortestRoute(string startPoint, string endPoint);

        Route CalculateShortestRoute(Point startPoint, Point endPoint);
    }
}