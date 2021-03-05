using System;

namespace RouteFinder.ConsoleApp
{
    using System.Collections.Generic;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {

            var paths = new List<Path>
                {
                    new Path {Id = 1, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
                    new Path {Id = 2, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
                    new Path {Id = 3, PointOne = Points.A, PointTwo = Points.C, Distance = 15},
                    new Path {Id = 4, PointOne = Points.A, PointTwo = Points.B, Distance = 5},
                    new Path {Id = 5, PointOne = Points.B, PointTwo = Points.C, Distance = 5}
                };

            var userStartPoint = args[0].ToUpper();
            var userEndPoint = args[1].ToUpper();

            var startPointEnum = (Core.Points)Enum.Parse(typeof(Core.Points), userStartPoint);
            var endPointEnum = (Core.Points)Enum.Parse(typeof(Core.Points), userEndPoint);

            //Console.WriteLine("Welcome to my application! I will calculate the shortest route if you give me a start and end point.");
            //Console.WriteLine("Please enter your start point - (A-D)");

            //var userStartPoint = Console.ReadLine()?.ToUpper();
            //var startPointEnum = (Core.Points)Enum.Parse(typeof(Core.Points), userStartPoint);

            //Console.WriteLine($"You have chosen '{userStartPoint}' as your start point.");
            //Console.WriteLine("Please choose an end point - (A-D)");

            //var userEndPoint = Console.ReadLine()?.ToUpper();
            //var endPointEnum = (Core.Points)Enum.Parse(typeof(Core.Points), userEndPoint);

            //Console.WriteLine($"You have chosen '{userEndPoint}' as your start point.");

            var routeFinder = new RouteFinder(
                new RouteExplorer(),
                new ShortestRouteFinder(), 
                paths, 
                new RouteValidator());

            var shortestRouteList = routeFinder.CalculateShortestRoute(startPointEnum, endPointEnum);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The shortest route for {userStartPoint} to {userEndPoint} is: ");

            foreach (var path in shortestRouteList.Paths)
            {
                Console.WriteLine($"Path {path.Id}: {path.PointOne} to {path.PointTwo} - Distance {path.Distance}");
            }



        }
    }
}
