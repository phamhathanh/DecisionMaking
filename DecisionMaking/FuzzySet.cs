using System;

namespace DecisionMaking
{
    public class FuzzySet<T>
    {
        private readonly Func<T, double> membershipFunction;

        public FuzzySet(Func<T, double> membershipFunction)
        {
            this.membershipFunction = membershipFunction;
        }

        public double GetMembershipOf(T item)
            => membershipFunction(item);
    }
}
