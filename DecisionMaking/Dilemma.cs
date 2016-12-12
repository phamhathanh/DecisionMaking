using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace DecisionMaking
{
    public class Dilemma
    {
        private delegate double FuzzyRelation(string alternative1, string alternative2);

        private readonly Dictionary<string, Dictionary<Criterion, string>> evaluation;
        private readonly string[] alternatives;
        private readonly Criterion[] criteria;

        private readonly Dictionary<string, double> nonDominationDegree, nonDominanceDegree;

        public Dilemma(Dictionary<string, Dictionary<Criterion, string>> evaluation)
        {
            this.evaluation = evaluation;
            // Shallow.
            // TODO: Validation.

            alternatives = evaluation.Keys.ToArray();

            // Hack-ish.
            criteria = evaluation.First().Value.Keys.ToArray();

            //var alternativesSet = new HashSet<string>(alternatives);
            //var proximityRelation = new FuzzyBinaryRelation<string>(alternativesSet, GetProximityRelation);

            int n = alternatives.Length;
            var vertices = Enumerable.Range(0, n);
            var alternativesSet = new HashSet<string>(alternatives);

            FuzzyRelation outranking = (a1, a2) => criteria.Min(c => Max(1 - c.Weight, GetCredibilityOfPreference(a1, a2, c))),
                strictPreference = (a1, a2) => Max(outranking(a1, a2) - outranking(a2, a1), 0);

            this.nonDominationDegree = new Dictionary<string, double>(n);
            this.nonDominanceDegree = new Dictionary<string, double>(n);
            foreach (var alternative in alternatives)
            {
                nonDominationDegree.Add(alternative, alternatives.Min(other => 1 - strictPreference(other, alternative)));
                nonDominanceDegree.Add(alternative, 1 - alternatives.Max(other => strictPreference(alternative, other)));
            }
        }

        private double GetCredibilityOfPreference(string alternative1, string alternative2, Criterion criterion)
        {
            ValidateAlternative(alternative1);
            ValidateAlternative(alternative2);

            var label1 = evaluation[alternative1][criterion];
            var fuzzyValue1 = criterion.GetValueOf(label1);

            var label2 = evaluation[alternative2][criterion];
            var fuzzyValue2 = criterion.GetValueOf(label2);

            return fuzzyValue1.GetCredibilityOfPreferenceOver(fuzzyValue2);
        }

        private void ValidateAlternative(string alternative)
        {
            if (!alternatives.Contains(alternative))
                throw new ArgumentException("Invalid alternative name.");
        }

        public double GetNonDominationDegree(string alternative)
        {
            ValidateAlternative(alternative);
            return nonDominationDegree[alternative];
        }

        public double GetNonDominanceDegree(string alternative)
        {
            ValidateAlternative(alternative);
            return nonDominanceDegree[alternative];
        }
    }
}
