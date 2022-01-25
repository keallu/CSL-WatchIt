using System;

namespace WatchIt.Managers
{
    public class ProblemType : IComparable<ProblemType>
    {
        public string Sprite { get; set; }
        public int TotalBuildings { get; set; }
        public int TotalNetworks { get; set; }
        public int Total { get; set; }

        public int CompareTo(ProblemType other)
        {
            return other == null ? 1 : Total.CompareTo(other.Total);
        }
    }
}
