using System;

namespace WatchIt
{
    public class Warning : IComparable<Warning>
    {
        public Notification.Problem Problem { get; set; }
        public int Count { get; set; }

        public int CompareTo(Warning other)
        {
            return other == null ? 1 : Count.CompareTo(other.Count);
        }
    }
}
