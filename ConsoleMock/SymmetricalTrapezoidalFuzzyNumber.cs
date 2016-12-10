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

        public static double CredibilityOfPreference(SymmetricalTrapezoidalFuzzyNumber number1, SymmetricalTrapezoidalFuzzyNumber number2)
        {
            var averagePreferenceThreshold = (number1.preferenceThreshold + number2.preferenceThreshold) / 2;
            var averageIndifferenceThreshold = (number1.indifferenceThreshold + number2.indifferenceThreshold) / 2;
            var difference = number2.center - number1.center;
            var numerator = averagePreferenceThreshold - Min(difference, averagePreferenceThreshold);
            var denominator = averagePreferenceThreshold - Min(difference, averageIndifferenceThreshold);
            return numerator / denominator;
        }
    }
}
