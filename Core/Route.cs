using System.Collections.Generic;

namespace Core
{
    using System.Linq;

    public class Route
    {
        public List<Path> Paths { get; set; }

        public int TotalDistance
        {
            get { return Paths.Select(x => x.Distance).Sum(); }
        }

        public Route()
        {
            Paths = new List<Path>();
        }
    }
}