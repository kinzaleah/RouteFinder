using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class RouteFinderTests
    {
        [Fact]
        public void Given_Two_Possible_Routes_RouteFinder_Should_Find_The_Shortest_One()
        {
            // Arrange
            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1 },
                new Path { Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4 },
                new Path { Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3 },
                new Path { Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20 }
            };
            var routeFinder = new RouteFinder(routes, new RouteExplorer(), new ShortestRouteFinder());
            // mocking the routeExplorer? next time
            var startPoint = Points.A;
            var endPoint = Points.D;

            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            result.Paths.Should().BeEquivalentTo(
                new List<Path>
                {
                    new Path { Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1 },
                    new Path { Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4 },
                    new Path { Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3 }

                }
            );
        }
    }
}