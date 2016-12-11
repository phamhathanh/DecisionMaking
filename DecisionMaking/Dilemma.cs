using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace DecisionMaking
{
    public class Dilemma
    {
        private readonly Dictionary<string, Dictionary<Criterion, string>> evaluation;

        public Dilemma(Dictionary<string, Dictionary<Criterion, string>> evaluation)
        {
            this.evaluation = evaluation;
            // Shallow.
        }

        public double GetCredibilityOfPreference(string alternative1, string alternative2, Criterion criterion)
        {
            var alternatives = GetAlternatives();
            bool argsAreValid = alternatives.Contains(alternative1) && alternatives.Contains(alternative2);
            if (!argsAreValid)
                throw new ArgumentException("Invalid alternative name.");

            var label1 = evaluation[alternative1][criterion];
            var fuzzyValue1 = criterion.GetValueOf(label1);

            var label2 = evaluation[alternative2][criterion];
            var fuzzyValue2 = criterion.GetValueOf(label2);

            return fuzzyValue1.GetCredibilityOfPreferenceOver(fuzzyValue2);
        }

        private ICollection<string> GetAlternatives()
            => evaluation.Keys;
    }
}
