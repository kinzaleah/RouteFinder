using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace Core.Tests
{
    public class RouteExplorerTests
    {
        private readonly Point pointA = new Point { Id = 1, Name = "A" };
        private readonly Point pointB = new Point { Id = 2, Name = "B" };
        private readonly Point pointC = new Point { Id = 3, Name = "C" };
        private readonly Point pointD = new Point { Id = 4, Name = "D" };
        private readonly Point pointE = new Point { Id = 5, Name = "E" };
        private readonly Point pointH = new Point { Id = 6, Name = "H" };

        [Fact]
        public void Should_Explore_All_Single_Path_Routes_For_Given_Points()
        {
            // Arrange
            var startPoint = this.pointA;
            var endPoint = this.pointD;

            var routes = new List<Path>
            {
                new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                new Path {Id = 5, PointOne = this.pointD, PointTwo = this.pointA, Distance = 14},
                new Path {Id = 6, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5}
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert
            
            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = this.pointD, PointTwo = this.pointA, Distance = 14},
                }
            };

            result.Count().Should().Be(2);
            result.Should().BeEquivalentTo(
                    new List<Route>
                    {
                        expectedOne,
                        expectedTwo
                    }
                );
        }

        [Fact]
        public void Should_Explore_All_Two_Path_Routes_For_Given_Points()
        {
            // Arrange
            var startPoint = this.pointA;
            var endPoint = this.pointD;

            var routes = new List<Path>
            {
                new Path {Id = 4, PointOne = this.pointB, PointTwo = this.pointE, Distance = 20},
                new Path {Id = 3, PointOne = this.pointA, PointTwo = this.pointD, Distance = 15},
                new Path {Id = 5, PointOne = this.pointD, PointTwo = this.pointA, Distance = 14},
                new Path {Id = 6, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                new Path {Id = 7, PointOne = this.pointC, PointTwo = this.pointD, Distance = 5},
                new Path {Id = 8, PointOne = this.pointB, PointTwo = this.pointA, Distance = 5},
                new Path {Id = 9, PointOne = this.pointD, PointTwo = this.pointB, Distance = 5}
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert

            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 6, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                    new Path {Id = 7, PointOne = this.pointC, PointTwo = this.pointD, Distance = 5},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = this.pointD, PointTwo = this.pointA, Distance = 14},
                }
            };

            var expectedThree = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 8, PointOne = this.pointB, PointTwo = this.pointA, Distance = 5},
                    new Path {Id = 9, PointOne = this.pointD, PointTwo = this.pointB, Distance = 5},
                }
            };

            var expectedFour = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 3, PointOne = this.pointA, PointTwo = this.pointD, Distance = 15},
                }
            };

            result.Count().Should().Be(4);
            result.Should().BeEquivalentTo(
                new List<Route>
                {
                    expectedFour,
                    expectedTwo,
                    expectedOne,
                    expectedThree
                }
            );
        }

        [Fact]
    public void Should_Explore_All_Possible_Routes_For_Given_Points()
        {
            // Arrange
            var startPoint = this.pointA;
            var endPoint = this.pointD;

            var routes = new List<Path>
            {
                new Path {Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1},
                new Path {Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4},
                new Path {Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3},
                new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                new Path {Id = 5, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                new Path {Id = 6, PointOne = this.pointC, PointTwo = this.pointE, Distance = 7},
                new Path {Id = 7, PointOne = this.pointH, PointTwo = this.pointB, Distance = 7}
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert
            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1},
                    new Path {Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4},
                    new Path {Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1},
                    new Path {Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4},
                    new Path {Id = 5, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                    new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                }
            };
            
            var expectedThree = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                }
            };

            var expectedFour = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                    new Path {Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3},
                }
            };

            var expectedFive = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = this.pointA, PointTwo = this.pointC, Distance = 5},
                    new Path {Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4},
                    new Path {Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1},
                    new Path {Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20},
                }
            };

            result.Count().Should().Be(5);
            result.Should().BeEquivalentTo(
                    new List<Route>
                    {
                        expectedOne,
                        expectedTwo,
                        expectedThree,
                        expectedFive,
                        expectedFour
                    }
                );
        }
    }
}