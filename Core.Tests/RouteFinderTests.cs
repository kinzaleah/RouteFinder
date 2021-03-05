using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    using System;
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
            var routeFinder = new RouteFinder(new RouteExplorer(), new ShortestRouteFinder(), routes, new RouteValidator());
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
        public void Same_Start_And_End_Input_Should_Return_Exception()
        {
            // Arrange
            var routes = new List<Path>
            {
                new Path { Id = 1, PointOne = Points.A, PointTwo = Points.B, Distance = 1 },
                new Path { Id = 2, PointOne = Points.B, PointTwo = Points.C, Distance = 4 },
                new Path { Id = 3, PointOne = Points.C, PointTwo = Points.D, Distance = 3 },
                new Path { Id = 4, PointOne = Points.A, PointTwo = Points.D, Distance = 20 }
            };
            var routeFinder = new RouteFinder(new RouteExplorer(), new ShortestRouteFinder(), routes, new RouteValidator());
            
            var startPoint = Points.A;
            var endPoint = Points.A;

            // Act
            var result = routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            Assert.Throws<InputValidationException>(() => result);
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

            var mockRouteValidator = new Mock<IRouteValidator>();

            mockShortestRouteFinder
                .Setup(x => x.GetShortestRoute(expected))
                .Returns(mockShortestRouteFinderResult);

            var routeFinder = new RouteFinder(mockRouteExplorer.Object, mockShortestRouteFinder.Object, routes, mockRouteValidator.Object);
            
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
            var startPoint = Points.A;
            var endPoint = Points.A;

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

            var mockRouteValidator = new Mock<IRouteValidator>();

            var mockRouteValidatorResult = new InputValidationException("The start and end point cannot be the same");

            mockRouteValidator
                .Setup(x => x.ValidateInput(startPoint, endPoint))
                .Throws(mockRouteValidatorResult);

            var routeFinder = new RouteFinder(mockRouteExplorer.Object, mockShortestRouteFinder.Object, routes, mockRouteValidator.Object);

            // Act
            Action result = () => routeFinder.CalculateShortestRoute(startPoint, endPoint);

            // Assert
            //Assert.Equal("The start and end point cannot be the same", mockRouteValidatorResult.Message);  
            result.Should().Throw<InputValidationException>()
                  .WithMessage("The start and end point cannot be the same");
        }
    }
}