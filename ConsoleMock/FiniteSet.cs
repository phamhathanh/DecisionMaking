using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class FiniteSet<T> : IEnumerable<T>
    {
        private T[] members;

        public FiniteSet(IEnumerable<T> members)
        {
            this.members = members.ToArray();
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)this.members).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this.members).GetEnumerator();

        public FiniteSet<T> Add(T item) => new FiniteSet<T>(members.Append(item));

        public FiniteSet<T> UnionWith(FiniteSet<T> other) => new FiniteSet<T>(members.Concat(other.members));

        public FiniteSet<T> IntersectWith(FiniteSet<T> other) => new FiniteSet<T>(members.Intersect(other.members));

        public FiniteSet<T> ExceptWith(FiniteSet<T> other) => new FiniteSet<T>(members.Except(other.members));

        public bool Contains(T item) => members.Contains(item);

        public bool IsProperSubsetOf(FiniteSet<T> other) => IsSubsetOf(other) && !SetEquals(other);

        public bool IsSubsetOf(FiniteSet<T> other)
        {
            if (this.members.Length > other.members.Length)
                return false;

            foreach (var member in members)
                if (!other.Contains(member))
                    return false;

            return true;
        }

        public bool SetEquals(FiniteSet<T> other)
        {
            if (this.members.Length != other.members.Length)
                return false;

            foreach (var member in members)
                if (!other.Contains(member))
                    return false;

            return true;
        }

        public bool IsProperSupersetOf(FiniteSet<T> other) => other.IsProperSubsetOf(this);

        public bool IsSupersetOf(FiniteSet<T> other) => other.IsSubsetOf(this);

        public bool Overlaps(FiniteSet<T> other)
        {
            foreach (var member in members)
                if (other.Contains(member))
                    return true;

            return true;
        }
    }
}
