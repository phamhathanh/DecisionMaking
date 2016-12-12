using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DecisionMaking
{
    public class WeightedDigraph<TVertex>
    {
        private readonly Dictionary<int, Dictionary<int, double>> weights;

        public WeightedDigraph(int vertexCount, Dictionary<int, Dictionary<int, double>> weights)
        {
            this.weights = weights;
            // Shallow.
        }
    }
}
