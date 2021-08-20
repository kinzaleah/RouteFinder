namespace Core.Data
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Microsoft.Extensions.Configuration;

    //using Microsoft.Extensions.Configuration;

    public class DatabaseReader : IDatabaseReader
    {

        private readonly IConfiguration config;

        public DatabaseReader(IConfiguration config)
        {
            this.config = config;
        }


        //private const string ConnectionString = @"Server=LT229\SQLEXPRESS;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";
        //private const string ConnectionString = @"Server=LT187;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";

        public IEnumerable<Path> GetAllPaths()
        {
            var paths = new List<Path>();

            using (var connection = new SqlConnection(this.config.GetConnectionString("DefaultConnection")))
            {
                const string query = @"SELECT 
                                        [P].Id as Id, 
                                        [P].Distance,                                         
                                        [PTS_1].Id as Id,
                                        [PTS_1].Name,
                                        [PTS_2].Id as Id,
                                        [PTS_2].Name
                                    FROM [Paths] [P]
                                    JOIN [Points] [PTS_1] ON [P].PointOne = [PTS_1].Id
                                    JOIN [Points] [PTS_2] ON [P].PointTwo = [PTS_2].Id;";

                var result = connection.Query<Path, Point, Point, Path>(
                    query,
                    (path, pointOne, pointTwo) =>
                    {
                        path.PointOne = pointOne;
                        path.PointTwo = pointTwo;
                        return path;
                    });

                paths.AddRange(result.OrderBy(x => x.Id));
            }

            return paths;
        }

        public IEnumerable<Point> GetPoints(string pointOne, string pointTwo)
        {
            var points = new List<Point>();

            using (var connection = new SqlConnection(this.config.GetConnectionString("DefaultConnection")))
            {

                const string query = @"SELECT 
                                        name, id
                                     FROM
                                        Points
                                     WHERE 
                                        name = @pointOne
                                        OR
                                        name = @pointTwo;
                                        ";

                var result = connection.Query<Point>(
                    query,
                    //(point, p1, p2) =>
                    //{
                    //    point.Id = p1.Id;
                    //    point.Id = p2.Id;
                    //    point.Name = p1.Name;
                    //    point.Name = p2.Name;
                    //    return point;
                    //},
                    new { pointOne, pointTwo });

                points.AddRange(result.OrderBy(x => x.Id));
            }

            return points;
        }

        public Point GetPoint(string pointName)
        {
            var point = new Point();

            using (var connection = new SqlConnection(this.config.GetConnectionString("DefaultConnection")))
            {

                const string query = @"SELECT 
                                        name, id
                                     FROM
                                        Points
                                     WHERE 
                                        name = @point;
                                        ";

                var result = connection.QuerySingle<Point>(
                    query,
                    //(point, p1, p2) =>
                    //{
                    //    point.Id = p1.Id;
                    //    point.Id = p2.Id;
                    //    point.Name = p1.Name;
                    //    point.Name = p2.Name;
                    //    return point;
                    //},
                    new { point = pointName });

                point.Id = result.Id;
                point.Name = result.Name;
            }

            return point;
        }
    }
}