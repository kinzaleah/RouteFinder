using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace Core.Tests
{
    public class RouteExplorerTests
    {
        [Fact]
        public void Should_Explore_All_Single_Path_Routes_For_Given_Points()
        {
            // Arrange
            var startPoint = Points.A;
            var endPoint = Points.D;

            var routes = new List<Path>
            {
                new Path {Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
                new Path {Id = 5, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
                new Path {Id = 6, PointOne = Points.A, PointTwo = Points.C, Distance = 5}
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert
            
            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
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
            var startPoint = Points.A;
            var endPoint = Points.D;

            var routes = new List<Path>
            {
                new Path {Id = 4, PointOne = Points.B, PointTwo = Points.E, Distance = 20},
                new Path {Id = 3, PointOne = Points.A, PointTwo = Points.D, Distance = 15},
                new Path {Id = 5, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
                new Path {Id = 6, PointOne = Points.A, PointTwo = Points.C, Distance = 5},
                new Path {Id = 7, PointOne = Points.C, PointTwo = Points.D, Distance = 5},
                new Path {Id = 8, PointOne = Points.B, PointTwo = Points.A, Distance = 5},
                new Path {Id = 9, PointOne = Points.D, PointTwo = Points.B, Distance = 5}
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert

            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 6, PointOne = Points.A, PointTwo = Points.C, Distance = 5},
                    new Path {Id = 7, PointOne = Points.C, PointTwo = Points.D, Distance = 5},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = Points.D, PointTwo = Points.A, Distance = 14},
                }
            };

            var expectedThree = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 8, PointOne = Points.B, PointTwo = Points.A, Distance = 5},
                    new Path {Id = 9, PointOne = Points.D, PointTwo = Points.B, Distance = 5},
                }
            };

            var expectedFour = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 3, PointOne = Points.A, PointTwo = Points.D, Distance = 15},
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
            var startPoint = Points.A;
            var endPoint = Points.D;

            var routes = new List<Path>
            {
                new Path {Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1},
                new Path {Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4},
                new Path {Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3},
                new Path {Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
                new Path {Id = 5, PointOne = Points.A, PointTwo = Points.C, Distance = 5},
                new Path {Id = 6, PointOne = Points.C, PointTwo = Points.E, Distance = 7},
                new Path {Id = 7, PointOne = Points.H, PointTwo = Points.B, Distance = 7},
            };

            var sut = new RouteExplorer();

            // Act
            var result = sut.GetAllPossibleRoutes(routes, startPoint, endPoint);

            // Assert

            var expectedOne = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1},
                    new Path {Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4},
                    new Path {Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3},
                }
            };

            var expectedTwo = new Route
            {
                Paths = new List<Path>
                {
                    new Path
                    {
                        Id = 1,
                        PointOne = Points.A,
                        PointTwo = Points.B,
                        Distance = 1
                    },
                    new Path
                    {
                        Id = 2,
                        PointOne = Points.B,
                        PointTwo = Points.C,
                        Distance = 4
                    },
                    new Path
                    {
                        Id = 5,
                        PointOne = Points.A,
                        PointTwo = Points.C,
                        Distance = 5
                    },
                    new Path
                    {
                        Id = 4, 
                        PointOne = Points.A, 
                        PointTwo = Points.D, 
                        Distance = 20
                    },
                }
            };
            
            
            var expectedThree = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
                }
            };

            var expectedFour = new Route
            {
                Paths = new List<Path>
                {
                    new Path {Id = 5, PointOne = Points.A, PointTwo = Points.C, Distance = 5},
                    new Path {Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3},
                }
            };

            var expectedFive = new Route
            {
                Paths = new List<Path>
                {
                    new Path
                    {
                        Id = 5,
                        PointOne = Points.A,
                        PointTwo = Points.C,
                        Distance = 5
                    },
                    new Path
                    {
                        Id = 2,
                        PointOne = Points.B,
                        PointTwo = Points.C,
                        Distance = 4
                    },
                    new Path
                    {
                        Id = 1,
                        PointOne = Points.A,
                        PointTwo = Points.B,
                        Distance = 1
                    },
                    new Path {Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20},
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
                        expectedFour,
                        
                    }
                );
        }
    }
}