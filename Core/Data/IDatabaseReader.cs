namespace Core.Data
{
    using System.Collections.Generic;

    public interface IDatabaseReader
    {
        IEnumerable<Path> GetAllPaths();

        IEnumerable<Point> GetPoints(string pointOne, string pointTwo);

        Point GetPoint(string point);
    }
}