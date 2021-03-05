namespace Core
{
    public class RouteValidator : IRouteValidator
    {
        public void ValidateInput(Points startPoint, Points endPoint)
        {
            if (startPoint == endPoint)
            {
                throw new InputValidationException("The start and end point cannot be the same");
            }
        }
    }
}