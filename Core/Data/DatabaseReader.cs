using System.ComponentModel.Design.Serialization;

namespace Core.Data
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;

    public class DatabaseReader : IDatabaseReader
    {
        //private const string ConnectionString = @"Server=LT229\SQLEXPRESS;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";
        private const string ConnectionString = @"Server=LT187;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";

        public IEnumerable<Path> GetAllPaths()
        {
            var paths = new List<Path>();

            using (var connection = new SqlConnection(ConnectionString))
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
    }
}