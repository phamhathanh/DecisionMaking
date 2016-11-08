﻿using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace ConsoleMock
{
    public class Dilemma
    {
        private readonly List<string> choices;
        private readonly List<Criterion> criteria;

        private Func<string, Criterion, float> evaluation;
        // Hack.
        public Func<float, float> preferenceThreshold, indifferenceThreshold, vetoThreshold;

        public Dilemma(IEnumerable<Criterion> criteria, IEnumerable<string> choices)
        {
            this.criteria = criteria.ToList();
            this.choices = choices.ToList();
        }

        public void AddEvaluation(Func<string, Criterion, float> evaluation)
            // Does not add, but set instead.
        {
            this.evaluation = evaluation;
        }

        public float GetPreference(string choice1, string choice2, Criterion criterion)
            // Implemented by Concordance Index
        {
            var evaluationOfChoice1 = evaluation(choice1, criterion);
            var evaluationOfChoice2 = evaluation(choice2, criterion);

            var numerator = preferenceThreshold(evaluationOfChoice1) - Min(evaluationOfChoice2 - evaluationOfChoice1, preferenceThreshold(evaluationOfChoice1));
            var denominator = preferenceThreshold(evaluationOfChoice1) - Min(evaluationOfChoice2 - evaluationOfChoice1, indifferenceThreshold(evaluationOfChoice1));
            return numerator / denominator;
        }

        public float GetStrictPreference(string choice1, string choice2, Criterion criterion)
            => 1 - GetPreference(choice2, choice1, criterion);

        public float GetIndifference(string choice1, string choice2, Criterion criterion)
            => Min(GetPreference(choice1, choice2, criterion), GetPreference(choice2, choice1, criterion));

        public float GetDiscredit(string choice1, string choice2, Criterion criterion)
        {
            var evaluationOfChoice1 = evaluation(choice1, criterion);
            var evaluationOfChoice2 = evaluation(choice2, criterion);

            var numerator = evaluationOfChoice2 - evaluationOfChoice1 - preferenceThreshold(evaluationOfChoice1);
            var denominator = vetoThreshold(evaluationOfChoice1) - preferenceThreshold(evaluationOfChoice1);
            var output = numerator / denominator;
            if (output < 0)
                return 0;
            if (output > 1)
                return 1;
            return output;
        }
    }
}
