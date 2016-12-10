using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace DecisionMaking
{
    public class Dilemma
    {
        private readonly string[] alternatives;
        private readonly Criterion[] criteria;
        private readonly Dictionary<string, Dictionary<Criterion, string>> evaluation;

        public Dilemma(ICollection<Criterion> criteria, ICollection<string> alternatives, Dictionary<string, Dictionary<Criterion, string>> evaluation)
        {
            this.criteria = criteria.ToArray();
            this.alternatives = alternatives.ToArray();

            this.evaluation = evaluation;
            // Shallow.
        }
    }
}
