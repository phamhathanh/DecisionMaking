using System;

namespace ConsoleMock
{
    public class FuzzySet<T>
    {
        private readonly Func<T, float> membershipFunction;

        public FuzzySet(Func<T, float> membershipFunction)
        {
            this.membershipFunction = membershipFunction;
        }

        public float GetMembershipOf(T item)
            => membershipFunction(item);
    }
}
