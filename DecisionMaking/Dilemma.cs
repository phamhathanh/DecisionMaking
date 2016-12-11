using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace DecisionMaking
{
    public class Dilemma
    {
        private readonly Dictionary<string, Dictionary<Criterion, string>> evaluation;
        private readonly string[] alternatives;
        private readonly Criterion[] criteria;

        public FuzzyBinaryRelation<string> CredibilityOfPreference { get; }

        public Dilemma(Dictionary<string, Dictionary<Criterion, string>> evaluation)
        {
            this.evaluation = evaluation;
            // Shallow.
            // TODO: Validation.

            alternatives = evaluation.Keys.ToArray();

            // Hack-ish.
            criteria = evaluation.First().Value.Keys.ToArray();

            int n = alternatives.Length;
            var credibilityOfPreference = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    string a1 = alternatives[i], a2 = alternatives[j];
                    credibilityOfPreference[i, j] = criteria.Min(c => Max(1 - c.Weight, GetCredibilityOfPreference(a1, a2, c)));
                }
            CredibilityOfPreference = new FuzzyBinaryRelation<string>(new HashSet<string>(alternatives), credibilityOfPreference);
        }

        private double GetCredibilityOfPreference(string alternative1, string alternative2, Criterion criterion)
        {
            bool argsAreValid = alternatives.Contains(alternative1) && alternatives.Contains(alternative2);
            if (!argsAreValid)
                throw new ArgumentException("Invalid alternative name.");

            var label1 = evaluation[alternative1][criterion];
            var fuzzyValue1 = criterion.GetValueOf(label1);

            var label2 = evaluation[alternative2][criterion];
            var fuzzyValue2 = criterion.GetValueOf(label2);

            return fuzzyValue1.GetCredibilityOfPreferenceOver(fuzzyValue2);
        }
    }
}
