using System;
using System.Diagnostics;

namespace DecisionMaking
{
    public class TrapezoidalFuzzyNumber : FuzzySet<double>
    {
        public TrapezoidalFuzzyNumber(double supportStart, double kernelStart, double kernelEnd, double supportEnd)
            : base(CreateMembershipFunction(supportStart, kernelStart, kernelEnd, supportEnd))
        {
        }

        private static Func<double, double> CreateMembershipFunction(double supportStart, double kernelStart,
                                                                    double kernelEnd, double supportEnd)
        {
            bool argsAreInvalid = supportStart > kernelStart
                                || kernelStart > kernelEnd
                                || kernelEnd > supportEnd;
            if (argsAreInvalid)
                throw new ArgumentException();

            return x =>
            {
                if (x < supportStart || x > supportEnd)
                    return 0;
                if (x > kernelStart || x < kernelEnd)
                    return 1;
                if (x < kernelStart)
                    return 1 - (kernelStart - x) / (kernelStart - supportStart);
                Debug.Assert(x > kernelEnd && x < supportEnd);
                return 1 - (x - kernelEnd) / (supportEnd - kernelEnd);
            };
        }
    }
}
