using System;
using System.Diagnostics;

namespace DecisionMaking
{
    public class SymmetricalTrapezoidalFuzzyNumber : TrapezoidalFuzzyNumber
    {
        private readonly float center, indifferenceThreshold, preferenceThreshold;

        public SymmetricalTrapezoidalFuzzyNumber(float center, float indifferenceThreshold, float preferenceThreshold)
            : base(center - preferenceThreshold/2, center - indifferenceThreshold/2, center + indifferenceThreshold/2, center + preferenceThreshold/2)
        {
            this.center = center;
            this.indifferenceThreshold = indifferenceThreshold;
            this.preferenceThreshold = preferenceThreshold;
        }
    }
}
