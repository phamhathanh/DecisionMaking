using System;
using System.Diagnostics;

namespace DecisionMaking
{
    public class TrapezoidalFuzzyNumber : FuzzySet<double>
    {
        private readonly double supportStart, kernelStart, kernelEnd, supportEnd;

        public TrapezoidalFuzzyNumber(double supportStart, double kernelStart, double kernelEnd, double supportEnd)
            : base(CreateMembershipFunction(supportStart, kernelStart, kernelEnd, supportEnd))
        {
            this.supportStart = supportStart;
            this.kernelStart = kernelStart;
            this.kernelEnd = kernelEnd;
            this.supportEnd = supportEnd;
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

        public double GetCredibilityOfPreferenceOver(TrapezoidalFuzzyNumber other)
        {
            var n1 = this; var n2 = other;
            double a3 = n1.kernelEnd, a4 = n1.supportEnd,
                b1 = n2.supportStart, b2 = n2.kernelStart;
            if (a3 >= b2)
                return 1;
            if (a4 <= b1)
                return 0;
            return (a4 - b1)/(b2 - b1 + a4 - a3);
            // Need test.
        }
    }
}
