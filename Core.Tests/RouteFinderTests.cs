using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    using Moq;

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

        [Fact]
        public void RouteFinder_Should_Call_RouteExplorer()
        {
            // Arrange
            var startPoint = Points.A;
            var endPoint = Points.D;

            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1 },
                new Path { Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4 },
                new Path { Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3 },
                new Path { Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20 }
            };

            var expected = new List<Route>
            {
                new Route()
                {
                    Paths = new List<Path>()
                }
            };


            var mockRouteExplorer = new Mock<IRouteExplorer>();
            
            mockRouteExplorer
                .Setup(x => x.GetAllPossibleRoutes(It.IsAny<List<Path>>(), It.IsAny<Core.Points>(), It.IsAny<Core.Points>()))
                .Returns(expected);

            var mockShortestRouteFinder = new Mock<IShortestRouteFinder>();

            var mockShortestRouteFinderResult = new Route();

            mockShortestRouteFinder
                .Setup(x => x.GetShortestRoute(expected))
                .Returns(mockShortestRouteFinderResult);

            var routeFinder = new RouteFinder(routes, mockRouteExplorer.Object, mockShortestRouteFinder.Object);
            
            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            mockRouteExplorer.Verify(x => x.GetAllPossibleRoutes(routes, startPoint, endPoint), Times.Once);
            mockShortestRouteFinder.Verify(x => x.GetShortestRoute(expected), Times.Once);
            mockShortestRouteFinderResult.Should().Be(result);
        }
    }
}