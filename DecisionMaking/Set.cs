using System;

namespace DecisionMaking
{
    public class Set<T>
    {
        private readonly Func<T, bool> membershipFunction;

        public Set(Func<T, bool> membershipFunction)
        {
            this.membershipFunction = membershipFunction;
        }

        public Set<T> UnionWith(Set<T> other) => new Set<T>(item => membershipFunction(item) || other.membershipFunction(item));

        public Set<T> IntersectWith(Set<T> other) => new Set<T>(item => membershipFunction(item) && other.membershipFunction(item));

        public Set<T> ExceptWith(Set<T> other) => new Set<T>(item => membershipFunction(item) && !other.membershipFunction(item));

        public bool Contains(T item) => membershipFunction(item);
    }
}
