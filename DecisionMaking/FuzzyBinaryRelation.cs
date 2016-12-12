using System;
using System.Collections.Generic;

namespace DecisionMaking
{
    public class FuzzyBinaryRelation<T>
    {
        private readonly Dictionary<T, int> indices;
        private readonly double[,] values;

        public FuzzyBinaryRelation(ISet<T> set, double[,] values)
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

        public FuzzyBinaryRelation(ISet<T> set, Func<int, int, double> values)
        {
            this.indices = new Dictionary<T, int>(set.Count);
            int index = 0;
            foreach (var item in set)
            {
                indices.Add(item, index);
                index++;
            }

            int n = set.Count;
            this.values = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    this.values[i, j] = values(i, j);
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

        public FuzzyBinaryRelation<T> Inverse()
            => new FuzzyBinaryRelation<T>(new HashSet<T>(indices.Keys), (i, j) => values[j, i]);
            // Cachable.
    }
}
