﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionMaking
{
    public class Program
    {
        private const string POOR = "Poor", FAIR = "Fair", GOOD = "Good", FAIR_TO_GOOD = "Fair to good", VERY_GOOD = "Very good", NOT_CLEAR = "Not clear",
                            SAPA = "Sapa", TAM_DAO = "Tam Dao", BA_VI = "Ba Vi";

        public static void Main(string[] args)
        {
            var values = new Dictionary<string, TrapezoidalFuzzyNumber>
            {
                [POOR] = new TrapezoidalFuzzyNumber(0, 0.2, 0.2, 0.4),
                [FAIR] = new TrapezoidalFuzzyNumber(0.4, 0.6, 0.6, 0.8),
                [GOOD] = new TrapezoidalFuzzyNumber(0.6, 0.8, 0.8, 1),
                [FAIR_TO_GOOD] = new TrapezoidalFuzzyNumber(0.4, 0.6, 0.6, 1),
                [VERY_GOOD] = new TrapezoidalFuzzyNumber(0.8, 1, 1, 1),
                [NOT_CLEAR] = new TrapezoidalFuzzyNumber(0, 0, 1, 1)
            };
            var quality = new Criterion(values, 1);
            var price = new Criterion(values, 0.625);
            var funness = new Criterion(values, 0.625);
            var distance = new Criterion(values, 0.25);
            var criteria = new[] { quality, price, funness, distance };

            var alternatives = new[] { SAPA, TAM_DAO, BA_VI };

            var evaluation = new Dictionary<string, Dictionary<Criterion, string>>
            {
                [SAPA] = new Dictionary<Criterion, string> { [quality] = GOOD, [price] = POOR, [funness] = POOR, [distance] = GOOD },
                [TAM_DAO] = new Dictionary<Criterion, string> { [quality] = VERY_GOOD, [price] = POOR, [funness] = FAIR_TO_GOOD, [distance] = NOT_CLEAR },
                [BA_VI] = new Dictionary<Criterion, string> { [quality] = FAIR, [price] = POOR, [funness] = FAIR, [distance] = FAIR }
            };

            var dilemma = new Dilemma(evaluation);

            foreach (var alternative in alternatives)
            {
                Console.WriteLine($"{alternative}\t{dilemma.GetNonDominationDegree(alternative)}\t{dilemma.GetNonDominanceDegree(alternative)}");
            }

            Console.ReadLine();
        }
    }
}