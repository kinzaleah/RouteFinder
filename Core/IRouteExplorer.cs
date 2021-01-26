﻿namespace Core
{
    using System.Collections.Generic;

    public interface IRouteExplorer
    {
        IEnumerable<Route> GetAllPossibleRoutes(List<Path> paths, Points startPoint, Points endPoint);
    }
}