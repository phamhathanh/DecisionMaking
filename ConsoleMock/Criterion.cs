using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DecisionMaking
{
    public class Criterion
    {
        private readonly Dictionary<string, TrapezoidalFuzzyNumber> values;

        public ICollection<string> Labels => values.Keys;
        public ICollection<TrapezoidalFuzzyNumber> Values => values.Values;
        public double Weight { get; }

        public Criterion(Dictionary<string, TrapezoidalFuzzyNumber> values , double weight)
        {
            this.values = values;
            Weight = weight;
        }
    }
}
