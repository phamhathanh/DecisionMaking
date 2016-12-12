using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace ConsoleMock
{
    public class LOWA
    {
        private readonly int labelCount;
        public LOWA(int labelCount)
        {
            if (labelCount < 0)
                throw new ArgumentException("Label count must be positive.");

            this.labelCount = labelCount;
        }

        public int Average(ICollection<int> labels)
        {
            if (labels.Count < 2)
                throw new ArgumentException("Insufficient labels.");

            if (labels.Count == 2)
                return Average(labels.First(), labels.Last());
            return Average(labels.First(), Average(labels.Skip(1).ToList()));
        }

        private int Average(int label1, int label2)
        {
            Validate(label1);
            Validate(label2);
            return Min(label1 + (int)Round((label2 - label1) / 2.0), labelCount - 1);
        }

        private void Validate(int label)
        {
            if (label >= labelCount)
                throw new ArgumentOutOfRangeException("Label is outside of range.");
        }
    }
}
