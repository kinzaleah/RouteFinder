namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Core;
    using Core.Data;
    using Dapper;

    class Program
    {
        private const string ConnectionString = @"Server=LT229\SQLEXPRESS;Database=RouteFinder;User Id=routefinder_user;Password=Welcome1!;";
        // private const string ConnectionString = @"Data Source=LT229\SQLEXPRESS;Initial Catalog=RouteFinder;Integrated Security=True";

        static void Main(string[] args)
        {
            
            //using (var conn = new SqlConnection(ConnectionString))
            //{
            //    var query = "SELECT id, name FROM points";

            //    using (var cmd = new SqlCommand(query))
            //    {
            //        cmd.Connection = conn;
            //        conn.Open();

            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                points.Add(new Point()
            //                {
            //                    Id = Convert.ToInt32(reader["id"]),
            //                    Name = reader["name"].ToString(),
            //                });
            //            }
            //        }
            //    }
            //}

            //List<Point> points;

            //using (var connection = new SqlConnection(ConnectionString))
            //{
            //    var query = "SELECT id, name FROM points";

            //    points = connection.Query<Point>(query).ToList();
            //}
            //var dbReader = new DatabaseReader();
            //List<Path> paths = dbReader.GetAllPaths().ToList();


            //foreach (var path in paths)
            //{
            //    Console.WriteLine($"{path.Id} {path.PointOne} {path.PointTwo} {path.Distance}");
            //}


            var obj = new object();

            var objType = obj.GetType();

            //var newInteger = 7;

        }
    }
}
