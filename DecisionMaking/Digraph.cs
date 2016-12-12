using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class Digraph
    {
        private readonly HashSet<Edge> edges;

        public Digraph(int vertexCount, HashSet<Edge> edges)
        {
            this.edges = edges;
        }
    }
}
