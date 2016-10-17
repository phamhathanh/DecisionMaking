using System;
using System.Diagnostics;

namespace ConsoleMock
{
    public class TrapezoidalFuzzyNumber : FuzzySet<float>
    {
        public TrapezoidalFuzzyNumber(float supportStart, float kernelStart, float kernelEnd, float supportEnd)
            : base(CreateMembershipFunction(supportStart, kernelStart, kernelEnd, supportEnd))
        {
        }

        private static Func<float, float> CreateMembershipFunction(float supportStart, float kernelStart,
                                                                    float kernelEnd, float supportEnd)
        {
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
