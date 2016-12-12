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
            var outrankingValues = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    string a1 = alternatives[i], a2 = alternatives[j];
                    outrankingValues[i, j] = criteria.Min(c => Max(1 - c.Weight, GetCredibilityOfPreference(a1, a2, c)));
                }

            var alternativesSet = new HashSet<string>(alternatives);
            Func<int, int, double> outrankingRelation = (i, j) => criteria.Min(c => Max(1 - c.Weight, GetCredibilityOfPreference(alternatives[i], alternatives[j], c)));
            CredibilityOfPreference = new FuzzyBinaryRelation<string>(alternativesSet, GetProximityRelation);
        }

        private double GetProximityRelation(int index1, int index2)
            => Min(GetOutranking(index1, index2), GetOutranking(index2, index1));

        private double GetOutranking(int index1, int index2)
            => criteria.Min(criterion => Max(1 - criterion.Weight, GetCredibilityOfPreference(alternatives[index1], alternatives[index2], criterion)));

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
