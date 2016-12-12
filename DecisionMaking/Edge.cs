using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public struct Edge
    {
        public readonly int source, destination;
        public Edge(int source, int destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public static bool operator ==(Edge edge1, Edge edge2)
            => edge1.source == edge2.source && edge1.destination == edge2.destination;

        public static bool operator !=(Edge edge1, Edge edge2)
            => !(edge1 == edge2);

        public override bool Equals(object other)
            => other is Edge && (Edge)other == this;

        public override int GetHashCode()
            => source.GetHashCode() + 5 * destination.GetHashCode();
    }
}
