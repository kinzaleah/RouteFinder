namespace Core.Data
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;

    public class DatabaseReader : IDatabaseReader
    {
        private const string ConnectionString = @"Server=LT229\SQLEXPRESS;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";
        public IEnumerable<Path> GetAllPaths()
        {
            List<Path> paths;

            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT 
                                paths.Id, 
                                p1.Id as PointOneId,
                                p2.Id as PointTwoId,
                                p1.name as PointOneName, 
                                p2.name as PointTwoName, 
                                paths.Distance 
                            FROM Paths 
                            JOIN Points as p1 
                            ON Paths.PointOne=p1.id 
                            JOIN Points as p2 
                           ON Paths.PointTwo=p2.id";

                paths = connection.Query<Path>(query).ToList();
            }

            // Need to convert to Paths!!!

            //var paths = new List<Path>();

            //foreach (var point in points)
            //{
            //    paths.Add( new Path() { Id = point.Id});
            //}

            return paths;

            

            //return new List<Path>
            //{
            //    new Path {Id = 1, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
            //    new Path {Id = 2, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
            //    new Path {Id = 3, PointOne = Points.A, PointTwo = Points.C, Distance = 15},
            //    new Path {Id = 4, PointOne = Points.A, PointTwo = Points.B, Distance = 5},
            //    new Path {Id = 5, PointOne = Points.B, PointTwo = Points.C, Distance = 5}
            //};
        }
    }
}