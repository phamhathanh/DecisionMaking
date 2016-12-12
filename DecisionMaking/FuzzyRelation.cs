using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class FuzzyRelation<T>
    {
        private readonly Dictionary<T, int> indices;
        private readonly double[,] values;

        public FuzzyRelation(ISet<T> set, double[,] values)
        {
            this.indices = new Dictionary<T, int>(set.Count);
            int index = 0;
            foreach (var item in set)
            {
                indices.Add(item, index);
                index++;
            }

            this.values = (double[,])values.Clone();
        }

        public FuzzyRelation(ISet<T> set, Func<T, T, double> values)
        {
            int n = set.Count;
            this.indices = new Dictionary<T, int>(n);
            this.values = new double[n, n];
            int i = 0;
            foreach (var item in set)
            {
                indices.Add(item, i);
                int j = 0;
                foreach (var otherItem in set)
                {
                    this.values[i, j] = values(item, otherItem);
                    j++;
                }
                i++;
            }
        }

        public FuzzyRelation(ISet<T> set, WeightedUndirectedGraph graph)
        // Quasiorder only.
        // Should be symmetric.
        {
            int n = set.Count;
            this.values = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    this.values[i, j] = graph.GetWeight(new Edge(i, j));
        }

        public double GetRelation(T item1, T item2)
        {
            int index1, index2;
            try
            {
                index1 = indices[item1];
                index2 = indices[item2];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid item.");
            }

            return values[index1, index2];
        }
    }
}
