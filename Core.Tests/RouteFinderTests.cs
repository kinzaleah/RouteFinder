namespace Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Data;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class RouteFinderTests
    {
        private readonly Point pointA = new Point { Id = 1, Name = "A" };
        private readonly Point pointB = new Point { Id = 2, Name = "B" };
        private readonly Point pointC = new Point { Id = 3, Name = "C" };
        private readonly Point pointD = new Point { Id = 4, Name = "D" };

        [Fact]
        public void Given_Two_Possible_Routes_RouteFinder_Should_Find_The_Shortest_One()
        {
            // Arrange
            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                new Path { Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20 }
            };

            var mockDatabaseReader = new Mock<IDatabaseReader>();

            mockDatabaseReader.Setup(x => x.GetAllPaths())
                .Returns(routes);

            var routeFinder = new RouteFinder(new RouteExplorer(), new ShortestRouteFinder(), mockDatabaseReader.Object, new RouteValidator());
            
            var startPoint = this.pointA;
            var endPoint = this.pointD;

            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            result.Paths.Should().BeEquivalentTo(
                new List<Path>
                {
                    new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                    new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                    new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                }
            );
        }

        [Fact]
        public void Given_Two_Possible_Routes_RouteFinder_Should_Find_The_Shortest_One_CalculateShortestRouteTakesString()
        {
            // Arrange
            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                new Path { Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20 }
            };

            var startPoint = "A";
            var endPoint = "D";

            var mockDatabaseReader = new Mock<IDatabaseReader>();

            mockDatabaseReader.Setup(x => x.GetAllPaths())
                .Returns(routes);

            mockDatabaseReader.Setup(x => x.GetPoint(startPoint)).Returns(this.pointA);
            mockDatabaseReader.Setup(x => x.GetPoint(endPoint)).Returns(this.pointD);

            var routeFinder = new RouteFinder(new RouteExplorer(), new ShortestRouteFinder(), mockDatabaseReader.Object, new RouteValidator());

            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            result.Paths.Should().BeEquivalentTo(
                new List<Path>
                {
                    new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                    new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                    new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                }
            );
        }

        [Fact]
        public void Same_Start_And_End_Input_Should_Return_Exception()
        {
            // Arrange

            var mockDatabaseReader = new Mock<IDatabaseReader>();

            mockDatabaseReader.Setup(x => x.GetAllPaths()).Returns( new List<Path>());

            var routeFinder = new RouteFinder(new RouteExplorer(), new ShortestRouteFinder(), mockDatabaseReader.Object, new RouteValidator());
           
            // Act & Assert
            Assert.Throws<InputValidationException>(() => routeFinder.CalculateShortestRoute(this.pointA, this.pointA));
        }

        [Fact]
        public void RouteFinder_Should_Call_RouteExplorer()
        {
            // Arrange
            var startPoint = this.pointA;
            var endPoint = this.pointD;

            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                new Path { Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20 }
            };

            var expected = new List<Route>
            {
                new Route
                {
                    Paths = new List<Path>()
                }
            };

            var mockRouteExplorer = new Mock<IRouteExplorer>();
            
            mockRouteExplorer
                .Setup(x => x.GetAllPossibleRoutes(It.IsAny<List<Path>>(), It.IsAny<Point>(), It.IsAny<Point>()))
                .Returns(expected);

            var mockShortestRouteFinder = new Mock<IShortestRouteFinder>();

            var mockShortestRouteFinderResult = new Route();

            var mockRouteValidator = new Mock<IRouteValidator>();

            mockShortestRouteFinder
                .Setup(x => x.GetShortestRoute(expected))
                .Returns(mockShortestRouteFinderResult);

            var mockDatabaseReader = new Mock<IDatabaseReader>();

            mockDatabaseReader.Setup(x => x.GetAllPaths())
                .Returns(routes);

            var routeFinder = new RouteFinder(mockRouteExplorer.Object, mockShortestRouteFinder.Object, mockDatabaseReader.Object, mockRouteValidator.Object);
            
            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            mockRouteExplorer.Verify(x => x.GetAllPossibleRoutes(routes, startPoint, endPoint), Times.Once);
            mockShortestRouteFinder.Verify(x => x.GetShortestRoute(expected), Times.Once);
            mockShortestRouteFinderResult.Should().Be(result);
        }

        [Fact]
        public void RouteFinder_Should_Call_RouteValidator()
        {
            // Arrange
            var startPoint = this.pointA;
            var endPoint = this.pointA;

            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = this.pointA, PointTwo = this.pointB, Distance = 1 },
                new Path { Id = 2, PointOne = this.pointB, PointTwo = this.pointC, Distance = 4 },
                new Path { Id = 3, PointOne = this.pointC, PointTwo = this.pointD, Distance = 3 },
                new Path { Id = 4, PointOne = this.pointA, PointTwo = this.pointD, Distance = 20 }
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
                .Setup(x => x.GetAllPossibleRoutes(It.IsAny<List<Path>>(), It.IsAny<Point>(), It.IsAny<Point>()))
                .Returns(expected);

            var mockShortestRouteFinder = new Mock<IShortestRouteFinder>();

            var mockShortestRouteFinderResult = new Route();

            mockShortestRouteFinder
                .Setup(x => x.GetShortestRoute(expected))
                .Returns(mockShortestRouteFinderResult);

            var mockRouteValidator = new Mock<IRouteValidator>();

            var mockRouteValidatorResult = new InputValidationException("The start and end point cannot be the same");

            mockRouteValidator
                .Setup(x => x.ValidateInput(startPoint, endPoint))
                .Throws(mockRouteValidatorResult);

            var mockDatabaseReader = new Mock<IDatabaseReader>();

            mockDatabaseReader.Setup(x => x.GetAllPaths())
                .Returns(routes);

            var routeFinder = new RouteFinder(mockRouteExplorer.Object, mockShortestRouteFinder.Object, mockDatabaseReader.Object, mockRouteValidator.Object);

            // Act
            Action result = () => routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert 
            result.Should().Throw<InputValidationException>()
                  .WithMessage("The start and end point cannot be the same");
        }
    }
}