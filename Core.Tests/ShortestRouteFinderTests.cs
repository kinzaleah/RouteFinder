namespace Core.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class ShortestRouteFinderTests
    {
        [Fact]
        public void Should_Return_Shortest_Route_From_All_Given_Paths()
        {
            // Arrange
            var routeOne = new Route() { Paths = new List<Path>
            {
                new Path {Id = 1, PointOne = Points.A, PointTwo = Points.D, Distance = 20}
            }};

            var routeTwo = new Route()
            {
                Paths = new List<Path>
            {
                new Path {Id = 2, PointOne = Points.A, PointTwo = Points.B, Distance = 4},
                new Path {Id = 3, PointOne = Points.B, PointTwo = Points.D, Distance = 5},
            }};

            var possibleRoutes = new List<Route>
            {
                routeOne,
                routeTwo
            };

            var sut = new ShortestRouteFinder();

            // Act
            var result = sut.GetShortestRoute(possibleRoutes);

            // Assert

            result.TotalDistance.Should().Be(9);
            result.Should().BeEquivalentTo(routeTwo);
        }
    }
}