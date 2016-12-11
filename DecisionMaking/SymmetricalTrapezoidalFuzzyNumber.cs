using static System.Math;

namespace DecisionMaking
{
    public class SymmetricalTrapezoidalFuzzyNumber : TrapezoidalFuzzyNumber
    {
        private readonly double center, indifferenceThreshold, preferenceThreshold;

        public SymmetricalTrapezoidalFuzzyNumber(double center, double indifferenceThreshold, double preferenceThreshold)
            : base(center - preferenceThreshold/2, center - indifferenceThreshold/2, center + indifferenceThreshold/2, center + preferenceThreshold/2)
        {
            this.center = center;
            this.indifferenceThreshold = indifferenceThreshold;
            this.preferenceThreshold = preferenceThreshold;
        }

        public double GetCredibilityOfPreference(SymmetricalTrapezoidalFuzzyNumber other)
        {
            var n1 = this; var n2 = other;

            var averagePreferenceThreshold = (n1.preferenceThreshold + n2.preferenceThreshold) / 2;
            var averageIndifferenceThreshold = (n1.indifferenceThreshold + n2.indifferenceThreshold) / 2;
            var difference = n2.center - n1.center;
            var numerator = averagePreferenceThreshold - Min(difference, averagePreferenceThreshold);
            var denominator = averagePreferenceThreshold - Min(difference, averageIndifferenceThreshold);
            return numerator / denominator;
        }
    }
}
