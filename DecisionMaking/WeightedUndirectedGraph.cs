﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class WeightedUndirectedGraph
    {
        private readonly int vertexCount;
        private readonly Dictionary<Edge, double> weightByEdge;

        public ICollection<Edge> Edges => weightByEdge.Keys;

        public WeightedUndirectedGraph(int vertexCount, double[][] weights)
            // Complete case.
            // TODO: Derive instead.
        {
            this.vertexCount = vertexCount;

            this.weightByEdge = new Dictionary<Edge, double>(vertexCount*(vertexCount + 1)/2);

            // Need sanity check here.
            for (int i = 0; i < vertexCount; i++)
                for (int j = 0; j < i + 1; j++)
                    weightByEdge.Add(new Edge(i, j), weights[i][j]);
        }

        public WeightedUndirectedGraph(int vertexCount, Dictionary<int, Dictionary<int, double>> weights)
            // Args are not really suitable.
        {
            this.vertexCount = vertexCount;

            this.weightByEdge = new Dictionary<Edge, double>();
            foreach (var sourcesKVP in weights)
                foreach (var destinationKVP in sourcesKVP.Value)
                {
                    var source = sourcesKVP.Key;
                    var destination = destinationKVP.Key;
                    var weight = destinationKVP.Value;
                    this.weightByEdge.Add(new Edge(source, destination), weight);
                }
        }

        public WeightedUndirectedGraph(int vertexCount, Dictionary<Edge, double> weights)
            // Args are not really suitable.
        {
            this.vertexCount = vertexCount;
            this.weightByEdge = weights;
            // Shallow.
        }

        public double GetWeight(Edge edge)
            => weightByEdge[edge];

        public WeightedUndirectedGraph GetShortestSpanningTree()
        {
            var vertices = Enumerable.Range(0, vertexCount);
            var trees = vertices.Select(vertex => new HashSet<int>(new[] { vertex }))
                                .ToList();

            var edgesOrderByWeight = from edgeWeightKVP in weightByEdge
                                     orderby edgeWeightKVP.Value
                                     select edgeWeightKVP.Key;
            var output = new Dictionary<Edge, double>();
            foreach (var edge in edgesOrderByWeight)
            {
                var sourceTree = trees.Find(tree => tree.Contains(edge.source));
                var destinationTree = trees.Find(tree => tree.Contains(edge.destination));
                if (sourceTree == destinationTree)
                    continue;

                sourceTree.UnionWith(destinationTree);
                trees.Remove(destinationTree);
                output.Add(edge, weightByEdge[edge]);

                if (trees.Count == 1)
                    return new WeightedUndirectedGraph(vertexCount, output);
            }
            throw new InvalidOperationException("Graph is not connected.");
        }
    }
}
