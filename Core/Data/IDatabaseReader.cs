namespace Core.Data
{
    using System.Collections.Generic;

    public interface IDatabaseReader
    {
        IEnumerable<Path> GetAllPaths();
    }
}