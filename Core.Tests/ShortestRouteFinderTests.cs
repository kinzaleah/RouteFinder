namespace Core.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class ShortestRouteFinderTests
    {
        private readonly Point pointA = new Point() { Id = 1, Name = "A" };
        private readonly Point pointB = new Point() { Id = 2, Name = "B" };
        private readonly Point pointC = new Point() { Id = 3, Name = "C" };
        private readonly Point pointD = new Point() { Id = 4, Name = "D" };

        [Fact]
        public void Should_Return_Shortest_Route_From_All_Given_Paths()
        {
            // Arrange
            var routeOne = new Route() { Paths = new List<Path>
            {
                new Path {Id = 1, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20}
            }};

            var routeTwo = new Route()
            {
                Paths = new List<Path>
            {
                new Path {Id = 2, PointOne = this.pointA, PointTwo = this.pointB, Distance = 4},
                new Path {Id = 3, PointOne = this.pointB, PointTwo = this.pointD, Distance = 5},
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